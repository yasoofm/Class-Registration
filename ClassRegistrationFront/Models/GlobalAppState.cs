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
        public bool IsLoggedIn  => Token != null && Token != "";
        public bool IsAdmin { get; set; } = false;

        public void SaveToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            //var claimsIdentity = new ClaimsIdentity(jwtSecurityToken.Claims,
            //    CookieAuthenticationDefaults.AuthenticationScheme);
            Username = jwtSecurityToken.Claims.FirstOrDefault(p => p.Type == "username")?.Value ?? "";
            UserId = int.Parse(jwtSecurityToken.Claims.FirstOrDefault(p => p.Type == "yousefmubarak.Id")?.Value ?? "0");
            if (jwtSecurityToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value == "Admin")
            {
                IsAdmin = true;
            }
            Token = token;
        }

        public void RemoveToken()
        {
            Token = "";
            IsAdmin = false;
        }

        public HttpClient CreateClient()
        {
            var client = new HttpClient();
            if(Token != "" || Token != null)
            {
                client.DefaultRequestHeaders.Authorization =
                          new AuthenticationHeaderValue("Bearer", Token);
            }
            client.BaseAddress = new Uri("https://localhost:7288");

            return client;
        }
    }
}
