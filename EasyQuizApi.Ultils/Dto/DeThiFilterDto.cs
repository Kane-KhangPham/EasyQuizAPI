namespace EasyQuizApi.Share.Dto
{
    public class DeThiFilterDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? KyThiId { get; set; }
        public int? NamKyThi { get; set; }
        public int? MonHocId { get; set; }
        public int? LopHocId { get; set; }
    }
}