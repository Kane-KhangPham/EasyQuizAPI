﻿using System;
using System.Collections.Generic;


namespace EasyQuizApi.Model.Entities
{
    public class De : IEntityBase
    {
        public int Id { get; set; }
        public int? MonHocId { get; set; }
        public int SoCau { get; set; }
        public int ThoiGian { get; set; }
        public Status Status { get; set; }
        public int KyThiId { get; set; }
        public DateTime NgayThi { get; set; }
        public int SoDe { get; set; }
        public int? RootDeId { get; set; }

        public int? LopId { get; set; }
        public string GhiChu { get; set; }
        public int KieuDanTrang { get; set; }

        public virtual Lop Lop { get; set; }

        public virtual KyThi KyThi { get; set; }
        public virtual MonHoc MonHoc { get; set; }
        public virtual ICollection<SoanDe> SoanDes { get; set; }
    }
}
