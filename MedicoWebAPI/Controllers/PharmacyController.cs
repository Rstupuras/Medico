using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace MedicoWebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class PharmacyController : ControllerBase
    {
        ClientHandler clientHandler = new ClientHandler();


        [HttpGet]
        public ActionResult<ICollection<Pharmacy>> Get()
        {
            Response response = new Response
            {
                Number = 27
            };

            return Ok(clientHandler.GetAllPharmacies(response));
        }


        [HttpGet("{id}")]
        public ActionResult<ICollection<Pharmacy>> Get(int id)
        {
            Response response = new Response
            {
                Number = 27
            };
            ICollection<Pharmacy> pharmaciesFromDB = clientHandler.GetAllPharmacies(response);
            if (!(pharmaciesFromDB.Any(x => x.ID == id)))
            {
                return NotFound("No pharmacy with this ID");
            }

            ICollection<Pharmacy> pharmaciesToReturn = new HashSet<Pharmacy>();
            foreach (Pharmacy pharmacy in pharmaciesFromDB)
            {
                if (pharmacy.ID == id)
                {
                    pharmaciesToReturn.Add(pharmacy);
                }
            }

            return Ok(pharmaciesToReturn);
        }


        [HttpPost]
        public ActionResult Post([FromBody] Pharmacy pharmacy)
        {
            if (pharmacy.ID != 0)
            {
                return BadRequest("ID must be 0");
            }
            Response responseGetAllPharmacies = new Response
            {
                Number = 27
            };

            if (clientHandler.GetAllPharmacies(responseGetAllPharmacies).Any(x => x.Username == pharmacy.Username))
            {
                return BadRequest("Pharmacy with this username already exists");
            }
            Response response = new Response
            {
                Number = 28,
                Pharmacy = pharmacy
            };


            clientHandler.AddPharmacy(response);
            return Ok("Pharmacy added");
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Pharmacy pharmacy)
        {
            Response responseGetAllPharmacies = new Response
            {
                Number = 27
            };
            if (!(clientHandler.GetAllPharmacies(responseGetAllPharmacies).Any(x => x.ID == id)))
            {
                return NotFound("No pharmacy with this id");
            }
              if (clientHandler.GetAllPharmacies(responseGetAllPharmacies).Any(x => x.Username == pharmacy.Username && x.ID != pharmacy.ID))
            {
                return BadRequest("Pharmacy with this username already exists");
            }
              if (clientHandler.GetAllPharmacies(responseGetAllPharmacies).First(x=> x.ID == id).Orders.Count!=0)
            {
                return BadRequest("Pharmacy with orders cannot be edited");
            }

            Response response = new Response
            {
                Number = 27,
                Pharmacy = pharmacy
            };

            clientHandler.UpdatePharmacy(response, id);
            return Ok("Pharmacy updated");
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Response responseGetAllPharmacies = new Response
            {
                Number = 27
            };
            if (!(clientHandler.GetAllPharmacies(responseGetAllPharmacies).Any(x => x.ID == id)))
            {
                return NotFound("No pharmacy with this id");
            }

            if ((clientHandler.GetAllPharmacies(responseGetAllPharmacies)
                .Any(x => x.Orders.Any(p => p.PharmacyID == id))))
            {
                return BadRequest("Pharmacy with orders cannot bet deleted");
            }

            Pharmacy pharmacy = new Pharmacy();
            pharmacy.ID = id;
            Response response = new Response
            {
                Number = 30,
                Pharmacy = pharmacy
            };
            clientHandler.DeletePharmacy(response);
            return Ok("Pharmacy deleted");
        }

        [HttpPost("{login}")]
        [AllowAnonymous]
        public ActionResult Login()
        {
            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic ".Length).Trim();
                var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)); //admin:pass
                var usernameAndPass = usernameAndPassenc.Split(":");


                Response response = new Response
                {
                    Number = 27
                };

                ICollection<Pharmacy> pharmacies = clientHandler.GetAllPharmacies(response);
                ICollection<Pharmacy> pharmaciesToReturn = new HashSet<Pharmacy>();
                foreach (var pharmacy in pharmacies)
                {
                    if (usernameAndPass[0].Equals(pharmacy.Username) && usernameAndPass[1].Equals(pharmacy.Password))
                    {
                        pharmaciesToReturn.Add(pharmacy);
                        return Ok(pharmaciesToReturn);
                    }


                }
            }
            return BadRequest("Unauthorized");
        }



    }
}