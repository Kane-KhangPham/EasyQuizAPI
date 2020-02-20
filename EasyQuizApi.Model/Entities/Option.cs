using System;
using System.Collections.Generic;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class Option : IEntityBase
    {
        public int Id { get; set; }
        public int CauHoiId { get; set; }
        public string Content { get; set; }
        public bool IsAnswer { get; set; }

        public CauHoi CauHoi { get; set; }
    }
}
