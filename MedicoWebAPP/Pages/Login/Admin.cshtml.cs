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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MedicoWebAPP.Pages.Login
{
    public class AdminModel : PageModel
    {
        private readonly WebAPI _api = new WebAPI();

        public async Task<IActionResult> OnPostLoginAsync(string username, string password)
        {
            Doctor d = new Doctor
            {
                Username = username,
                Password = password
            };

            HttpClient client = _api.Initial();
            var myContent = JsonConvert.SerializeObject(d);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage responseMessage = client.PostAsync("api/doctor/login", byteContent).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                d = responseMessage.Content.ReadAsAsync<Doctor>().Result;
                if (d.IsAdmin == true)
                {
                    var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, d.ID.ToString()),
            new Claim(ClaimTypes.Role,"admin"),
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                    var principal = new ClaimsPrincipal(identity);


                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);
                    return RedirectToPage("/Index");
                }
                else
                {
                    ViewData["Error"] = "Invalid username or password.";
                    return this.Page();
                }




            }
            else
            {
                ViewData["Error"] = "Invalid username or password.";
                return this.Page();
            }
            
        }
    }
}