using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiggy.Core.IRepository;
using Swiggy.Data;
using Swiggy.Models;

namespace Swiggy.Core.Repository
{
    public class UserRepository :IUserRepository
    {
        private readonly SwiggyDbContext dbContext;

        public UserRepository(SwiggyDbContext swiggyDbContext)
        {
            dbContext = swiggyDbContext;
        }
        public async Task<UserModel> Register(AddUserModel user)
        {
            try
            {
                //var result = await dbContext.Users.FindAsync(user.UserId);
                //if (result != null)
                //{
                //    return null;
                //}
                var users = new UserModel()
                {
                    UserId = Guid.NewGuid(),
                    Name = user.Name,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Password = user.Password,
                    UserName = user.UserName

                };
                await dbContext.Users.AddAsync(users);
                 dbContext.SaveChanges();
                return users;
            }
            catch(Exception)
            {
                throw ;
            }

        }
        public UserModel SignIn(SignInModel signInModel)
        {
            try
            {
                var login =  dbContext.Users.FirstOrDefault(x => x.Email == signInModel.Email && x.Password == signInModel.Password);
               
                    return login;
             
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        public async Task<List<UserModel>> GetUsers()
        {
            try
            {
                var users = await dbContext.Users.ToListAsync();
                
                return users;
            }
            catch(Exception e)
            {
                throw e;
            }

        }
        public async Task<UserModel> UpdateUser(Guid id,AddUserModel addUserModel)
        {
            try
            {
                var user = await dbContext.Users.FindAsync(id);
                if (user != null)
                {
                    user.UserId = id;
                    user.Name = addUserModel.Name;
                    user.UserName = addUserModel.UserName;
                    user.Email = addUserModel.Email;
                    user.Mobile = addUserModel.Mobile;
                    user.Password = addUserModel.Password;
                    await dbContext.SaveChangesAsync();
                    return user;

                }
                return null;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public async Task<UserModel> DeleteUser(Guid id)
        {
            try
            {
                var user = await dbContext.Users.FindAsync(id);
                if (user == null)
                    return null;
                dbContext.Users.Remove(user);
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
