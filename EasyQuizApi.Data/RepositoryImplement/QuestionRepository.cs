using System;
using System.Collections.Generic;
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
                    MonHoc = x.MonHoc.Name,
                    Options = x.Options.Select(o => new OptionDto()
                    {
                        Id = o.Id,
                        Content = o.Content
                    }).ToList()
                }).ToList();
            return Task.FromResult(result);
        }

        public Task<List<SubjectLookupDto>> GetListSubjectLookup()
        {
            var result = _dbContext.MonHocs.Select(item => new SubjectLookupDto()
            {
                Id = item.Id,
                Value = item.Name
            }).ToListAsync();
            return result;
        }

        /// <summary>
        /// return 1: thành công, 0: đã có đề thi sử dụng, không được xóa, -1: không tìm thấy id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public int DeleteQuestion(int questionId)
        {
            // đã có đề thi sử dụng câu hỏi thì không cho xóa
            var question = _dbContext.CauHois.Include(x => x.DeCauHois).SingleOrDefault(x => x.Id == questionId);
            if (question == null)
            {
                return -1;
            }

            if (question.DeCauHois != null && question.DeCauHois.Count > 0)
            {
                return 0;
            }
            
            _dbContext.Entry(question).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return 1;
        }

        public void EditQuestion(QuestionEditDto data)
        {
            var question = _dbContext.CauHois.FirstOrDefault(item => item.Id == data.Id);
            if (question != null)
            {
                question.Content = data.Question;
                question.MonHocId = data.MonHocId;
                _dbContext.CauHois.Update(question);
                
                //update đáp án
                var options = _dbContext.Options.Where(x => x.CauHoiId == data.Id).ToList();
                if (options.Count > 0)
                {
                    foreach (var option in data.Options)
                    {
                        var dbItemIndex = options.FindIndex(x => x.Id == option.Id);
                        if (dbItemIndex !=  -1)
                        {
                            var dbItem = options[dbItemIndex];
                            dbItem.Content = option.Value;
                        }
                        else
                        {
                            var newOption = new Option()
                            {
                                Content = option.Value,
                                CauHoiId = data.Id,
                                IsAnswer = false
                            };

                            _dbContext.Options.Add(newOption);
                        }
                    }
                }
                _dbContext.SaveChanges();
            }
        }
    }
}