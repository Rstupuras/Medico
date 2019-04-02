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
using Newtonsoft.Json;

namespace MedicoWebAPP.Pages.Pat
{
    [Authorize(Policy = "mustbepatient")]
    public class MedicamentsModel : PageModel
    {
        [BindProperty]
        public ICollection<Medicament> medicaments { get; set; }
        [BindProperty]
        public ICollection<Prescription> Prescriptions { get; set; }
        public HttpClient client { get; set; }
        public HttpResponseMessage responseMessage {get;set;}
        private readonly WebAPI _api = new WebAPI();
        public void OnGet()
        {
            client = _api.Initial();
            responseMessage = client.GetAsync("api/medicament/").Result;
            medicaments = new HashSet<Medicament>();
            foreach (Medicament m in responseMessage.Content.ReadAsAsync<ICollection<Medicament>>().Result)
            {
                if (m.IsPrescribed == false)
                {
                    medicaments.Add(m);
                }

            }
            Prescriptions = new HashSet<Prescription>();
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();
            int PatientID = Int32.Parse(sid.ToString());

            responseMessage = client.GetAsync("api/prescription/").Result;
            foreach(Prescription prescription in responseMessage.Content.ReadAsAsync<ICollection<Prescription>>().Result)
            {
                if (prescription.PatientID == PatientID)
                {
                    if (prescription.DateTimeTo >= DateTime.Now)
                    {
                        if (medicaments.Any(m=> m.ID == prescription.Medicament.ID)==false)
                        {
                            medicaments.Add(prescription.Medicament);
                        }
                        
                    }
                    
                }
            }
            
            
           

        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPostAddToOrderAsync(int id)
        {
            Order order = new Order{
                ID = 0
            };
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
            if (order.ID == 0) 
            {
                responseMessage = client.GetAsync("api/pharmacy").Result;
                ICollection<Pharmacy> pharmacies = new HashSet<Pharmacy>();
                pharmacies = responseMessage.Content.ReadAsAsync<ICollection<Pharmacy>>().Result;
                Pharmacy pharmacy = new Pharmacy();
                pharmacy = pharmacies.First();
                var myContent = JsonConvert.SerializeObject(order);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                responseMessage  =await client.PostAsync("api/order?Patient=" + PatientID + "&PharmacyId=" + pharmacy.ID, byteContent);


                if (responseMessage.IsSuccessStatusCode)
                {
                    
                 
                    while (order.ID ==0)
                    {
                        responseMessage = await client.GetAsync("api/order/");
                        foreach (Order o in responseMessage.Content.ReadAsAsync<ICollection<Order>>().Result)
                        {

                            if (o.PatientID == PatientID)
                            {
                                if (o.IsSent == false)
                                {
                                    order.ID = o.ID;
                                }
                            }
                        }
                    }
                    

                    Console.WriteLine(order.ID);
                }
                responseMessage = client.PutAsync("api/order/" + order.ID + "/item/" + id + "?q=1",null).Result;
               
                ViewData["Message"] = "Medicament was added to order";
                    
                    responseMessage = client.GetAsync("api/medicament/").Result;
                    medicaments = new HashSet<Medicament>();
                    foreach (Medicament m in responseMessage.Content.ReadAsAsync<ICollection<Medicament>>().Result)
                    {
                        if (m.IsPrescribed == false)
                        {
                            medicaments.Add(m);
                        }

                    }
                    Prescriptions = new HashSet<Prescription>();
                    
                    responseMessage = client.GetAsync("api/prescription/").Result;
                    foreach (Prescription prescription in responseMessage.Content.ReadAsAsync<ICollection<Prescription>>().Result)
                    {
                        if (prescription.PatientID == PatientID)
                        {
                            if (prescription.DateTimeTo >= DateTime.Now)
                            {
                                if (medicaments.Any(m => m.ID == prescription.Medicament.ID) == false)
                                {
                                medicaments.Add(prescription.Medicament);
                                }
                        }

                        }
                    }
                    return this.Page();
                     



            }
            else
            {
                
                responseMessage = client.PutAsync("api/order/" + order.ID + "/item/" + id + "?q=1", null).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Medicament was added to order";
                    
                   responseMessage = client.GetAsync("api/medicament/").Result;
         
                    medicaments = new HashSet<Medicament>();
                    foreach (Medicament m in responseMessage.Content.ReadAsAsync<ICollection<Medicament>>().Result)
                    {
                        if (m.IsPrescribed == false)
                        {
                            medicaments.Add(m);
                        }

                    }
                  
                    
                    Prescriptions = new HashSet<Prescription>();
                    
                    responseMessage = client.GetAsync("api/prescription").Result;
                    foreach (Prescription prescription in responseMessage.Content.ReadAsAsync<ICollection<Prescription>>().GetAwaiter().GetResult())
                    {
                        if (prescription.PatientID == PatientID)
                        {
                            if (prescription.DateTimeTo >= DateTime.Now)
                            {
                                if (medicaments.Any(m => m.ID == prescription.Medicament.ID) == false)
                                {
                                    medicaments.Add(prescription.Medicament);
                                }
                            }

                        }
                    }
                    return this.Page();
                }
                else
                {
                    return RedirectToPage("/Error");
                }
            }
            
            
        }
    }
}