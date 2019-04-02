using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace MedicoWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        ClientHandler clientHandler = new ClientHandler();

        [HttpGet]
        public ActionResult<ICollection<Prescription>> Get()
        {
            Response response = new Response
            {
                Number = 23
            };

            return Ok(clientHandler.GetAllPrescriptions(response));
        }


        [HttpGet("{id}")]
        public ActionResult<Prescription> Get(int id)
        {
            Response response = new Response
            {
                Number = 23
            };
            ICollection<Prescription> prescriptionsFromDb = clientHandler.GetAllPrescriptions(response);
            if (prescriptionsFromDb.Any(x => x.ID == id) == false)
            {
                return NotFound("No prescription with this ID");
            }

            ICollection<Prescription> prescriptionsToReturn = new HashSet<Prescription>();
            foreach (Prescription prescription in prescriptionsFromDb)
            {
                if (prescription.ID == id)
                {
                    prescriptionsToReturn.Add(prescription);
                }
            }

            return Ok(prescriptionsToReturn);
        }


        [HttpPost("{patientID}")]
        public ActionResult Post(int patientID, [FromQuery] int doctor, [FromQuery] int medicament,
            [FromBody] Prescription prescription)
        {
            Response responsGetAllDoctors = new Response
            {
                Number = 1
            };

            Response responseGetAllPatients = new Response
            {
                Number = 9
            };
            if (clientHandler.GetAllDoctors(responsGetAllDoctors).Any(x => x.ID == doctor) == false)
            {
                return NotFound("No doctor with this ID");
            }

            if (clientHandler.GetAllPatients(responseGetAllPatients).Any(x => x.ID == patientID) == false)
            {
                return NotFound("No patient with this ID");
            }
            Response responseGetAllMedicaments = new Response
            {
                Number = 13
            };
            Medicament medicament1 = new Medicament();
            ICollection<Medicament> medicamentsFromDB = clientHandler.GetAllMedicaments(responseGetAllMedicaments);
            if (medicamentsFromDB.Any(x => x.ID == medicament) == false)
            {
                return NotFound("No medicament with this ID");
            }
            prescription.Medicament = medicamentsFromDB.First(x => x.ID == medicament);
            prescription.PatientID = patientID;
            prescription.DoctorID = doctor;

            if (prescription.ID != 0)
            {
                return BadRequest("ID must be 0");
            }

            Response response = new Response
            {
                Number = 24,
                Prescription = prescription
            };
            clientHandler.AddPrescription(response);
            return Ok("Prescription added");
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Prescription prescription)
        {
            Response responseGetAllPrescriptions = new Response
            {
                Number = 23
            };
         

        
         
            if (clientHandler.GetAllPrescriptions(responseGetAllPrescriptions).Any(x => x.ID == id))
            {
                Response response = new Response
                {
                    Number = 23,
                    Prescription = prescription
                };
                clientHandler.UpdatePrescription(response, id);
                return Ok("Prescription updated");
            }

            return NotFound("No prescription with this ID");
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Response responseGetAllPrescriptions = new Response
            {
                Number = 23
            };
            if (clientHandler.GetAllPrescriptions(responseGetAllPrescriptions).Any(x => x.ID == id) == false)

            {
                return NotFound("No prescription with this ID");
            }
            Prescription prescription = new Prescription();
            prescription.ID = id;
            Response response = new Response
            {
                Number = 26,
                Prescription = prescription
            };
            clientHandler.DeletePrescription(response);
            return Ok("Prescription deleted");
        }
    }
}