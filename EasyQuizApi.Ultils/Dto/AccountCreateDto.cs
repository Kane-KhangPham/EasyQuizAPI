using System.Collections.Generic;

namespace EasyQuizApi.Share.Dto
{
    public class AccountCreateDto
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public int GiaoVienId { get; set; }
        public string Email { get; set; }
    }
    
    public class ListAccountPageDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
    }

    public class AccountListDto
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string GiaoVienName { get; set; }
    }
    
    public class AccountListResponseDto
    {
        public int TotalRow { get; set; }
        public List<AccountListDto> Data { get; set; }
    } 
}