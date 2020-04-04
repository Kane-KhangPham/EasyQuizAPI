namespace EasyQuizApi.Share.Dto
{
    public class ListQuestionPageDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? MonHoc { get; set; }
        public string Keyword { get; set; }
    }
}