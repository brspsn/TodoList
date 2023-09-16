using Microsoft.AspNetCore.Mvc;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GetAll()
        {
            return Json(_context.Categories.ToList());
        }
    }
}
