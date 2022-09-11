using DiaryApp.DTO;
using DiaryApp.Models;

namespace DiaryApp.Services.AuthService
{
    public interface IAuthService
    {
        public bool UsernameExists(string username);
        public bool EmailExists(string email);
        public Option<string> Login(LoginDTO data);
        public Option<string> Register(RegisterDTO data);
    }
}
