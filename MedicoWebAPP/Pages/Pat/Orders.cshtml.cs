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
    public class OrdersModel : PageModel
    {
        private readonly WebAPI _api = new WebAPI();
        [BindProperty]
        public Patient patient { get; set; }
        [BindProperty]
        public int PatientID { get; set; }
        public IAuthorizationService AuthorizationService { get; set; }
        [BindProperty]
        public ICollection<Order> Orders { get; set; }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
        public void OnGet()
        {
            Orders = new HashSet<Order>();
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();
            Console.WriteLine(sid);
            PatientID = Int32.Parse(sid.ToString());
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = client.GetAsync("api/order/").Result;
            ICollection<Order> o = responseMessage.Content.ReadAsAsync<ICollection<Order>>().Result;





            foreach (Order order in o)
            {
                if (order.PatientID == PatientID)
                {
                    if (order.IsSent == true)
                    {
                        Orders.Add(order);
                    }
                }

            }
            
        }
    }
}