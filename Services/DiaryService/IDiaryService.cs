using DiaryApp.DTO;
using DiaryApp.Models;

namespace DiaryApp.Services.DiaryService
{
    public interface IDiaryService
    {
        public Diary[] ReadAll(DiaryReadAllDTO data, int userID);
        public Option<Diary> Read(int id, int userID);
        public Option<Diary> Create(DiaryCreateDTO data, int userID);
        public Option<Diary> Update(DiaryUpdateDTO data, int userID);
        public Option<string> Archive(int id, int userID);
        public Option<string> Unarchive(int id, int userID);
    }
}
