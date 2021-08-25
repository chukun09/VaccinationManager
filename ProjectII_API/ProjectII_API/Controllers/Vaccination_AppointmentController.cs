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
    [Route("api/v1/vaccinationappointment")]
    [ApiController]
    public class Vaccination_AppointmentController : ControllerBase
    {
        Respone _res;
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var VaccinationAppointmentContext = new VaccinationAppointmentContext();
                var vaccinationAppointment = VaccinationAppointmentContext.GetAllExtend();
                return Ok(vaccinationAppointment);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [HttpGet("user/{id}")]
        public IActionResult GetByIdExtend(Guid id)
        {
            try
            {
                var VaccinationAppointmentContext = new VaccinationAppointmentContext();
                var vaccinationAppointment = VaccinationAppointmentContext.GetByIdExtend(id);
                return Ok(vaccinationAppointment);
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
                var VaccinationAppointmentContext = new VaccinationAppointmentContext();
                var vaccinationAppointment = VaccinationAppointmentContext.GetById(id);
                return Ok(vaccinationAppointment);
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
                var VaccinationAppointmentContext = new VaccinationAppointmentContext();
                VaccinationAppointmentContext.DeleteById(id);
                return Ok(Project2.Common.Properties.Resources.DeleteSuccesful);
            }
            catch (Exception ex)
            {
                _res.DevMsg = ex.Message;
                _res.UserMsg = Project2.Common.Properties.Resources.ErrorUserMessage;
                return StatusCode(500, _res);
            }
        }
        [Authorize(Roles = "Bác sĩ")]
        [HttpPost]
        public IActionResult Post(Vaccination_Appointment vaccination_Appointment)
        {
            try
            {
                VaccinationAppointmentContext vaccinationAppointmentContext = new VaccinationAppointmentContext();
                var res = vaccinationAppointmentContext.Insert(vaccination_Appointment);
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
        public IActionResult Put(Vaccination_Appointment vaccination_Appointment, Guid id)
        {
            try
            {
                VaccinationAppointmentContext vaccinationAppointmentContext = new VaccinationAppointmentContext();
                var res = vaccinationAppointmentContext.Update(vaccination_Appointment);
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
