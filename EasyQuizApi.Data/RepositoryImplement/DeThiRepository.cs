using EasyQuizApi.Data.RepositoryBase;
using EasyQuizApi.Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyQuizApi.Data.RepositoryImplement
{
    public class DeThiRepository : IDeThiRepository
    {
        private readonly EasyQuizDbContext _dbContext;
        public DeThiRepository(EasyQuizDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<ObjectReference>> GetListKiThi()
        {
            var data = _dbContext.KiThis.Select(x => new ObjectReference()
            {
                Value = x.Name,
                Id = x.Id
            }).ToList();
            return Task.FromResult(data);
        }

        public Task<List<ObjectReference>> GetListMonHoc(string filter)
        {
            var data = _dbContext.MonHocs.Where(x => x.Name.Contains(filter)).Select(x => new ObjectReference()
            {
                Value = x.Name,
                Id = x.Id
            }).ToList();
            return Task.FromResult(data);
        }

        public Task<List<ObjectReference>> GetListLopHoc()
        {
            var data = _dbContext.Lops.Select(x => new ObjectReference()
            {
                Value = x.Name,
                Id = x.Id
            }).ToList();
            return Task.FromResult(new List<ObjectReference>());
        }
    }
}
