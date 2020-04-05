using EasyQuizApi.Data.RepositoryBase;
using EasyQuizApi.Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyQuizApi.Model.Entities;
using Microsoft.EntityFrameworkCore;
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

        private List<QuestionListItemDto> SaoTronCauHoi(List<QuestionListItemDto> cauHois)
        {
            var result = cauHois.Select(x => x).ToList();
            Random rand = new Random();
            for (var i = 0; i < result.Count - 1; i++)
            {
                var j = rand.Next(i, result.Count);
                var temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return result;
        }

        public DeThiDetailDto GetDeThiDetail(int id)
        {
            var result = new DeThiDetailDto();
            var de = _dbContext.Des.Include(d => d.Lop)
                .Include(d => d.KyThi)
                .Include(d => d.MonHoc)
                .SingleOrDefault(x => x.Id == id);
            if (de != null)
            {
                result.Id = de.Id;
                if (de.LopId.HasValue)
                {
                    result.LopHoc = new ObjectReference()
                    {
                        Id = de.LopId.Value,
                        Value = de.Lop.Name
                    };
                }
                result.KyThi = new ObjectReference()
                {
                    Id = de.KyThiId,
                    Value = de.KyThi.Name
                };
                result.MonHoc = new ObjectReference()
                {
                    Id = de.MonHocId.Value,
                    Value = de.MonHoc.Name
                };
                result.NgayThi = de.NgayThi;
                result.ThoiGian = de.ThoiGian;
                result.SoCau = de.SoCau;
                result.GhiChu = de.GhiChu;
                result.KieuDanTrang = de.KieuDanTrang;
                result.SoLuongDe = _dbContext.Des.Count(x => x.RootDeId == id) + 1;
                result.CauHoi = _dbContext.DeCauHois.Where(x => x.DeId == id).Select(d => new QuestionListItemDto()
                {
                    Id = d.CauHoiId,
                    Content = d.CauHoi.Content,
                    MonHoc = d.CauHoi.MonHoc.Name,
                    Options = d.CauHoi.Options.Select(option => new OptionDto()
                    {
                        Id = option.Id,
                        Content = option.Content,
                        IsDapAn = option.IsAnswer
                    }).ToList()
                }).ToList();
            }

            return result;
        }

        private int CreateDeThiIml(DeThiNewDto data, int rootId = 0)
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
                LopId = data.LopId,
                SoDe = data.SoDe.HasValue ? data.SoDe.Value : 1,
                GhiChu = data.GhiChu,
                KieuDanTrang = (int)data.KieuDanTrang
            };
            if (rootId > 0)
            {
                de.RootDeId = rootId;
            }
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
                _dbContext.SaveChanges();
            }

            return createdId;
        }
        
        public int CreateDeThi(DeThiNewDto data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())  
            { 
                try
                {
                    bool flag = true;
                    int createdRootId = CreateDeThiIml(data);
                    if (createdRootId > 0)
                    {
                        for (int i = 2; i <= data.SoLuongDeTuSinh; i++)
                        {
                            data.SoDe = i;
                            data.CauHois = SaoTronCauHoi(data.CauHois);
                            int createdId = CreateDeThiIml(data, createdRootId);
                            if (createdId < 0)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        flag = false;
                    }

                    if (!flag)
                    {
                        transaction.Rollback();
                        return -1;
                    }
                    transaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return -1;
                }
            }
        }
        public int UpdateDeThi(DeThiNewDto data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())  
            {
                try
                {
                    var de = _dbContext.Des.Find(data.Id);
                    if (de == null)
                    {
                        return -1;  // khoong tim thay id
                    }

                    var isDuplicate = _dbContext.Des.FirstOrDefault(x =>
                        x.Id != data.Id && x.KyThiId == data.KyThiId && x.MonHocId == data.MonHocId &&
                        x.LopId.HasValue && x.LopId.HasValue && x.LopId == data.LopId);

                    if (isDuplicate != null)
                    {
                        // trung de thi
                        return -2;
                    }

                    de.Id = data.Id;
                    de.MonHocId = data.MonHocId;
                    de.SoCau = data.SoCau;
                    de.ThoiGian = data.ThoiGian;
                    de.Status = data.Status;
                    de.KyThiId = data.KyThiId;
                    de.NgayThi = data.NgayThi;
                    de.LopId = data.LopId;
                    // de.SoDe = data.SoDe.HasValue ? data.SoDe.Value : 1;     ---- không update trường này
                    de.GhiChu = data.GhiChu;
                    de.KieuDanTrang = (int) data.KieuDanTrang;
                    
                    _dbContext.SaveChanges();
                    
                    // them de cau hoi
                    if (data.CauHois != null && data.CauHois.Count > 0)
                    {
                        var oldListQuestion = _dbContext.DeCauHois.Where(x => x.DeId == data.Id).Select(x => x.CauHoiId).ToList();
                        var newListQuestion = data.CauHois.Select(x => x.Id).ToList();
                        var listSame = new List<int>();
                        oldListQuestion.ForEach(x =>
                        {
                            if (newListQuestion.IndexOf(x) >= 0)
                            {
                                listSame.Add(x);
                            }
                        });
                        oldListQuestion.RemoveAll(i => listSame.IndexOf(i) >= 0);
                        newListQuestion.RemoveAll(i => listSame.IndexOf(i) >= 0);
                        var listRemoveEntity = _dbContext.DeCauHois.Where(x => x.DeId == data.Id && oldListQuestion.IndexOf(x.CauHoiId) >= 0).ToList();
                        listRemoveEntity.ForEach(x => { _dbContext.DeCauHois.Remove(x); });
                        _dbContext.SaveChanges();
                        newListQuestion.ForEach(x =>
                        {
                            var entity = new DeCauHoi()
                            {
                                DeId = data.Id,
                                CauHoiId = x
                            };
                            _dbContext.DeCauHois.Add(entity);
                        });
                        _dbContext.SaveChanges();
                    }

                    transaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return -1;
                }
            }
        }

        public DeThiListResponseDto GetListDeThi(DeThiFilterDto data)
        {
            var result = new DeThiListResponseDto();
            var query = _dbContext.Des.AsQueryable();
            if (data.MonHocId > 0)
            {
                query = query.Where(x => x.MonHocId == data.MonHocId);
            }

            if (data.KyThiId > 0)
            {
                query = query.Where(x => x.KyThiId == data.KyThiId && x.NgayThi.Year == data.NamKyThi);
            }
            if (data.LopHocId > 0)
            {
                query = query.Where(x => x.LopId == data.LopHocId);
            }
            result.TotalRow = query.Count();
            result.Data = query.Skip((data.Page - 1) * data.PageSize).Take(data.PageSize).Select(x =>
                new DeThiListDto(){
                    DeThiId = x.Id,
                    KyThi = $"{x.KyThi.Name}, {x.NgayThi.Year}",
                    LopThi = x.Lop.Name,
                    MonHoc = x.MonHoc.Name,
                    SoDe = x.SoDe.ToString(),
                    DeThiRootId = x.RootDeId.HasValue ? x.RootDeId.Value : x.Id
                }).ToList();
            return result;
        }
    }
}
