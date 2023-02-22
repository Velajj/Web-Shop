using Core.Domain;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    public interface IUsersService
    {
        List<UserViewModel> GetAll();
        UserViewModel GetById(int id);
        UserViewModel GetUserByUserName(string userName);
        UserViewModel GetUserByEmail(string email);
        bool Insert(UserViewModel user);
        bool Delete(int id);
        bool Update(int userId, UserViewModel user);
        UserViewModel? Login(string userNameOrEmail, string password);
    }
}
