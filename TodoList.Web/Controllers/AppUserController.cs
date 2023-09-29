using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUnitOfWork _unitOfWork;

        public AppUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AppUser appUser)
        {
            AppUser user = _unitOfWork.AppUsers.GetAll(u => u.UserName == appUser.UserName && u.Password == appUser.Password).Include(u => u.UserType).First();



            if (user != null)
            {
                //eğer boyle bir kullanıcı varsa login işlemlerine baslayabiliriz.

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim(ClaimTypes.Role, user.UserType.Name));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties { IsPersistent = true });



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
            return Json(new { data = _unitOfWork.AppUsers.GetAll().Include(u => u.UserType).ToList() });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _unitOfWork.AppUsers.DeleteById(id);
            _unitOfWork.Save();

            return Ok();

        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(AppUser appUser)
        {
            _unitOfWork.AppUsers.Add(appUser);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(AppUser appUser)
        {
            _unitOfWork.AppUsers.Update(appUser);
            _unitOfWork.Save();
            return Ok();
        }

    }
}