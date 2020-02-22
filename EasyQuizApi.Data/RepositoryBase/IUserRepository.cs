using EasyQuizApi.Share.Dto;
using System.Threading.Tasks;

namespace EasyQuizApi.Data.RepositoryBase
{
    public interface IUserRepository
    {
        UserLoginResponse Authenticate(string userName, string password);
        Task<int> AddUser(UserLoginResponse user);
    }
}
