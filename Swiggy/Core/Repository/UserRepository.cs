using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiggy.Core.IRepository;
using Swiggy.Data;
using Swiggy.Models;
using System.Security.Claims;

namespace Swiggy.Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SwiggyDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        //private SwiggyDbContext cox;
        //public UserRepository(SwiggyDbContext cox)
        //{
        //    this.cox = cox;
        //}

        public UserRepository(SwiggyDbContext swiggyDbContext, IHttpContextAccessor _httpContextAccessor)
        {
            dbContext = swiggyDbContext;
            httpContextAccessor = _httpContextAccessor;
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
                    UserName = user.UserName,
                  //  Role = user.Role,
                };
                await dbContext.Users.AddAsync(users);
                await dbContext.SaveChangesAsync();
                return users;
            }
            catch(Exception)
            {
                throw ;
            }

        }
        public async Task<UserModel> SignIn(SignInModel signInModel)
        {
            try
            {
                var login =  dbContext.Users.Where(x => x.Email == signInModel.Email && x.Password == signInModel.Password).FirstOrDefault();
               if(login == null)
                {
                    throw new Exception("Invalid login details");
                }
                var claims = new List<Claim>
               {
                   new Claim(ClaimTypes.NameIdentifier, login.Email),
                  // new Claim(ClaimTypes.Role, login.Role)
               };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                    return login;
             
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<List<UserModel>> GetUsers()
        {
            try
            {
                var users = await dbContext.Users.ToListAsync();
                
                return users;
            }
            catch(Exception )
            {
                throw new Exception();
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
                throw new Exception();
            }
            catch(Exception )
            {
                throw new Exception();
            }

        }

        public async Task<UserModel> DeleteUser(Guid id)
        {
            try
            {
                var user = await dbContext.Users.FindAsync(id);
                if (user == null)
                    throw new Exception();
                dbContext.Users.Remove(user);
                return user;
            }
            catch (Exception )
            {
                throw new Exception();
            }
        }
    }
}
