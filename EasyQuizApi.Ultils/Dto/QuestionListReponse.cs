using System.Collections.Generic;

namespace EasyQuizApi.Share.Dto
{
    public class QuestionListReponse
    {
        public int TotalRow { get; set; }
        public List<QuestionListItemDto> Data { get; set; }
    }
}