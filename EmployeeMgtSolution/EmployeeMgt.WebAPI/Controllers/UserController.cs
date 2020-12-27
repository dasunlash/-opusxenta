using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmployeeMgt.CORE.Entities;
using EmployeeMgt.CORE.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeMgt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBLL _userBLL;
        private readonly IConfiguration _config;

        public UserController(IUserBLL userBLL, IConfiguration config)
        {
            _userBLL = userBLL;
            _config = config;

        }
        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await _userBLL.GetUsers());
        }
        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            user.CreatedUser = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
            return Ok(await _userBLL.AddUser(user));
        }
        // GET: api/User/1
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _userBLL.GetUsers(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/User or PUT: api/User/5
        [HttpPut]
        public async Task<ActionResult<User>> UpdateStudent([FromBody] User user)
        {
            user.ModifiedUser = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
            var userObj = await _userBLL.UpdateUser(user);
            if (userObj == null)
            {
                return StatusCode(500, $"Unable to update user with id {user.Id}");
            }
            return Ok(userObj);

        }
        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteStudent(int id)
        {
            var isUserDeleted = await _userBLL.DeleteUser(id);
            if (!isUserDeleted)
            {
                return StatusCode(500, $"Unable to delete user with id {id}");
            }
            return Ok(isUserDeleted);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginViewModel user)
        {

            var userObj = await _userBLL.ValidateUser(user.Email,user.Password);
            if (userObj!=null && userObj.Id > 0)
            {
                var Token = GenerateJSONWebToken(userObj);
                return Ok(new { Token = Token, userObj = new { userObj.Id, userObj.FirstName, userObj.LastName, userObj.SecurityProfileId, userObj.Email,userObj.SecurityProfile } });
            }
            else
            {
                return StatusCode(401, $"Invalid login credentials");
            }

        }
        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("UserId",user.Id.ToString()),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(365),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
