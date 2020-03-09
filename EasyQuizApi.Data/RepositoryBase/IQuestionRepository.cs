using System.Collections.Generic;
using System.Threading.Tasks;
using EasyQuizApi.Share.Dto;

namespace EasyQuizApi.Data.RepositoryBase
{
    public interface IQuestionRepository
    {
        Task<int> CreateQuestion(QuestionCreateModel data);
        Task<QuestionListReponse> GetListQuestion(ListQuestionPageDto data);
        Task<List<SubjectLookupDto>> GetListSubjectLookup();

        int DeleteQuestion(int questionId);
        void EditQuestion(QuestionEditDto data);
    }
}