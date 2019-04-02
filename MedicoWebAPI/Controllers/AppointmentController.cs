using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MedicoWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        ClientHandler clientHandler = new ClientHandler();

        [HttpGet]
        public ActionResult<ICollection<Appointment>> Get()
        {
            Response response = new Response
            {
                Number = 5
            };
            return Ok(clientHandler.GetAllAppointments(response));
        }


        [HttpGet("{id}")]
        public ActionResult<ICollection<Appointment>> Get(int id)
        {
            Response response = new Response
            {
                Number = 5
            };

            foreach (Appointment a in clientHandler.GetAllAppointments(response))
            {
                if (a.ID == id)
                {
                    return Ok(a);
                }
            }

            return NotFound("No appointment with this ID");
        }

        // POST api/appointment
        [HttpPost]
        public ActionResult Post([FromBody] Appointment appointment,[FromQuery] int Doctor,[FromQuery] int Patient)
        {
            Response responsGetAllDoctors = new Response
            {
                Number = 1
            };

            Response responseGetAllPatients = new Response
            {
                Number = 9
            };
            if (clientHandler.GetAllDoctors(responsGetAllDoctors).Any(x => x.ID == Doctor)==false)
            {
                return NotFound("No doctor with this ID");
            }

            if (clientHandler.GetAllPatients(responseGetAllPatients).Any(x => x.ID == Patient)==false)
            {
                return NotFound("No patient with this ID");
            }

            Doctor doctor = new Doctor
            {
                ID = Doctor
            };
            Patient patient = new Patient
            {
                ID = Patient
            };
            Response response = new Response{
                Number = 6,
                Appointment = appointment,
                Doctor = doctor,
                Patient = patient
            };
            clientHandler.AddAppointment(response);
            return Ok("Appoinment added");
        }

        // PUT api/appointment/5?DateTime=2013-02-04T13:24
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Appointment appointment)
        {
            Response responseGetAllAppoitnemtns = new Response
            {
                Number = 5
            };
            if (clientHandler.GetAllAppointments(responseGetAllAppoitnemtns).Any(x => x.ID == id)==false)
            {
                return NotFound("No appointment with this ID");
            }
            Response response = new Response
            {
                Number = 5,
                Appointment = appointment
            };
            clientHandler.UpdateAppointment(response, id);
            return Ok("Appointment updated");
        }

        // DELETE api/appointment/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Appointment appointment = new Appointment();
            appointment.ID = id;
            Response response = new Response
            {
                Number = 8,
                Appointment = appointment
            };
            clientHandler.DeleteAppointment(response);
        }
    }
}