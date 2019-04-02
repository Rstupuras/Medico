using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace MedicoWebAPP.Pages.Doc.Appointments
{
    [Authorize(Policy = "mustbedoctor")]
    public class ViewModel : PageModel
    {
        private readonly WebAPI _api = new WebAPI();
        [BindProperty]
        public Appointment appointment { get; set; }
        public SelectList medicaments{ get; set;}
        public SelectList doctors { get; set; }
        [BindProperty]
        public Prescription prescription { get; set; }
        [BindProperty]
        public Appointment newAppointment { get; set; }

        public IAuthorizationService AuthorizationService {get; set;}
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
        public void OnGet(int? id)
        {
            HttpClient client = _api.Initial();
            var sid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                   .Select(c => c.Value).SingleOrDefault();
            int ID = Int32.Parse(sid.ToString());
            HttpResponseMessage responseMessage = client.GetAsync("api/appointment/" + id).Result;
            appointment = responseMessage.Content.ReadAsAsync<Appointment>().Result;
            
            HttpResponseMessage Message = client.GetAsync("api/medicament/").Result;
            ICollection<Medicament> med = new HashSet<Medicament>();
            foreach (Medicament m in Message.Content.ReadAsAsync<ICollection<Medicament>>().Result)
            {
                if (m.IsPrescribed == true)
                {
                    med.Add(m);
                }
            }
            medicaments = new SelectList(med, "", "Name");
            HttpResponseMessage mess = client.GetAsync("api/doctor/").Result;
            ICollection<Doctor> doct = new HashSet<Doctor>();
            foreach (Doctor d in mess.Content.ReadAsAsync<ICollection<Doctor>>().Result)
            {
                if (d.IsAdmin==false)
                {
                    doct.Add(d);
                }
                
            }
            doctors = new SelectList(doct, "", "Name");
        }
        public async Task<IActionResult> OnPostAppointment(int? id)
        {
            Appointment app = new Appointment();

            HttpClient client = _api.Initial();
            HttpResponseMessage Message = await client.GetAsync("api/appointment/" + id);
            app = Message.Content.ReadAsAsync<Appointment>().Result;
            if (appointment.Summary=="" || appointment.Summary ==null)
            {
                return RedirectToPage("/Error");
            }
            else {
                app.Summary = appointment.Summary;
                app.IsViewed= true;
                
            
            
            var myContent = JsonConvert.SerializeObject(app);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Message = await client.PutAsync("api/appointment/" + id, byteContent);



            if (Message.IsSuccessStatusCode)
            {
                return RedirectToPage("/Doc/Appointments");
            }
            else
            {
                return RedirectToPage("/Error");
            };
            }
            

        }
        public async Task<IActionResult> OnPostGivePrescription(int? id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage responseMessage = await client.GetAsync("api/medicament/");
            Medicament medicament = new Medicament();
            Console.WriteLine(prescription.Description);
            foreach (Medicament m in responseMessage.Content.ReadAsAsync<ICollection<Medicament>>().Result)
            {
                if (m.Name == prescription.Medicament.Name)
                {
                    if (m.IsPrescribed == true)
                    {
                        medicament = m;
                    }
                }
            }


            responseMessage = await client.GetAsync("api/appointment/" + id);
            Appointment app = responseMessage.Content.ReadAsAsync<Appointment>().Result;
            
            Prescription p = new Prescription {
                DateTimeFrom = prescription.DateTimeFrom,
                DateTimeTo = prescription.DateTimeTo,
                Description = prescription.Description
            };
            var myContent = JsonConvert.SerializeObject(p);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            responseMessage = await client.PostAsync("api/prescription/" + app.PatientID+"?doctor="+app.DoctorID+"&medicament="+medicament.ID, byteContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                responseMessage = await client.GetAsync("api/medicament/");
                ICollection<Medicament> med = new HashSet<Medicament>();
                foreach (Medicament m in responseMessage.Content.ReadAsAsync<ICollection<Medicament>>().Result)
                {
                    if (m.IsPrescribed == true)
                    {
                        med.Add(m);
                    }
                }
                responseMessage = await client.GetAsync("api/doctor/");
                ICollection<Doctor> doct = new HashSet<Doctor>();
                foreach (Doctor d in responseMessage.Content.ReadAsAsync<ICollection<Doctor>>().Result)
                {
                    if (d.IsAdmin == false)
                    {
                        doct.Add(d);
                    }
                }
                doctors = new SelectList(doct, "", "Name");
                ViewData["Message"] = "Prescription was added";
                medicaments = new SelectList(med, "", "Name");
                this.appointment = app;
               return this.Page();
            }
            else
            {
               return RedirectToPage("/Appointments/Error");
            }
            
        
        }
        public IActionResult OnPostBookAppointment(int? id)
        {
            int DoctorID = 0;
            int PatientID = 0;
            HttpClient client = _api.Initial();
            HttpResponseMessage mess = client.GetAsync("api/doctor/").Result;
            ICollection<Doctor> doct = new HashSet<Doctor>();
            foreach (Doctor d in mess.Content.ReadAsAsync<ICollection<Doctor>>().Result)
            {
                if (d.Name == newAppointment.Doctor.Name)
                {
                    DoctorID = d.ID;
                }
            }
            HttpResponseMessage Message = client.GetAsync("api/appointment/" + id).Result;
            Appointment app = Message.Content.ReadAsAsync<Appointment>().Result;
            PatientID = app.PatientID;
            Appointment appointment = new Appointment
            {
                DateTime = newAppointment.DateTime,
                Reason = newAppointment.Reason
            };
            var myContent = JsonConvert.SerializeObject(appointment);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage responseMessage = client.PostAsync("api/appointment?Doctor="+DoctorID+"&Patient="+PatientID,byteContent).Result;

            if(responseMessage.IsSuccessStatusCode)
            {
                HttpResponseMessage mes = client.GetAsync("api/medicament/").Result;
                ICollection<Medicament> med = new HashSet<Medicament>();
                foreach (Medicament m in mes.Content.ReadAsAsync<ICollection<Medicament>>().Result)
                {
                    if (m.IsPrescribed == true)
                    {
                        med.Add(m);
                    }
                }
                HttpResponseMessage me = client.GetAsync("api/doctor/").Result;
                ICollection<Doctor> doc = new HashSet<Doctor>();
                foreach (Doctor d in me.Content.ReadAsAsync<ICollection<Doctor>>().Result)
                {
                    if (d.IsAdmin == false)
                    {
                        doct.Add(d);
                    }
                }
                doctors = new SelectList(doc, "", "Name");
                ViewData["Message"] = "Appointment is booked";
                medicaments = new SelectList(med, "", "Name");
                this.appointment = app;
                return this.Page();
            }
            else
            {
                return RedirectToPage("/Appointments/Error");
            }



        }

    }
}