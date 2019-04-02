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

namespace MedicoWebAPP.Pages.Doc
{
    [Authorize(Policy = "mustbedoctor")]
    public class ActivePrescriptionsModel : PageModel
    {
        private readonly WebAPI _api = new WebAPI();
        [BindProperty]
        public Doctor doctor { get; set; }
        [BindProperty]
        public int DoctorID { get; set; }
        public IAuthorizationService AuthorizationService { get; set; }
        [BindProperty]
        public ICollection<Prescription> Prescriptions { get; set; }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
        public void OnGet()
        {
            Prescriptions = new HashSet<Prescription>();
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();
            Console.WriteLine(sid);
            DoctorID = Int32.Parse(sid.ToString());
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = client.GetAsync("api/prescription/").Result;
            ICollection<Prescription> p = responseMessage.Content.ReadAsAsync<ICollection<Prescription>>().Result;




            foreach (Prescription prescription in p)
            {
                if (prescription.DoctorID == DoctorID)
                {
                    if (prescription.DateTimeTo >= DateTime.Now)
                    {
                        Prescriptions.Add(prescription);
                    }
                   
                }

            }
            Prescriptions = Prescriptions.OrderBy(a => a.DateTimeTo).ToList();
        }
    }
}