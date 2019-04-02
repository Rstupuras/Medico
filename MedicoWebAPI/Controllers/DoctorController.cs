using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace MedicoWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        ClientHandler clientHandler = new ClientHandler();

        // GET api/doctor
        [HttpGet]
        public ActionResult<ICollection<Doctor>> Get()
        {
            Response response = new Response
            {
                Number = 1
            };
            return Ok(clientHandler.GetAllDoctors(response));
        }

        // GET api/doctor/5
        [HttpGet("{id}")]
        public ActionResult<Doctor> Get(int id)
        {
            Response response = new Response
            {
                Number = 1
            };

            foreach (Doctor doctor in clientHandler.GetAllDoctors(response))
            {
                if (doctor.ID == id)
                {
                    return Ok(doctor);
                }
            }

            return NotFound("No doctor with this ID");
        }

        // GET api/doctor/5/appointments
        [HttpGet("{id}/appointments")]
        public ActionResult<ICollection<Appointment>> GetDoctorAppointments(int id)
        {
            Response responseGetAllDoctors = new Response
            {
                Number = 1
            };


            if (clientHandler.GetAllDoctors(responseGetAllDoctors).Any(x => x.ID == id))
            {
                Response responseGetAllAppointments = new Response
                {
                    Number = 5
                };
                ICollection<Appointment> AppointmentsWithID = new HashSet<Appointment>();

                foreach (Appointment a in clientHandler.GetAllAppointments(responseGetAllAppointments))
                {
                    if (a.DoctorID == id)
                    {
                        AppointmentsWithID.Add(a);
                    }
                }

                return Ok(AppointmentsWithID);
            }

            return NotFound("No doctor with this ID");
        }

        // POST api/doctor
        [HttpPost]
        public ActionResult Post([FromBody] Doctor doctor)
        {
            if (doctor.ID != 0)
            {
                return BadRequest("ID must be 0 or not used");
            }
            Response responseGetAllDoctors = new Response
            {
                Number = 1
            };
            if ((clientHandler.GetAllDoctors(responseGetAllDoctors).Any(x => x.Username == doctor.Username)))
            {
                return BadRequest("Doctor with this username already exists");
            }

            Response response = new Response
            {
                Number = 2,
                Doctor = doctor
            };
            clientHandler.AddDoctor(response);
            return Ok("Doctor added");
        }

        // POST api/doctor/login
        [HttpPost("login")]
        public ActionResult Login([FromBody] Doctor doctor)
        {
            Response response = new Response
            {
                Number = 1
            };
            ICollection<Doctor> doctors = clientHandler.GetAllDoctors(response);
            Doctor d = null;
            foreach (Doctor Doctor in doctors)
            {
                if (doctor.Username == Doctor.Username)
                {
                    if (doctor.Password == Doctor.Password)
                    {
                        d = Doctor;
                    }
                }
            }
            if (d == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(d);
            }
        }


        // PUT api/doctor/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Doctor Doctor)
        {
            Response responseGetAllDoctors = new Response
            {
                Number = 1
            };

            if (clientHandler.GetAllDoctors(responseGetAllDoctors).Any(x => x.ID == id))
            {
                   if ((clientHandler.GetAllDoctors(responseGetAllDoctors).Any(x => x.Username == Doctor.Username && x.ID != Doctor.ID)))
            {
                return BadRequest("Doctor with this username already exists");
            }

                Response response = new Response
                {
                    Number = 1,
                    Doctor = Doctor
                };
                clientHandler.UpdateDoctor(response, id);
                return Ok("Doctor updated");
            }

            return NotFound("No doctor with this id");
        }

        // DELETE api/doctor/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Response responseGetAllAppointments = new Response
            {
                Number = 5
            };
            Response responseGetAllPatients = new Response
            {
                Number = 9
            };
            if ((clientHandler.GetAllPatients(responseGetAllPatients).Any(x => x.MainDoctorID == id)))
            {
                return BadRequest("Patient Main Doctor cannot be deleted");
            }
            if ((clientHandler.GetAllAppointments(responseGetAllAppointments).Any(x => x.PatientID == id)))
            {
                return BadRequest("Doctor with appointments cannot be deleted");
            }

            Doctor doctor = new Doctor();
            doctor.ID = id;
            Response response = new Response
            {
                Number = 4,
                Doctor = doctor
            };
            clientHandler.DeleteDoctor(response);
            return Ok("Doctor deleted");
        }
    }
}