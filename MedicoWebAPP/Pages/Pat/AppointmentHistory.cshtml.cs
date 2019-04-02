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

namespace MedicoWebAPP.Pages.Pat
{
    [Authorize(Policy = "mustbepatient")]
    public class AppointmentHistoryModel : PageModel
    {
        private readonly WebAPI _api = new WebAPI();
        [BindProperty]
        public Patient patient { get; set; }
        [BindProperty]
        public int PatientID { get; set; }
        public IAuthorizationService AuthorizationService { get; set; }
        [BindProperty]
        public ICollection<Appointment> Appointments { get; set; }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
        public void OnGet()
        {
            Appointments = new HashSet<Appointment>();
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();
            Console.WriteLine(sid);
            PatientID = Int32.Parse(sid.ToString());
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = client.GetAsync("api/patient/" + PatientID).Result;
            Patient p = responseMessage.Content.ReadAsAsync<Patient>().Result;
            patient = p;
            responseMessage = client.GetAsync("api/patient/" + PatientID + "/appointments").Result;
            ICollection<Appointment> appointments = responseMessage.Content.ReadAsAsync<ICollection<Appointment>>().Result;

            foreach (Appointment appointment in appointments)
            {
                Appointments.Add(appointment);
            }
            Appointments = Appointments.OrderBy(a => a.DateTime).ToList();
        }
    }
}