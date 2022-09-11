using DiaryApp.Data;
using DiaryApp.DTO;
using DiaryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DiaryApp.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext ctx;
        private readonly IHttpContextAccessor httpCtx;
        private readonly IConfiguration cfg;

        public AuthService(DataContext ctx, IHttpContextAccessor httpCtx, IConfiguration cfg)
        {
            this.ctx = ctx;
            this.httpCtx = httpCtx;
            this.cfg = cfg;
        }

        public bool EmailExists(string email)
        {
            User? user = ctx.Users.FirstOrDefault(u => u.email == email);
            return user != null;
        }

        public Option<string> Login(LoginDTO data)
        {
            if (!EmailExists(data.email)) return new Option<string>(null, "Email does not exist");
            User? user = ctx.Users.FirstOrDefault((u) => u.email == data.email && u.password == data.password);
            if (user == null) return new Option<string>(null, "Invalid password");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(cfg.GetSection("AppSettings:Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            JwtSecurityToken token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(30), signingCredentials: creds);
            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new Option<string>(jwt);
        }

        public Option<string> Register(RegisterDTO data)
        {
            bool checks = true;

            checks = data.password0.Equals(data.password1);
            if (!checks) return new Option<string>(null, "Password and its confirmation must be the same");

            checks = !EmailExists(data.email);
            if (!checks) return new Option<string>(null, "Email is already registered");

            checks = !UsernameExists(data.username);
            if (!checks) return new Option<string>(null, "Username is already registered");

            ctx.Users.Add(new User
            {
                email = data.email,
                username = data.username,
                password = data.password0,

            });
            ctx.SaveChanges();

            return new Option<string>("Registered user");
        }

        public bool UsernameExists(string username)
        {
            User? user = ctx.Users.FirstOrDefault(u => u.username == username);
            return user != null;
        }
    }
}
