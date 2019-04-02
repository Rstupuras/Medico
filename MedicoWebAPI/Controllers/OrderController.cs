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
    public class OrderController : ControllerBase
    {
        ClientHandler clientHandler = new ClientHandler();

        [HttpGet]
        public ActionResult<ICollection<Order>> Get()
        {
            Response response = new Response
            {
                Number = 17
            };
            return Ok(clientHandler.GetAllOrders(response));
        }


        [HttpGet("{id}")]
        public ActionResult<ICollection<Order>> Get(int id)
        {
            Response response = new Response
            {
                Number = 17
            };
            ICollection<Order> ordersFromDb = clientHandler.GetAllOrders(response);
            if (!(ordersFromDb.Any(x => x.ID == id)))
            {
                return NotFound("No order with this ID");
            }

            ICollection<Order> ordersToReturn = new HashSet<Order>();
            foreach (Order order in ordersFromDb)
            {
                if (order.ID == id)
                {
                    ordersToReturn.Add(order);
                }
            }

            return Ok(ordersToReturn);
        }


        [HttpPost]
        public ActionResult Post([FromBody] Order Order, [FromQuery] int Patient, [FromQuery] int PharmacyId)
        {
            Response responseGetAllPatients = new Response
            {
                Number = 9
            };
            Response responseGetAllPharmacies = new Response
            {
                Number = 27
            };
            if (clientHandler.GetAllPatients(responseGetAllPatients).Any(x => x.ID == Patient)==false)
            {
                return NotFound("No patient with this ID");
            }

            if (clientHandler.GetAllPharmacies(responseGetAllPharmacies).Any(x => x.ID == PharmacyId)==false)
            {
                return NotFound("No pharmacy with this ID");
            }

            Order.PharmacyID = PharmacyId;
            Patient patient = new Patient
            {
                ID = Patient
            };
            Response response = new Response
            {
                Number = 18,
                Order = Order,
                Patient = patient
            };

            clientHandler.AddOrder(response);
            return Ok("Order added");
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Order Order)
        {
            Response responseGetAllOrders = new Response
            {
                Number = 17
            };
            
        

            if (clientHandler.GetAllOrders(responseGetAllOrders).Any(x => x.ID == id)==false)
            {
                return NotFound("No order with this ID");
            }


            Response response = new Response
            {
                Number = 17,
                Order = Order
            };
            

            clientHandler.UpdateOrder(response, id);
            return Ok("Order updated");
        }

        [HttpPut("{orderId}/item/{medicamentId}")]
        public ActionResult PutItemsToOrder(int orderId, int medicamentId, [FromQuery] int q)
        {
            Response responseGetAllOrders = new Response
            {
                Number = 17
            };
            Response responseGetAllMedicaments = new Response
            {
                Number = 13
            };

            if (clientHandler.GetAllOrders(responseGetAllOrders).Any(x => x.ID == orderId)==false)
            {
                return NotFound("No order with this ID");
            }

            if (clientHandler.GetAllMedicaments(responseGetAllMedicaments).Any(x => x.ID == medicamentId)==false)
            {
                return NotFound("No medicament with this ID");
            }

            Response response = new Response
            {
                Number = 20,
            };
            Order order = new Order
            {
                ID = orderId
            };
            Medicament medicament = new Medicament
            {
                ID = medicamentId
            };
            OrderItem orderItem = new OrderItem
            {
                Medicament = medicament,
                Quantity = q
            };
            response.Order = order;
            response.OrderItem = orderItem;
            clientHandler.AddItemsToOrder(response);
            return Ok("Item added to order");
        }


        // DELETE api/order/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Response responseGetAllOrders = new Response
            {
                Number = 17
            };
            if ((clientHandler.GetAllOrders(responseGetAllOrders).Any(x => x.Items.Count > 0)))
            {
                return BadRequest("Order with items cannot be deleted");
            }

            Order order = new Order();
            order.ID = id;
            Response response = new Response
            {
                Number = 21,
                Order = order
            };
            clientHandler.DeleteOrder(response);
            return Ok("Order deleted");
        }

        [HttpDelete("{orderId}/item/{itemId}")]
        public ActionResult DeleteItemsFromOrder(int orderId, int itemId)
        {
            Response responseGetAllOrders = new Response
            {
                Number = 17
            };
            if (clientHandler.GetAllOrders(responseGetAllOrders).Any(x => x.ID == orderId)==false)
            {
                return NotFound("No order with this ID");
            }
            if (clientHandler.GetAllOrders(responseGetAllOrders).Where(x => x.ID == orderId)
                .Any(o => o.Items.Any(i => i.ID == itemId))==false)
            {
                return NotFound("No item with this ID");
            }
            Response response = new Response
            {
                Number = 22,
            };
            Order order = new Order
            {
                ID = orderId
            };
            OrderItem orderItem = new OrderItem
            {
                ID = itemId
            };
            response.Order = order;
            response.OrderItem = orderItem;
         
            clientHandler.DeleteItemFromOrder(response);
            return Ok("Item deleted");
        }
    }
}