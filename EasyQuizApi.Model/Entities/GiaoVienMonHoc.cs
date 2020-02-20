using System;
using System.Collections.Generic;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class GiaoVienMonHoc
    {
        public int GiaoVienId { get; set; }
        public int MonHocId { get; set; }

        public MonHoc MonHoc { get; set; }
        public GiaoVien GiaoVien { get; set; }
    }
}
