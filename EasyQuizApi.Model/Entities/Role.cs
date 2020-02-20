using System;
using System.Collections.Generic;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class Role : IEntityBase
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string RoleName { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
