using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.Common.Entity;
using Project2.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectII_API.Controllers
{
    [Route("api/v1/Authority")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        Respone _res;
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var authorityContext = new AuthorityContext();
                var authorities = authorityContext.GetAll();
                return Ok(authorities);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
    }
}
