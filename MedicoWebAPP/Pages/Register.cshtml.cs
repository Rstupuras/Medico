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
    public class RegisterModel : PageModel
    {
        private readonly WebAPI _api = new WebAPI();

        [BindProperty]
        public Patient newPatient { get; set; }
        [BindProperty]
        public int MainDoctorId { get; set; }
        [BindProperty]
        public List<Doctor> DoctorList { get; set; }

        public void OnGet()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = client.GetAsync("api/doctor").Result;

            if (response.IsSuccessStatusCode)
            {
                DoctorList = new List<Doctor>();
                ICollection<Doctor> doctors = response.Content.ReadAsAsync<ICollection<Doctor>>().Result;
                foreach (Doctor doc in doctors)
                {
                    if (doc.IsAdmin == false)
                    {
                        DoctorList.Add(doc);
                    }
                }

            }
        }


        public async Task<IActionResult> OnPostRegisterAsync(string Name, string Password, string Email, string PhoneNumber, string Username)
        {
            Patient p = new Patient
            {
                Name = Name,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Username = Username,
                Password = Password

            };
            HttpClient client = _api.Initial();
            var myContent = JsonConvert.SerializeObject(p);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage responseMessage = await client.PostAsync("api/patient?MainDoctor="+MainDoctorId, byteContent);
            if (responseMessage.IsSuccessStatusCode)
            {

                client = _api.Initial();
                HttpResponseMessage response = client.GetAsync("api/doctor").Result;
                ViewData["Message"] = "Patient was created";
                newPatient = null;
                ModelState.Clear();
                if (response.IsSuccessStatusCode)
                {
                    DoctorList = new List<Doctor>();
                    ICollection<Doctor> doctors = await response.Content.ReadAsAsync<ICollection<Doctor>>();
                    foreach (Doctor doc in doctors)
                    {
                        if (doc.IsAdmin == false)
                        {
                            DoctorList.Add(doc);
                        }
                    }

                }
                return this.Page();
            }
            else
            {
                client = _api.Initial();
                HttpResponseMessage response = client.GetAsync("api/doctor").Result;
                ViewData["Error"] = "Username already exists";
                newPatient = null;
                ModelState.Clear();
                if (response.IsSuccessStatusCode)
                {
                    DoctorList = new List<Doctor>();
                    ICollection<Doctor> doctors = await response.Content.ReadAsAsync<ICollection<Doctor>>();
                    foreach (Doctor doc in doctors)
                    {
                        if (doc.IsAdmin == false)
                        {
                            DoctorList.Add(doc);
                        }
                    }

                }
                return this.Page();
            }

        }
    }

}


