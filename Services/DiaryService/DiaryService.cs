using DiaryApp.Data;
using DiaryApp.DTO;
using DiaryApp.Models;
using System;
using System.Reflection.Metadata.Ecma335;

namespace DiaryApp.Services.DiaryService
{
    public class DiaryService : IDiaryService
    {
        private readonly DataContext ctx;
        private readonly IHttpContextAccessor httpCtx;
        private readonly IConfiguration cfg;

        public DiaryService(DataContext ctx, IHttpContextAccessor httpCtx, IConfiguration cfg)
        {
            this.ctx = ctx;
            this.httpCtx = httpCtx;
            this.cfg = cfg;
        }

        public Option<string> Archive(int id, int userID)
        {
            Diary? diary = ctx.Diaries.FirstOrDefault(d => d.id == id && d.userID == userID);
            if (diary == null) return new Option<string>(null, "Unable to archive entry");
            diary.isArchived = true;
            diary.updated = DateTime.Now;
            ctx.Diaries.Update(diary);
            ctx.SaveChanges();
            return new Option<string>("Archived entry");
        }

        public Option<Diary> Create(DiaryCreateDTO data, int userID)
        {
            User? user = ctx.Users.FirstOrDefault(u => u.id == userID);
            if (user == null) return new Option<Diary>(null, "Cannot identify user"); //this means the secret key leaked somewhere
            Diary diary = new Diary
            {
                isArchived = false,
                content = data.content,
                created = DateTime.Now,
                title = data.title,
                updated = DateTime.Now,
                user = user!,
            };
            ctx.Diaries.Add(diary);
            ctx.SaveChanges();
            return new Option<Diary>(diary);
        }

        public Option<Diary> Read(int id, int userID)
        {
            Diary? res = ctx.Diaries.FirstOrDefault(d => d.id == id && d.userID == userID && !d.isArchived);
            return res != null ? new Option<Diary>(res) : new Option<Diary>(null, "Cannot find entry");
        }

        public Diary[] ReadAll(DiaryReadAllDTO data, int userID)
        {
            return ctx.Diaries.Where(d => (!d.isArchived || data.includeArchived) && d.userID == userID).Where(d => d.title.Contains(data.title)).Skip(data.page * 10).Take(10).ToArray();
        }

        public Option<string> Unarchive(int id, int userID)
        {
            Diary? diary = ctx.Diaries.FirstOrDefault(d => d.id == id && d.userID == userID);
            if (diary == null) return new Option<string>(null, "Unable to unarchive entry");
            diary.isArchived = false;
            diary.updated = DateTime.Now;
            ctx.Diaries.Update(diary);
            ctx.SaveChanges();
            return new Option<string>("Unarchived entry");
        }

        public Option<Diary> Update(DiaryUpdateDTO data, int userID)
        {
            Diary? diary = ctx.Diaries.FirstOrDefault(d => d.id == data.id && d.userID == userID && !d.isArchived);
            if (diary == null) return new Option<Diary>(null, "Unable to edit entry");
            diary.title = data.title;
            diary.content = data.content;
            diary.updated = DateTime.Now;
            ctx.Diaries.Update(diary);
            ctx.SaveChanges();
            return new Option<Diary>(diary);
        }
    }
}
