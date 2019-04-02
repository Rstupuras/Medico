using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MedicoWebAPP.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MedicoWebAPP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebAPI _api = new WebAPI();

        public Doctor doctor {get;set;}

        public IAuthorizationService AuthorizationService {get; set;}

        public ICollection<Appointment> appointments {get;set;}
        
        public Patient patient { get; set; }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("doctor"))
                {
                    var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                   .Select(c => c.Value).SingleOrDefault();
                    int Id = Int32.Parse(sid.ToString());
                    doctor = new Doctor();
                    HttpClient client = _api.Initial();
                    HttpResponseMessage responseMessage = client.GetAsync("api/doctor/"+Id).Result;
  
                    Doctor d = responseMessage.Content.ReadAsAsync<Doctor>().Result;
                    doctor = d;
                    responseMessage = client.GetAsync("api/doctor/"+ Id+"/appointments").Result;
                    appointments = responseMessage.Content.ReadAsAsync<ICollection<Appointment>>().Result;
                }
                if (User.IsInRole("patient"))
                {
                    var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                   .Select(c => c.Value).SingleOrDefault();
                    int Id = Int32.Parse(sid.ToString());
                    patient = new Patient();
                    HttpClient client = _api.Initial();
                    HttpResponseMessage responseMessage = client.GetAsync("api/patient/" + Id).Result;

                    Patient p = responseMessage.Content.ReadAsAsync<Patient>().Result;
                    patient = p;
                    responseMessage = client.GetAsync("api/patient/" + Id + "/appointments").Result;
                    appointments = responseMessage.Content.ReadAsAsync<ICollection<Appointment>>().Result;
                }

            }
                
            }
        }
        
    }
    
