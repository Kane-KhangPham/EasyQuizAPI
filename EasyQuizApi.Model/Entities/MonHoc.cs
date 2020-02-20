using System;
using System.Collections.Generic;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class MonHoc : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CauHoi> DsCauHoi { get; set; }
        public ICollection<GiaoVienMonHoc> GiaoVienMonHocs { get; set; }
    }
}
