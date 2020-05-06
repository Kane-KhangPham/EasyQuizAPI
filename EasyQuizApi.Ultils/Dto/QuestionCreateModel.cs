namespace EasyQuizApi.Share.Dto
{
    public class QuestionCreateModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int MonHocId { get; set; }
        public string DapAn { get; set; }
    }

    public class QuestionUploadDto
    {
        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int MonHocId { get; set; }
        public string DapAn { get; set; }
        public string MonHoc { get; set; }
    }
}