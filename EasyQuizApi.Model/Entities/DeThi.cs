using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class DeThi : IEntityBase
    {
        public int Id { get; set; }
        public int DeId { get; set; }
        public string SoDe { get; set; }
        public string FilePath { get; set; }

        [Column("ThuTuCauHoi", TypeName = "ntext")]
        public string ThuTuCauHoi { get; set; }
        public De DeGoc { get; set; }
    }
}
