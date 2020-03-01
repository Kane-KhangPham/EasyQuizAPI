using System;
using System.Collections.Generic;

namespace EasyQuizApi.Model.Entities
{
    public class CauHoi : IEntityBase
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int MonHocId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int GiaoVienId { get; set; }
        public Status Status { get; set; }

        public GiaoVien GiaoVien { get; set; }
        public MonHoc MonHoc { get; set; }
        public ICollection<Option> Options { get; set; }
        public ICollection<DeCauHoi> DeCauHois { get; set; }
    }
}
