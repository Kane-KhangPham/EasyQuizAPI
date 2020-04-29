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

        public Task<int> CreateMonHoc(MonHocDto data)
        {
            var monHoc = new MonHoc();
            monHoc.Id = 0;
            monHoc.Name = data.Name;
            //check trufng tên
            if (_dbContext.MonHocs.SingleOrDefault(x => x.Name.ToUpper().Equals(monHoc.Name.ToUpper())) != null)
            {
                return Task.FromResult(-1);
            }
            _dbContext.MonHocs.Add(monHoc);
            return _dbContext.SaveChangesAsync();
        }
        
        public Task<QuestionListReponse> GetListQuestion(ListQuestionPageDto data)
        {
            var result = new QuestionListReponse();
            var monhocId = data.MonHoc.GetValueOrDefault();
            var keyword = string.IsNullOrEmpty(data.Keyword) ? string.Empty : data.Keyword;
            var query = _dbContext.CauHois.Include(x => x.Options).Where(x => x.Content.Contains(keyword)).AsQueryable();
            if (monhocId > 0)
            {
                query = query.Where(x => x.MonHocId == monhocId);
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
                        Content = o.Content,
                        IsDapAn =  o. IsAnswer
                    }).ToList()
                }).ToList();
            return Task.FromResult(result);
        }

        public Task<MonHocResponseDto> GetListMonHoc(ListQuestionPageDto data)
        {
            var result = new MonHocResponseDto();
            var keyword = string.IsNullOrEmpty(data.Keyword) ? string.Empty : data.Keyword;
            var query = _dbContext.MonHocs.Where(x => x.Name.Contains(keyword)).AsQueryable();

            result.TotalRow = query.Count();
            result.Data = query.Skip((data.Page - 1) * data.PageSize).Take(data.PageSize).Select(x =>
                new MonHocDto()
                {
                    Id = x.Id,
                    Name = x.Name
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
            var questionExist = _dbContext.DeCauHois.FirstOrDefault(x => x.CauHoiId == questionId);
            if (questionExist != null)
            {
                return -1;
            }

            var question = _dbContext.CauHois.Find(questionId);
            if (question == null) return 0;
            _dbContext.Entry(question).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return 1;
        }

        public int DeleteMonHoc(int monHocId)
        {
            var exist = _dbContext.Des.SingleOrDefault(x => x.MonHocId == monHocId);
            if (exist != null)
            {
                // khoong the xoa
                return -1;
            }

            var monhoc = _dbContext.MonHocs.SingleOrDefault(x => x.Id == monHocId);
            if (monhoc == null)
            {
                return 0;
            }
            
            _dbContext.Entry(monhoc).State = EntityState.Deleted;
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
                            dbItem.IsAnswer = option.IsDapAn;
                        }
                        else
                        {
                            var newOption = new Option()
                            {
                                Content = option.Value,
                                CauHoiId = data.Id,
                                IsAnswer = option.IsDapAn
                            };

                            _dbContext.Options.Add(newOption);
                        }
                    }
                }
                _dbContext.SaveChanges();
            }
        }
        
        
        public void EditMonHoc(MonHocDto data)
        {
            var monHoc = _dbContext.MonHocs.FirstOrDefault(item => item.Id == data.Id);
            if (monHoc != null)
            {
                monHoc.Name = data.Name;
                _dbContext.MonHocs.Update(monHoc);
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<ObjectReference>> GetListKhoa()
        {
            var result = await _dbContext.Khoas.Select(khoa => new ObjectReference()
            {
                Id = khoa.Id,
                Value = khoa.Name
            }).ToListAsync();
            return result;
        }
        
        public async Task<GiaoVienListResponseDto> GetListGiaoVien(ListGiaoVienPageDto data)
        {
            var result = new GiaoVienListResponseDto();
            var keyword = string.IsNullOrEmpty(data.Keyword) ? string.Empty : data.Keyword;
            var query = _dbContext.GiaoViens.Where(x => x.Name.Contains(keyword)).AsQueryable();
            if (data.KhoaId.HasValue && data.KhoaId.Value > 0)
            {
                query = query.Where(x => x.KhoaId == data.KhoaId.Value);
            }

            result.TotalRow = query.Count();
            result.Data = await (from giaovien in query
                join khoa in _dbContext.Khoas on giaovien.KhoaId equals khoa.Id
                select new GiaoVienDto()
                {
                    Id = giaovien.Id,
                    Name = giaovien.Name,
                    KhoaId = giaovien.KhoaId,
                    KhoaName = khoa.Name
                }).Skip((data.Page - 1) * data.PageSize).Take(data.PageSize).ToListAsync();
            return result;
        }
        
        public Task<int> CreateGiaoVien(GiaoVienInsertDto data)
        {
            var giaoVien = new GiaoVien();
            giaoVien.Id = 0;
            giaoVien.Name = data.Name;
            giaoVien.KhoaId = data.KhoaId;
            _dbContext.GiaoViens.Add(giaoVien);
            return _dbContext.SaveChangesAsync();
        }
        
        public void EditGiaoVien(GiaoVienInsertDto data)
        {
            var giaoVien = _dbContext.GiaoViens.FirstOrDefault(item => item.Id == data.Id);
            if (giaoVien != null)
            {
                giaoVien.Name = data.Name;
                giaoVien.KhoaId = data.KhoaId;
                _dbContext.GiaoViens.Update(giaoVien);
                _dbContext.SaveChanges();
            }
        }
        
        public int DeleteGiaoVien(int id)
        {
            var questionExist = _dbContext.GiaoVienMonHocs.FirstOrDefault(x => x.GiaoVienId == id);
            if (questionExist != null)
            {
                return -1;
            }

            var question = _dbContext.GiaoViens.Find(id);
            if (question == null) return 0;
            _dbContext.Entry(question).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return 1;
        }

        public async Task<int> CreateAccount(AccountCreateDto data)
        {
            var account = new Account();
            account.Id = 0;
            account.AccountName = data.AccountName;
            account.Password = data.Password;
            account.GiaoVienId = data.GiaoVienId;
            account.RoleId = 1;
            // check 1 giao vien chi co 1 account
            var giaovienAcc = _dbContext.Accounts.FirstOrDefault(acc => acc.GiaoVienId == account.GiaoVienId);
            if (giaovienAcc != null)
            {
                return 0;
            }
            // check trùng account
            var isExist = _dbContext.Accounts.FirstOrDefault(x => x.AccountName.ToUpper().Equals(data.AccountName.ToUpper()));
            if (isExist != null)
            {
                return -1;
            }
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return 1;
        }

        public async Task<AccountListResponseDto> GetListAccount(ListAccountPageDto data)
        {
            var result = new AccountListResponseDto();
            var keyword = string.IsNullOrEmpty(data.Keyword) ? string.Empty : data.Keyword;
            var query = _dbContext.Accounts.Where(x => x.AccountName.Contains(keyword)).AsQueryable();

            result.TotalRow = query.Count();
            result.Data = query.Skip((data.Page - 1) * data.PageSize).Take(data.PageSize).Select(x =>
                new AccountListDto()
                {
                    Id = x.Id,
                    AccountName = x.AccountName,
                    GiaoVienName = x.GiaoVien.Name
                }).ToList();
            return result;
        }
    }
}