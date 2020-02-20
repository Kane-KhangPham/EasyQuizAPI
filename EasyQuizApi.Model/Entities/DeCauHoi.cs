using System;
using System.Collections.Generic;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class DeCauHoi
    {
        public int DeId { get; set; }

        public int CauHoiId { get; set; }

        public CauHoi CauHoi { get; set; }
        public De De { get; set; }
    }
}
