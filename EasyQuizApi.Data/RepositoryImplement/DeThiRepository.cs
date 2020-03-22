using EasyQuizApi.Data.RepositoryBase;
using EasyQuizApi.Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyQuizApi.Model.Entities;
using Newtonsoft.Json;

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
            return Task.FromResult(data);
        }

        private DeThi CreateRandomDeThi(int deId, int stt, IEnumerable<QuestionListItemDto> cauHois)
        {
            var deThi = new DeThi();
            deThi.DeId = deId;
            deThi.SoDe = stt.ToString();
            var listCauHoiId = cauHois.Select(x => x.Id).ToArray();
            // đề thứ 1 giống đề gốc
            if (stt > 1)
            {
                Random rand = new Random();
                for (var i = 0; i < listCauHoiId.Length - 1; i++)
                {
                    var j = rand.Next(i, listCauHoiId.Length);
                    var temp = listCauHoiId[i];
                    listCauHoiId[i] = listCauHoiId[j];
                    listCauHoiId[j] = temp;
                }
            }
            deThi.ThuTuCauHoi = JsonConvert.SerializeObject(listCauHoiId);
            return deThi;
        }
        
        public int CreateDeThi(DeThiNewDto data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())  
            { 
                try
                {
                    var de = new De()
                    {
                        Id = data.Id,
                        MonHocId = data.MonHocId,
                        SoCau = data.SoCau,
                        ThoiGian = data.ThoiGian,
                        Status = data.Status,
                        KyThiId = data.KyThiId,
                        NgayThi = data.NgayThi,
                        LopId = data.LopId
                    };
                    _dbContext.Des.Add(de);
                    _dbContext.SaveChanges();
                    var createdId = de.Id;

                    if (createdId > 0)
                    {
                        // thêm vào bảng giáo viên tạo đề thi
                        var giaoVienTaoDe = new SoanDe()
                        {
                            GiaoVienId = data.GiaoVienId,
                            DeId = createdId
                        };
                        _dbContext.SoanDes.Add(giaoVienTaoDe);
                        
                        // them de cau hoi
                        if (data.CauHois != null && data.CauHois.Count > 0)
                        {
                            data.CauHois.ForEach(cauHoi =>
                            {
                                var deCauHoi = new DeCauHoi()
                                {
                                    DeId = createdId,
                                    CauHoiId = cauHoi.Id
                                };
                                _dbContext.DeCauHois.Add(deCauHoi);
                            });
                        }
                        
                        // Thêm các đề thi trộn câu hỏi dựa vào số đề muốn tự sinh
                        for (int stt = 1; stt <= data.SoLuongDeTuSinh; stt++)
                        {
                            var deThi = CreateRandomDeThi(createdId, stt, data.CauHois);
                            _dbContext.DeThis.Add(deThi);
                        }

                        _dbContext.SaveChanges();
                    }

                    if (createdId <= 0)
                    {
                        transaction.Rollback();
                        return -1;
                    }
                    transaction.Commit();
                    return createdId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return -1;
                }
            }
        }
    }
}
