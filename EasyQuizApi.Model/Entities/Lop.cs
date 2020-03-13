using System;
using System.Collections.Generic;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class Lop : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool DeletedStatus { get; set; }

    }
}
