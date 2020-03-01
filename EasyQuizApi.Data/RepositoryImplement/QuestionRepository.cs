using System;
using System.Linq;
using System.Threading.Tasks;
using EasyQuizApi.Data.RepositoryBase;
using EasyQuizApi.Model.Entities;
using EasyQuizApi.Share.Dto;
using EasyQuizApi.Share.ModelExtention;
using Microsoft.EntityFrameworkCore;

namespace EasyQuizApi.Data.RepositoryImplement
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly EasyQuizDbContext _dbContext;
        public QuestionRepository(EasyQuizDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public Task<int> CreateQuestion(QuestionCreateModel data)
        {
            var question = new CauHoi();
            question.MappingData(data);
            _dbContext.CauHois.Add(question);
            return _dbContext.SaveChangesAsync();
        }
        
        public Task<QuestionListReponse> GetListQuestion(ListQuestionPageDto data)
        {
            var result = new QuestionListReponse();
            var monhocId = data.MonHoc.GetValueOrDefault();
            var giaovienId = data.GiaoVien.GetValueOrDefault();
            var query = _dbContext.CauHois.Include(x => x.Options).AsQueryable();
            if (monhocId > 0 || giaovienId > 0)
            {
                query = query.Where(x => x.MonHocId == monhocId || x.GiaoVienId == giaovienId);
            }

            result.TotalRow = query.Count();
            result.Data = query.Skip((data.Page - 1) * data.PageSize).Take(data.PageSize).Select(x =>
                new QuestionListItemDto()
                {
                    Id = x.Id,
                    Content = x.Content,
                    MonHocId = x.MonHocId,
                    Options = x.Options.Select(o => new OptionDto()
                    {
                        Id = o.Id,
                        Content = o.Content
                    }).ToList()
                }).ToList();
            return Task.FromResult(result);
        }
    }
}