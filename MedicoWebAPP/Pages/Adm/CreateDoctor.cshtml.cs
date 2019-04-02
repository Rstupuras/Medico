using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MedicoWebAPP.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MedicoWebAPP.Pages.Adm
{
    [Authorize(Policy = "mustbeadmin")]
    public class CreateDoctorModel : PageModel
    {
        private readonly WebAPI _api = new WebAPI();

        [BindProperty]
        public Doctor doctor { get; set; }


        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostCreateDoctor()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage mes = await client.GetAsync("api/doctor/");
            ICollection<Doctor> doc = new HashSet<Doctor>();
            Doctor docs = null;
            if (mes.IsSuccessStatusCode)
            {
                
                foreach (Doctor d in  mes.Content.ReadAsAsync<ICollection<Doctor>>().Result)
                {
                    if (doctor.Username == d.Username)
                    {
                        docs = d;
                    }
                }
                if (docs == null)
                {
                    var myContent = JsonConvert.SerializeObject(doctor);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage responseMessage = await client.PostAsync("api/doctor", byteContent);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        ViewData["Message"] = "Doctor was created";
                        doctor = null;
                        ModelState.Clear();
                        return this.Page();
                    }
                    else
                    {
                        
                        return RedirectToPage("/Error");
                    }
                }
                else
                    {
                    ModelState.AddModelError("", "Username already exists");
                    return this.Page();
                }


            }
            else
            {
                return RedirectToPage("/Error");
            }

        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }


    }
}