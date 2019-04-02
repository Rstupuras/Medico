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

namespace MedicoWebAPP.Pages.Doc
{
    [Authorize(Policy = "mustbedoctor")]
    public class AppointmentsModel : PageModel
    {
        private readonly WebAPI _api = new WebAPI();

        public Doctor doctor {get;set;}

        public IAuthorizationService AuthorizationService {get; set;}

        public ICollection<Appointment> appointmentsToday {get;set;}

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
        public void OnGet()
        {
            appointmentsToday = new HashSet<Appointment>();
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();
            Console.WriteLine(sid);
            int Id = Int32.Parse(sid.ToString());
            HttpClient client = _api.Initial();
            Console.WriteLine("api/doctor/" + Id);
            HttpResponseMessage responseMessage = client.GetAsync("api/doctor/"+Id).Result;
            Doctor d = responseMessage.Content.ReadAsAsync<Doctor>().Result;
            doctor = d;
            responseMessage = client.GetAsync("api/doctor/"+ Id+"/appointments").Result;
            ICollection<Appointment> appointments = responseMessage.Content.ReadAsAsync<ICollection<Appointment>>().Result;

            foreach (Appointment appointment in appointments){
            if (appointment.DateTime.Day == @DateTime.Now.Day && appointment.DateTime.Month == @DateTime.Now.Month &&
                appointment.DateTime.Year == @DateTime.Now.Year)
                {
                    appointmentsToday.Add(appointment);
                }
                appointmentsToday = appointmentsToday.OrderBy(a=> a.DateTime).ToList();   
        }  
    }
        
    }
}