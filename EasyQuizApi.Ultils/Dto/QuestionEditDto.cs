using System.Collections.Generic;

namespace EasyQuizApi.Share.Dto
{
    public class QuestionEditDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        
        public List<ObjectReference> Options { get; set; }
        public int MonHocId { get; set; }
    }
}