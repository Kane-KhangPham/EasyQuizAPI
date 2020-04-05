using System;
using System.Collections.Generic;
using EasyQuizApi.Model;
using EasyQuizApi.Share.Enums;

namespace EasyQuizApi.Share.Dto
{
    public class DeThiNewDto
    {
        public int Id { get; set; }
        public int MonHocId { get; set; }
        public int SoCau { get; set; }
        public int ThoiGian { get; set; }
        public Status Status { get; set; }
        public int KyThiId { get; set; }
        public int? LopId { get; set; }
        public DateTime NgayThi { get; set; }
        public int GiaoVienId { get; set; }
        public int SoLuongDeTuSinh { get; set; }
        public string GhiChu { get; set; }
        public List<QuestionListItemDto> CauHois { get; set; }
        public KieuDanTrang KieuDanTrang { get; set; }
        public int? SoDe { get; set; }
    }

    public class DeThiPdfDto
    {
        public string HocPhan { get; set; }
        public string KyThi { get; set; }
        public string LopThi { get; set; }
        public int ThoiGianThi { get; set; }
        public DateTime NgayThi { get; set; }
        public int SoDe { get; set; }
        public List<QuestionListItemDto> CauHois { get; set; }
        public KieuDanTrang KieuDanTrang { get; set; }
        public bool IsContainDapAn { get; set; }
    }

    public class DeThiDetailDto
    {
        public int Id { get; set; }
        public ObjectReference LopHoc { get; set; }
        public ObjectReference KyThi { get; set; }
        public ObjectReference MonHoc { get; set; }
        public DateTime NgayThi { get; set; }
        public int ThoiGian { get; set; }
        public int SoCau { get; set; }
        public int SoLuongDe { get; set; }
        public List<QuestionListItemDto> CauHoi { get; set; }
        public string GhiChu { get; set; }
        public int KieuDanTrang { get; set; }
    }
}