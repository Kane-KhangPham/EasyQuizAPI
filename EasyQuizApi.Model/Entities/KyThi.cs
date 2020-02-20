using System;
using System.Collections.Generic;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class KyThi : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public ICollection<DeThi> DeThi { get; set; }
    }
}
