using System.Collections.Generic;

namespace EasyQuizApi.Share.Dto
{
    public class GiaoVienDto : GiaoVienInsertDto
    {
        public string KhoaName { get; set; }
    }
    
    public class GiaoVienInsertDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int KhoaId { get; set; }
    } 
    
    public class ListGiaoVienPageDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? KhoaId { get; set; }
        public string Keyword { get; set; }
    }
    
    public class GiaoVienListResponseDto
    {
        public int TotalRow { get; set; }
        public List<GiaoVienDto> Data { get; set; }
    }
}