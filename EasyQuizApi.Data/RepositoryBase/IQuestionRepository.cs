using System.Collections.Generic;
using System.Threading.Tasks;
using EasyQuizApi.Share.Dto;

namespace EasyQuizApi.Data.RepositoryBase
{
    public interface IQuestionRepository
    {
        Task<int> CreateQuestion(QuestionCreateModel data);
        Task<int> CreateMonHoc(MonHocDto data);
        Task<QuestionListReponse> GetListQuestion(ListQuestionPageDto data);
        Task<MonHocResponseDto> GetListMonHoc(ListQuestionPageDto data);
        Task<List<SubjectLookupDto>> GetListSubjectLookup();

        int DeleteQuestion(int questionId);
        int DeleteMonHoc(int monHocId);
        void EditQuestion(QuestionEditDto data);
        void EditMonHoc(MonHocDto data);
        Task<List<ObjectReference>> GetListKhoa();
        Task<GiaoVienListResponseDto> GetListGiaoVien(ListGiaoVienPageDto data);
        Task<int> CreateGiaoVien(GiaoVienInsertDto data);
        void EditGiaoVien(GiaoVienInsertDto data);
        int DeleteGiaoVien(int id);

        Task<int> CreateAccount(AccountCreateDto data);
        Task<AccountListResponseDto> GetListAccount(ListAccountPageDto data);
    }
}