using System.Collections.Generic;

namespace EasyQuizApi.Share.Dto
{
    public class QuestionListItemDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int MonHocId { get; set; }
        public List<OptionDto> Options { get; set; }
    }
}