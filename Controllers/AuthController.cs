using DiaryApp.Data;
using DiaryApp.DTO;
using DiaryApp.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext ctx;
        private readonly IAuthService authService;

        public AuthController(DataContext dataContext, IAuthService authService)
        {
            ctx = dataContext;
            this.authService = authService;
        }

        [HttpPost("register")]
        public ActionResult<string> Register(RegisterDTO data)
        {
            Option<string> res = authService.Register(data);
            return res.HasData ? Ok(res.HasData) : BadRequest(res.msg);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginDTO data)
        {
            Option<string> token = authService.Login(data);
            return token.HasData ? Ok(token.UnwrappedData) : BadRequest(token.msg);
        }
    }
}
