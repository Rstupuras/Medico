using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using MedicoWebAPP.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MedicoWebAPP.Pages.Pat
{
    [Authorize(Policy = "mustbepatient")]
    public class OrderModel : PageModel
    {
        [BindProperty]
        public Order order { get; set; }
        public SelectList pharmacies { get; set; }
        [BindProperty]
        public string Location { get; set; }
        private readonly WebAPI _api = new WebAPI();
        public HttpClient client { get; set; }
        public HttpResponseMessage responseMessage { get; set; }
        public void OnGet()
        {
            order = new Order();
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();
            int PatientID = Int32.Parse(sid.ToString());

            client = _api.Initial();
            responseMessage = client.GetAsync("api/order/").Result;
            ICollection<Order> orders = new HashSet<Order>();
            foreach (Order o in responseMessage.Content.ReadAsAsync<ICollection<Order>>().Result)
            {
                if (o.PatientID == PatientID)
                {
                    if (o.IsSent == false)
                    {
                        order = o;
                    }
                }
            }



            responseMessage = client.GetAsync("api/pharmacy/").Result;
            ICollection<Pharmacy> pharm = new HashSet<Pharmacy>();
            foreach (Pharmacy p in responseMessage.Content.ReadAsAsync<ICollection<Pharmacy>>().Result)
            {
                pharm.Add(p);
            }
            pharmacies = new SelectList(pharm,"Location", "Location");
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPostDeleteItem(int id)
        {
            order = new Order();
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();
            int PatientID = Int32.Parse(sid.ToString());
            client = _api.Initial();
            responseMessage = await client.GetAsync("api/order/");
            ICollection<Order> orders = new HashSet<Order>();
            foreach (Order o in responseMessage.Content.ReadAsAsync<ICollection<Order>>().Result)
            {
                if (o.PatientID == PatientID)
                {
                    if (o.IsSent == false)
                    {
                        order = o;
                        
                    }
                }
            }
            responseMessage = await client.DeleteAsync("api/order/"+order.ID+"/item/"+id);
            if (responseMessage.IsSuccessStatusCode)
            {
                
                responseMessage = await client.GetAsync("api/order/");
                orders = new HashSet<Order>();
                foreach (Order o in responseMessage.Content.ReadAsAsync<ICollection<Order>>().Result)
                {
                    if (o.PatientID == PatientID)
                    {
                        if (o.IsSent == false)
                        {
                            order = o;

                        }
                    }
                }
                return RedirectToPage("/Pat/Order");
            }
            else
            {
                return RedirectToPage("/Error");
            }
      
            
        }
        public async Task<IActionResult> OnPostOrderOrder()
        {
            Random rnd = new Random();
            int random = rnd.Next(00000001, 99999999);
            order = new Order();
            Pharmacy pharmacy = new Pharmacy();
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();
            int PatientID = Int32.Parse(sid.ToString());
            client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync("api/order/");
            ICollection<Order> orders = new HashSet<Order>();
            foreach (Order ord in responseMessage.Content.ReadAsAsync<ICollection<Order>>().Result)
            {
                if (ord.PatientID == PatientID)
                {
                    if (ord.IsSent == false)
                    {
                        order = ord;

                    }
                }
            }
            
            responseMessage = await client.GetAsync("api/pharmacy/");
            ICollection<Pharmacy> pharm = new HashSet<Pharmacy>();
            foreach (Pharmacy p in responseMessage.Content.ReadAsAsync<ICollection<Pharmacy>>().Result)
            {
                if (p.Location == Location)
                {
                    pharmacy = p;
                }
            }
            Order o = new Order
            {
                Status = "Not Completed",
                OrderNumber = Location.ToUpper() + "" + random,
                IsSent = true,
                OrderDate = DateTime.Now,
                PharmacyID = pharmacy.ID
            };
            Console.WriteLine(o.PharmacyID);
            var myContent = JsonConvert.SerializeObject(o);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            responseMessage = await client.PutAsync("api/order/"+order.ID, byteContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                order = new Order();
                sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault();
                PatientID = Int32.Parse(sid.ToString());

                client = _api.Initial();
                responseMessage = client.GetAsync("api/order/").Result;
                orders = new HashSet<Order>();
                foreach (Order ord in responseMessage.Content.ReadAsAsync<ICollection<Order>>().Result)
                {
                    if (ord.PatientID == PatientID)
                    {
                        if (ord.IsSent == false)
                        {
                            order = ord;
                        }
                    }
                }



                responseMessage = client.GetAsync("api/pharmacy/").Result;
                pharm = new HashSet<Pharmacy>();
                foreach (Pharmacy p in responseMessage.Content.ReadAsAsync<ICollection<Pharmacy>>().Result)
                {
                    pharm.Add(p);
                }
                pharmacies = new SelectList(pharm, "Location", "Location");
                return this.Page();
            }
            else
            {
                return RedirectToPage("/Error");
            }


        
        }
    }
}