using DiaryApp.Models;

namespace DiaryApp.DTO
{
    public class DiaryReadAllResultDTO
    {
        public Diary[] diaries { get; set; }
        public int count { get; set; }
    }
}
