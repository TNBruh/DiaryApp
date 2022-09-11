using DiaryApp.Data;
using DiaryApp.DTO;
using DiaryApp.Models;
using DiaryApp.Services.DiaryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        private readonly DataContext ctx;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDiaryService diaryService;
        private int UserID {
            get
            {
                return int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }

        public DiaryController(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IDiaryService diaryService)
        {
            ctx = dataContext;
            this.httpContextAccessor = httpContextAccessor;
            this.diaryService = diaryService;
        }

        //public int GetUserIDClaim()
        //{
        //    return int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        //}

        [HttpGet, Authorize]
        public ActionResult<DiaryReadAllResultDTO> ReadAll([FromQuery] DiaryReadAllDTO data)
        {
            Diary[] diaries = diaryService.ReadAll(data, UserID);
            return new DiaryReadAllResultDTO
            {
                diaries = diaries,
                count = diaries.Length,
            };
        }

        [HttpGet("{id}"), Authorize]
        public ActionResult<Diary> Read(int id)
        {
            Option<Diary> res = diaryService.Read(id, UserID);
            return res.HasData ? Ok(res.UnwrappedData) : BadRequest(res.msg);
        }

        [HttpPost, Authorize]
        public ActionResult<Diary> Create(DiaryCreateDTO data)
        {
            Option<Diary> res = diaryService.Create(data, UserID);
            return res.HasData ? Ok(res.UnwrappedData) : BadRequest(res.msg);
        }

        [HttpPut, Authorize]
        public ActionResult<Diary> Update(DiaryUpdateDTO data)
        {
            Option<Diary> res = diaryService.Update(data, UserID);
            return res.HasData ? Ok(res.UnwrappedData) : BadRequest(res.msg);
        }

        [HttpPost("archive"), Authorize]
        public ActionResult<string> Archive([FromQuery] int id)
        {
            Option<string> res = diaryService.Archive(id, UserID);
            return res.HasData ? Ok(res.UnwrappedData) : BadRequest(res.msg);

        }

        [HttpDelete("archive"), Authorize]
        public ActionResult<string> Unarchive([FromQuery] int id)
        {
            Option<string> res = diaryService.Unarchive(id, UserID);
            return res.HasData ? Ok(res.UnwrappedData) : BadRequest(res.msg);
        }
    }
}
