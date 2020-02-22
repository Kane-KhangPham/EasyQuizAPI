using System;
using System.Collections.Generic;
using System.Text;

namespace EasyQuizApi.Share.Dto
{
    public class UserLoginResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}
