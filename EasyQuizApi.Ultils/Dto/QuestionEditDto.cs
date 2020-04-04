using System.Collections.Generic;

namespace EasyQuizApi.Share.Dto
{
    public class QuestionEditDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        
        public List<OptionEditDto> Options { get; set; }
        public int MonHocId { get; set; }
    }
}