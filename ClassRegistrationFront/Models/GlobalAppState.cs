using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ClassRegistrationFront.Models
{
    public class GlobalAppState
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }

        public void SaveToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            //var claimsIdentity = new ClaimsIdentity(jwtSecurityToken.Claims,
            //    CookieAuthenticationDefaults.AuthenticationScheme);
            Username = jwtSecurityToken.Claims.FirstOrDefault(p => p.Type == "username")?.Value ?? "";
            UserId = int.Parse(jwtSecurityToken.Claims.FirstOrDefault(p => p.Type == "yousefmubarak.id")?.Value ?? "0");
            Token = token;
        }
        public bool IsLoggedIn()
        {
            if(Token == null)
            {
                return false;
            }
            return true;
        }

        public HttpClient CreateClient()
        {
            var client = new HttpClient();
            if(Token != "")
            {
                client.DefaultRequestHeaders.Authorization =
                          new AuthenticationHeaderValue("Bearer", Token);
            }
            client.BaseAddress = new Uri("https://localhost:7288");

            return client;
        }
    }
}
