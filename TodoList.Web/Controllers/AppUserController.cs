using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Web.Controllers
{
    public class AppUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AppUserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AppUser appUser)
        {
            AppUser user = _context.Users.FirstOrDefault(u => u.UserName == appUser.UserName && u.Password == appUser.Password);

            if (user != null)
            {
                //eğer  byle bir kulanıcı varsa login işlemleine başlayabiliriz.

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                if (user.IsAdmin)
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                else
                    claims.Add(new Claim(ClaimTypes.Role, "User"));


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Ok();
            }
            else
            {
                return BadRequest();
                //böyle bir kullanıcı YOK hatası döndürebiliriz.
            }
        }
    }
}
