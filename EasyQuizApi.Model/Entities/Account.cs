using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyQuizApi.Model.Entities
{
    public class Account : IEntityBase
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        public int? GiaoVienId { get; set; }
        public GiaoVien GiaoVien { get; set; }
        public Role Role { get; set; }
    }
}
