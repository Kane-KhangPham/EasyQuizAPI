﻿using System;
using System.Collections.Generic;


namespace EasyQuizApi.Model.Entities
{
    public class De : IEntityBase
    {
        public int Id { get; set; }
        public int MonHocId { get; set; }
        public int SoCau { get; set; }
        public int ThoiGian { get; set; }
        public Status Status { get; set; }
        public int KyThiId { get; set; }
        public DateTime NgayThi { get; set; }

        public int LopId { get; set; }

        public Lop Lop { get; set; }

        public KyThi KyThi { get; set; }
        public MonHoc MonHoc { get; set; }
        public ICollection<SoanDe> SoanDes { get; set; }
        public ICollection<DeThi> DeThi { get; set; }
        public ICollection<DeCauHoi> DeCauHois { get; set; }
    }
}
