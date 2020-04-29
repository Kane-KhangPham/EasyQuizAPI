using EasyQuizApi.Data.RepositoryBase;
using EasyQuizApi.Share;
using EasyQuizApi.Share.Dto;
using EasyQuizApi.Share.Helper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EasyQuizApi.Data.RepositoryImplement
{
    public class UserRepository : IUserRepository
    {
        private readonly EasyQuizDbContext _dbContext;
        private readonly AppSettings _appSetting;

        public UserRepository(IOptions<AppSettings> appSettings, EasyQuizDbContext dbContext)
        {
            _appSetting = appSettings.Value;
            _dbContext = dbContext;
        }

        public Task<int> AddUser(UserLoginResponse user)
        {
            throw new NotImplementedException();
        }

        public UserLoginResponse Authenticate(string userName, string password)
        {
            string passwordHash = PasswordHelper.HashPassword(password);
            var user = _dbContext.Accounts.SingleOrDefault(u => u.AccountName == userName && u.Password == passwordHash);
            // Username and Passord incorrect
            if(user == null)
            {
                return null;
            }

            //authent success, gen jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var userResponse = new UserLoginResponse();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userResponse.Id = user.Id;
            userResponse.AccountName = user.AccountName;
            userResponse.FullName = user.GiaoVien != null ? user.GiaoVien.Name : "";
            userResponse.Token = tokenHandler.WriteToken(token);
            return userResponse;
        }

        public bool ChangePassword(ChangePasswordDto data)
        {
            data.NewPass = PasswordHelper.HashPassword(data.NewPass);
            var account = _dbContext.Accounts.FirstOrDefault(acc =>
                acc.AccountName == data.AccountName && acc.Password == PasswordHelper.HashPassword(data.OldPass));
            if (account != null)
            {
                account.Password = data.NewPass;
                _dbContext.Accounts.Update(account);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
