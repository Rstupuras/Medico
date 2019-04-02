using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MedicoWebAPP.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MedicoWebAPP.Pages.Login
{
    public class PatientModel : PageModel
    {
        
    private readonly WebAPI _api = new WebAPI();
        

    public async Task<IActionResult> OnPostLoginAsync(string username, string password) {
            Patient p = new Patient{
                Username = username,
                Password = password
            };

            HttpClient client = _api.Initial();
            var myContent = JsonConvert.SerializeObject(p);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage responseMessage = client.PostAsync("api/patient/login", byteContent).Result;
            
            if (responseMessage.IsSuccessStatusCode)
            {
                p = responseMessage.Content.ReadAsAsync<Patient>().Result;
                
                
                var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, p.ID.ToString()),
                new Claim(ClaimTypes.Role,"patient"),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            

            var principal = new ClaimsPrincipal(identity);


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return RedirectToPage("/Index");
            }
            else {
                ViewData["Error"] = "Invalid username or password.";
            }

            return this.Page(); 
        }
    }    
}

