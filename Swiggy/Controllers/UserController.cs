using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiggy.Core.Repository;
using Swiggy.Data;
using Swiggy.Models;

namespace Swiggy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly SwiggyDbContext dbContext;

        public UserController(UserRepository _userRepository, SwiggyDbContext swiggyDbContext)
        {
            userRepository = _userRepository;
            dbContext = swiggyDbContext;
        }
        [HttpPost]
        [Route("Register")]
        public  IActionResult Register([FromBody] AddUserModel addUserModel)
        {
            try
            {
                //var user=await  dbContext.Users.FindAsync(addUserModel.Email);
                //if (user != null)
                //{
                //    return Ok(user);
                //}

                var result = userRepository.Register(addUserModel);
                //if (result == null)
                //    return Ok("Email Already Registered");
                return Ok(result);

            }
            catch(Exception e)
            {
                return NotFound(e);
            }
        }
        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignIn([FromBody] SignInModel signInModel)
        {
            try
            {
                var login = userRepository.SignIn(signInModel);
                if (login == null)
                    return NotFound("Invalid User Details");
                return Ok("Logged Successfully");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public Task<List<UserModel>> GetUsers()
        {
            try
            {
               /* var users =*/ return userRepository.GetUsers();
                //if (users == null)
                //    return NotFound();
                //return Ok(users);
            }
            catch(Exception )
            {
                return null;
            }
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateUser([FromRoute] Guid id,AddUserModel addUserModel)
        {
            try
            {
                var result = userRepository.UpdateUser(id, addUserModel);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            var result = userRepository.DeleteUser(id);
            if (result == null)
                return NotFound();
            return Ok(result + "Deleted Successfully");
        }
    }
}
