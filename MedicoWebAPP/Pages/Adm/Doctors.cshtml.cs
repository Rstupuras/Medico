using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using MedicoWebAPP.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicoWebAPP.Pages.Adm
{
    [Authorize(Policy = "mustbeadmin")]
    public class DoctorsModel : PageModel
    {
        public ICollection<Doctor> doctors { get; set; }
        private readonly WebAPI _api = new WebAPI();
        public void OnGet()
        {
            doctors = new HashSet<Doctor>();
            HttpClient client = _api.Initial();
            
            HttpResponseMessage responseMessage = client.GetAsync("api/doctor/").Result;
           
            doctors = responseMessage.Content.ReadAsAsync<ICollection<Doctor>>().Result;
            int ID = 0;
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();
            ID = Int32.Parse(sid.ToString());
            doctors.Remove(doctors.First(d => d.ID == ID));

        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPostDeleteDoctor(int id)
        {
            HttpClient client = _api.Initial();
            
            HttpResponseMessage responseMessage = await client.DeleteAsync("api/doctor/"+id);

            var content = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(content.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                doctors = new HashSet<Doctor>();
                client = _api.Initial();
 
                responseMessage = await client.GetAsync("api/doctor/");

                doctors = responseMessage.Content.ReadAsAsync<ICollection<Doctor>>().Result;
                int ID = 0;
                var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault();
                ID = Int32.Parse(sid.ToString());
                doctors.Remove(doctors.First(d => d.ID == ID));
                ViewData["Message"] = content.ToString();
                return this.Page();
            }
            else
            {
                ViewData["Error"] = content.ToString();
                doctors = new HashSet<Doctor>();
                client = _api.Initial();
                
                responseMessage = await client.GetAsync("api/doctor/");

                doctors = responseMessage.Content.ReadAsAsync<ICollection<Doctor>>().Result;
                int ID = 0;
                var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault();
                ID = Int32.Parse(sid.ToString());
                doctors.Remove(doctors.First(d => d.ID == ID));
                return this.Page();
            }
            
        }
    }
}