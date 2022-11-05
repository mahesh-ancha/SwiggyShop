using Microsoft.AspNetCore.Mvc;
using Swiggy.Models;

namespace Swiggy.Core.IRepository
{
    public interface IUserRepository
    {
        Task<UserModel> Register(AddUserModel user);
        Task<UserModel> SignIn(SignInModel signInModel);
        Task<List<UserModel>> GetUsers();
        Task<UserModel> UpdateUser(Guid id, AddUserModel addUserModel);
        Task<UserModel> DeleteUser(Guid id);
    }
}
