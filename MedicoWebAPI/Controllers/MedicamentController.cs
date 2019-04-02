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
    public class MedicamentController : ControllerBase
    {
        ClientHandler clientHandler = new ClientHandler();

        // GET api/medicament
        [HttpGet]
        public ActionResult<ICollection<Medicament>> Get()
        {
            Response response = new Response
            {
                Number = 13
            };
            return Ok(clientHandler.GetAllMedicaments(response));
        }

        // GET api/medicament/5
        [HttpGet("{id}")]
        public ActionResult<Medicament> Get(int id)
        {
            Response response = new Response
            {
                Number = 13
            };
            ICollection<Medicament> medicamentsFromDB = clientHandler.GetAllMedicaments(response);
            if (medicamentsFromDB.Any(x => x.ID == id) == false)
            {
                return NotFound("No medicament with this ID");
            }

            ICollection<Medicament> medicamentsToReturn = new HashSet<Medicament>();
            foreach (Medicament medicament in medicamentsFromDB)
            {
                if (medicament.ID == id)
                {
                    medicamentsToReturn.Add(medicament);
                }
            }

            return Ok(medicamentsToReturn);
        }

        // POST api/medicament
        [HttpPost]
        public ActionResult Post([FromBody] Medicament medicament)
        {
            if (medicament.ID != 0)
            {
                return BadRequest("ID must be 0 or not used");
            }

            Response response = new Response
            {
                Number = 14,
                Medicament = medicament
            };
            clientHandler.AddMedicament(response);
            return Ok("Medicament added");
        }

        // PUT api/medicament/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Medicament Medicament)
        {
            Response responseGetAllMedicaments = new Response
            {
                Number = 13
            };
            ICollection<Medicament> Medicaments = clientHandler.GetAllMedicaments(responseGetAllMedicaments);
            Response responseGetAllOrders = new Response
            {
                Number = 17
            };
            ICollection<Order> orders = clientHandler.GetAllOrders(responseGetAllOrders);
            if (Medicaments.Any(x => x.ID == id))
            {
                if ((orders.Any(m => m.Items.Any(x => x.Medicament.ID == id))))
                {
                    return BadRequest("Order contains this medicament. First complete orders with this medicament");
                }

                Response responseUpdateMedicament = new Response
                {
                    Number = 13,
                    Medicament = Medicament
                };

                clientHandler.UpdateMedicament(responseUpdateMedicament, id);
                return Ok("Medicament updated");
            }

            return NotFound("No medicament with this ID");
        }

        // DELETE api/medicament/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Response responseGetAllMedicaments = new Response
            {
                Number = 13
            };
            ICollection<Medicament> Medicaments = clientHandler.GetAllMedicaments(responseGetAllMedicaments);

            Response responseGetAllPrescriptions = new Response
            {
                Number = 23
            };
            ICollection<Prescription> presrciptions = clientHandler.GetAllPrescriptions(responseGetAllPrescriptions);
            Response responseGetAllOrders = new Response
            {
                Number = 17
            };


            ICollection<Order> orders = clientHandler.GetAllOrders(responseGetAllOrders);


            if (Medicaments.Any(x => x.ID == id) == false)
            {
                return NotFound("No medicament with this ID");
            }

            if ((presrciptions.Any(p => p.Medicament.ID == id)))
            {
                return BadRequest("Prescribed medicament cannot be deleted");
            }

            if ((orders.Any(m => m.Items.Any(x => x.Medicament.ID == id))))
            {
                return BadRequest("Order contains this medicament. First complete orders with this medicament");
            }

            Medicament medicament = new Medicament();
            medicament.ID = id;
            Response response = new Response
            {
                Number = 16,
                Medicament = medicament
            };
            clientHandler.DeleteMedicament(response);
            return Ok("Medicament deleted");
        }
    }

}