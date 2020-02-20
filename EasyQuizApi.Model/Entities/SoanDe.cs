using System;
using System.Collections.Generic;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class SoanDe
    {
        public int GiaoVienId { get; set; }

        public int DeId { get; set; }
        public DateTime LastModified { get; set; }

        public GiaoVien GiaoVien { get; set; }
        public De De { get; set; }
    }
}
