using EasyQuizApi.Model.Entities;
using EasyQuizApi.Share.Helper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EasyQuizApi.Data
{
    public class DbInitializer
    {
        private static EasyQuizDbContext context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EasyQuizDbContext>();
                InitializeSchedules(context);
            }
        }

        private static void InitializeSchedules(EasyQuizDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new Role[]{
                    new Role()
                    {
                        RoleName = "Admin",
                        Code = "admin"
                    }
                });
                context.SaveChanges();
            }
            if (!context.Accounts.Any())
            {
                Account saAccount = new Account
                {
                    AccountName = "admin",
                    Password = PasswordHelper.HashPassword("123qwe"),
                    RoleId = 1   // Admin
                };
                context.Accounts.Add(saAccount);
                context.SaveChanges();
            }
        }
    }
}
