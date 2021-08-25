using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.Common.Entity;
using System;
using Project2.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project2.Common.Properties;
namespace ProjectII_API.Controllers
{
    [Route("api/v1/vaccine")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        Respone _res;
        #region API Methods
        [HttpGet]
        public IActionResult GetAllVaccine()
        {
            try
            {
                var vaccineContext = new VaccineContext();
                var vaccine = vaccineContext.GetAll();
                if (vaccine != null)
                    return Ok(vaccine);
                return NoContent();
            }
            catch (Exception ex)
            {
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                _res.DevMsg = ex.Message;
                return StatusCode(500, _res);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteVaccine(Guid id)
        {
            var vaccineContext = new VaccineContext();
            try
            {
                vaccineContext.DeleteById(id);
                return Ok(Project2.Common.Properties.Resources.DeleteSuccesful);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [HttpPost]
        public IActionResult Post(Vaccine vaccine)
        {
            try
            {
                VaccineContext vaccineContext = new VaccineContext();
                var res = vaccineContext.Insert(vaccine);
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
        public IActionResult Put(Vaccine vaccine, Guid id)
        {
            try
            {
                VaccineContext vaccineContext = new VaccineContext();
                var res = vaccineContext.Update(vaccine);
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
        public IActionResult GetVaccineById(Guid id)
        {
            var vaccineContext = new VaccineContext();
            try
            {
                var vaccine = vaccineContext.GetById(id);
                return Ok(vaccine);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        #endregion

    }
}
