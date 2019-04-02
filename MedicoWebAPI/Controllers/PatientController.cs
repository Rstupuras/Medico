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
    public class PatientController : ControllerBase
    {
        ClientHandler clientHandler = new ClientHandler();

        // GET api/patient
        [HttpGet]
        public ActionResult<ICollection<Patient>> Get()
        {
            Response response = new Response
            {
                Number = 9
            };
            return Ok(clientHandler.GetAllPatients(response));
        }

        // GET api/patient/5
        [HttpGet("{id}")]
        public ActionResult<Patient> Get(int id)
        {
            Response response = new Response
            {
                Number = 9
            };

            foreach (Patient patient in clientHandler.GetAllPatients(response))
            {
                if (patient.ID == id)
                {
                    return Ok(patient);
                }
            }

            return NotFound("No patient with this ID");
        }
        [HttpGet("{id}/appointments")]
        public ActionResult<ICollection<Appointment>> GetPatientAppointments(int id)
        {
            Response responseGetAllPatients = new Response
            {
                Number = 9
            };
            if (clientHandler.GetAllPatients(responseGetAllPatients).Any(x => x.ID == id))
            {
                Response responseGetAllAppointments = new Response
                {
                    Number = 5
                };
                ICollection<Appointment> AppointmentsWithID = new HashSet<Appointment>();

                foreach (Appointment a in clientHandler.GetAllAppointments(responseGetAllAppointments))
                {
                    if (a.PatientID == id)
                    {
                        AppointmentsWithID.Add(a);
                    }
                }

                return Ok(AppointmentsWithID);
            }

            return NotFound("No patient with this ID");
        }
        // POST api/doctor/login
        [HttpPost("login")]
        public ActionResult Login([FromBody] Patient patient)
        {
            Response response = new Response
            {
                Number = 9
            };
            ICollection<Patient> patients = clientHandler.GetAllPatients(response);
            Patient p = null;
            foreach (Patient pat in patients)
            {
                if (patient.Username == pat.Username)
                {
                    if (patient.Password == pat.Password)
                    {
                        p = pat;
                    }
                }
            }
            if (p == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(p);
            }
        }

        // POST api/patient
        [HttpPost]
        public ActionResult Post([FromBody] Patient patient, [FromQuery] int MainDoctor)
        {
            Response responseGetAllPatients = new Response
            {
                Number = 9
            };

            if((clientHandler.GetAllPatients(responseGetAllPatients).Any(x=>x.Username==patient.Username)))
            {
                return BadRequest("Patient with this username already exists");
            }
            Response responseGetAllDoctors = new Response
            {
                Number = 1
            };
            if (clientHandler.GetAllDoctors(responseGetAllDoctors).Any(x => x.ID == MainDoctor))
            {
                Response response = new Response
                {
                    Number = 10,
                    Patient = patient
                };

                Doctor doctor = new Doctor
                {
                    ID = MainDoctor
                };
                response.Doctor = doctor;


                clientHandler.AddPatient(response);
                return Ok("Patient added");
            }

            return NotFound("No doctor with this ID");
        }

        // PUT api/patient/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Patient patient, [FromQuery] int MainDoctor)
        {
            Response responseGetAllPatients = new Response
            {
                Number = 9
            };
            if (clientHandler.GetAllPatients(responseGetAllPatients).Any(x => x.ID == id))
            {
                    if((clientHandler.GetAllPatients(responseGetAllPatients).Any(x=>x.Username==patient.Username && x.ID != patient.ID)))
            {
                return BadRequest("Patient with this username already exists");
            }
                Response responseGetAllDoctors = new Response
                {
                    Number = 1
                };
                if (clientHandler.GetAllDoctors(responseGetAllDoctors).Any(x => x.ID == MainDoctor))
                {
                    Response response = new Response
                    {
                        Number = 9,
                        Patient = patient
                    };
                    if (MainDoctor != 0)
                    {
                        Doctor doctor = new Doctor
                        {
                            ID = MainDoctor
                        };
                        response.Doctor = doctor;
                    }

                    clientHandler.UpdatePatient(response, id);
                    return Ok("Patient updated");
                }

                return NotFound("No doctor with this ID");
            }

            return NotFound("No patient with this ID");
        }

        // DELETE api/patient/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Response responseGetAllAppointments = new Response
            {
                Number = 5
            };
            if (clientHandler.GetAllAppointments(responseGetAllAppointments).Any(x => x.PatientID == id))
            {
                return BadRequest("Patient with appointments cannot be deleted");
            }

            Response responseGetPrescriptions = new Response
            {
                Number = 23
            };
            if (clientHandler.GetAllPrescriptions(responseGetPrescriptions).Any(x => x.PatientID == id))
            {
                return BadRequest("Patient with prescriptions cannot be deleted");
            }

            Patient patient = new Patient();
            patient.ID = id;
            Response response = new Response
            {
                Number = 12,
                Patient = patient
            };
            clientHandler.DeletePatient(response);
            return Ok("Patient deleted");
        }
    }
}