using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Teeq_Data;
using Teeq_Data.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Teeq_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ApplicationDbContext _context;

        public UsersController(ILogger<UsersController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("~/Users/Hello")]
        public string Hello()
        {
            return "Hello world";
        }

        [HttpGet("~/Users")]
        public async Task<List<User>> Get()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        [Authorize]
        [HttpGet("~/Users/AuthUser")]
        public async Task<ActionResult> AuthUser()
	    {
            var uid = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var authUser = await _context.Users
                    .FirstOrDefaultAsync(m => m.FirebaseId == uid);

            return Ok(authUser);
        }

        [HttpGet("~/Users/{id}")]
        public async Task<User> Details(Guid? id)
        {
            if (id == null)
            {
                return null;
	        }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return null;
		    }
            return user;
        }

        [HttpGet("~/Users/UsernameAvailable/{username}")]
        public async Task<bool> UsernameAvailable(string username)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Username == username);

            if (user == null)
            {
                return true;
            }
            return false;
        }

        public class UserPostData
        { 
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string username { get; set; }
            public string firebaseId { get; set; }
        }

        [HttpPost("~/Users")]
        public async Task<User> Post(UserPostData userData)
        {
            _logger.Log(LogLevel.Debug, "creating user");

            var newUser = new User();
            newUser.Id = Guid.NewGuid();
            newUser.FirstName = userData.firstName;
            newUser.LastName = userData.lastName;
            newUser.Username = userData.username;
            newUser.FirebaseId = userData.firebaseId;

            _context.Add(newUser);

            await _context.SaveChangesAsync();

            return newUser;
	    }

        public class UserPutData
        { 
            public string? firstName { get; set; }
            public string? lastName { get; set; }
            public string? username { get; set; }
        }

        [HttpPut("~/Users/{id}")]
        public async Task<User> Put(Guid id, UserPutData userData)
        {
            _logger.Log(LogLevel.Debug, "updating user");

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (!String.IsNullOrEmpty(userData.firstName))
            {
                user.FirstName = userData.firstName;
	        }
            if (!String.IsNullOrEmpty(userData.lastName))
            {
                user.LastName = userData.lastName;
	        }
            if (!String.IsNullOrEmpty(userData.username))
            {
                user.Username = userData.username;
	        }

            _context.Update(user);

            await _context.SaveChangesAsync();

            return user;
	    }

        [HttpDelete("~/Users/{id}")]
        public async Task<User> Delete(Guid id)
        {
            _logger.Log(LogLevel.Debug, "deleting user");

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            _context.Remove(user);

            await _context.SaveChangesAsync();

            return user;
	    }
    }
}
