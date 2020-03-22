using System.Collections.Generic;

namespace EasyQuizApi.Share.Dto
{
    public class QuestionListItemDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string MonHoc { get; set; }
        public List<OptionDto> Options { get; set; }
        public string DapAn { get; set; }
    }
}