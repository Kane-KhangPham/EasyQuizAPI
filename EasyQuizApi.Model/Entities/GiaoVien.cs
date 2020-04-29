using System;
using System.Collections.Generic;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class GiaoVien : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int KhoaId { get; set; }

        public Account Account { get; set; }
        public ICollection<GiaoVienMonHoc> GiaoVienMonHocs { get; set; }
        public ICollection<SoanDe> SoanDes { get; set; }
    }
}
