using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login(AppUser Appuser)
        {
            AppUser user= _context.Users.FirstOrDefault(u => u.UserName == Appuser.UserName && u.Password == Appuser.Password);
            if (user!= null)
            {
                //eger böyle bir kullanıcı varsa login işlemlerinize başlayabiliriz.
                return RedirectToAction("Index","Home");
            }
            else
            {
                return BadRequest();
                //böyle bir kullanıcı yok hatası döndürebbiliriz
            }
        }
    }
}
