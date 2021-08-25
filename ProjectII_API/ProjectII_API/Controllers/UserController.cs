using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Project2.Common.Entity;
using Project2.DataAccess;
using ProjectII_API.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Project2.Common.Properties;
using ProjectII_API.Service;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

namespace ProjectII_API.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        UserService _service;
        Respone _res;
        public UserController(IConfiguration config)
        {
            _config = config;
            _service = new UserService(_config);
            _res = new Respone();
        }
        #region API Methods
        [HttpGet]
        public IActionResult GetAllUser()
        {
            try
            {
                UserContext userContext = new UserContext();
                var user = userContext.GetAll();
                if (user != null)
                    return Ok(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [Authorize(Roles ="Quản trị viên")]
        [HttpPost]
        public IActionResult Post(User user)
        {
            try
            {
                UserContext userContext = new UserContext();
                var res = userContext.Insert(user);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(User user, Guid id)
        {
            try
            {
                UserContext userContext = new UserContext();
                var res = userContext.Update(user);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var userContext = new UserContext();
            try
            {
                var user = userContext.GetById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [Authorize(Roles = "Quản trị viên")]
        [HttpDelete("{id}")]
        public IActionResult DeleteVaccine(Guid id)
        {
            var userContext = new UserContext();
            try
            {
                userContext.DeleteById(id);
                return Ok(Project2.Common.Properties.Resources.DeleteSuccesful);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [HttpGet("Login")]  
        public IActionResult Login(string username, string password)
        {
            User login = new User();
            login.Account = username;
            login.Password = password;
            IActionResult response = Unauthorized();
            var user = _service.AuthenticateUser(login);
            if (user != null)
            {
                var tokenStr = _service.GenerateJSONWebToken(user);
                response = Ok(tokenStr);
            }
            return response;
        }
        [Authorize]
        [HttpGet("LoginRole")]
        public IActionResult GetRole()  
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var role = claim[2].Value;
            var id = claim[1].Value;
            var fullname = claim[0].Value;
            var respone = new { Role= role, Id= id, FullName= fullname};
            return Ok(respone);
        }
        [Authorize]
        [HttpGet("GetNewUID")]
        public Guid Get()
        {
            return Guid.NewGuid();
        }
        #endregion
    }
}
