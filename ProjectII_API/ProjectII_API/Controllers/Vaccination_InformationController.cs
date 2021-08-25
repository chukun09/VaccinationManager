using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v1/vaccinationinformation")]
    [ApiController]
    public class Vaccination_InformationController : ControllerBase
    {
        Respone _res;
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var vaccinationInformationContext = new VaccinationInformationContext();
                var vaccinationInformation = vaccinationInformationContext.GetAllExtend();
                return Ok(vaccinationInformation);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var vaccinationInformationContext = new VaccinationInformationContext();
                var vaccinationInformation = vaccinationInformationContext.GetByIdExtend(id);
                return Ok(vaccinationInformation);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            try
            {
                var vaccinationInformationContext = new VaccinationInformationContext();
                vaccinationInformationContext.DeleteById(id);
                return Ok(Project2.Common.Properties.Resources.DeleteSuccesful);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post(Vaccination_Information vaccination_Information)
        {
            try
            {
                VaccinationInformationContext vaccinationInformationContext = new VaccinationInformationContext();
                var res = vaccinationInformationContext.Insert(vaccination_Information);
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
        public IActionResult Put(Vaccination_Information vaccination_Information, Guid id)
        {
            try
            {
                VaccinationInformationContext vaccinationInformationContext = new VaccinationInformationContext();
                var res = vaccinationInformationContext.Update(vaccination_Information);
                return Ok(res);
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
