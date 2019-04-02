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
    public class BookAppointmentModel : PageModel
    {
        private readonly WebAPI _api = new WebAPI();
        public SelectList doctors { get; set; }
        [BindProperty]
        public Appointment newAppointment { get; set; }
        [BindProperty]
        public Doctor doctor { get; set; }
        public void OnGet()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage mess = client.GetAsync("api/doctor/").Result;
            ICollection<Doctor> doct = new HashSet<Doctor>();
            foreach (Doctor d in mess.Content.ReadAsAsync<ICollection<Doctor>>().Result)
            {
                if (d.IsAdmin == false)
                {
                    doct.Add(d);
                }
            }
            doctors = new SelectList(doct, "ID", "Name");
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPostBookAppointment()
        {
            int DoctorID = 0;
            int PatientID = 0;
            DoctorID = doctor.ID;
            Console.WriteLine(doctor.ID);

            HttpClient client = _api.Initial();
            HttpResponseMessage mess = client.GetAsync("api/doctor/").Result;
            ICollection<Doctor> doct = new HashSet<Doctor>();
            foreach (Doctor d in mess.Content.ReadAsAsync<ICollection<Doctor>>().Result)
            {
                if (d.ID == doctor.ID)
                {
                    if (d.IsAdmin == false)
                    {
                        doct.Add(d);
                    }
                }
            }
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();
            PatientID = Int32.Parse(sid.ToString());
            Appointment appointment = new Appointment
            {
                DateTime = newAppointment.DateTime,
                Reason = newAppointment.Reason
            };
            var myContent = JsonConvert.SerializeObject(appointment);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage responseMessage = await client.PostAsync("api/appointment?Doctor=" + DoctorID + "&Patient=" + PatientID, byteContent);

            
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewData["Message"] = "Appointment was succesfully booked";
                responseMessage = await client.GetAsync("api/doctor/");
                doct = new HashSet<Doctor>();
                ModelState.Clear();
                foreach (Doctor d in responseMessage.Content.ReadAsAsync<ICollection<Doctor>>().Result)
                {
                    if (d.IsAdmin == false)
                    {
                        doct.Add(d);
                    }
                }
                doctor = new Doctor();
                doctors = new SelectList(doct, "ID", "Name");
                return this.Page();
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}