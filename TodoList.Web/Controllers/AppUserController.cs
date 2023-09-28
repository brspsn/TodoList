using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoList.Data;
using TodoList.Models;
using TodoList.Repository.Shared.Abstract;

namespace TodoList.Web.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IRepository<AppUser> _repo;
        public AppUserController(IRepository<AppUser> repo)
        {
            _repo= repo;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AppUser appUser)
        {
            AppUser user = _repo.GetAll(u => u.UserName == appUser.UserName && u.Password == appUser.Password).Include(u => u.UserType).First();

            if (user != null)
            {
                //eğer  byle bir kulanıcı varsa login işlemleine başlayabiliriz.

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));

                claims.Add(new Claim(ClaimTypes.Role, user.UserType.Name));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

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

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Json(new { data = _repo.GetAll(u => u.IsDeleted == false).Include(u => u.UserType).ToList() });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _repo.DeleteById(id);
            _repo.Save();
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(AppUser appUser)
        {
            _repo.Add(appUser);
            _repo.Save();
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(AppUser appUser)
        {
            _repo.Update(appUser);
            _repo.Save();
            return Ok();
        }



    }

}