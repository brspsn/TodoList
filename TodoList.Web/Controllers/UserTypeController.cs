using Microsoft.AspNetCore.Mvc;
using TodoList.Data;

namespace TodoList.Web.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GetAll()
        {
            return Json(_context.UserTypes.ToList());
        }
    }
}
