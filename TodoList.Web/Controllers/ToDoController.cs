using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Web.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            _context.ToDos.Remove(_context.ToDos.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        public IActionResult GetAll() 
        {
            return Json( _context.ToDos.Include(t => t.Category).Where(t => t.IsActive==true).ToList());
        }
        public IActionResult SetIsActive(int id)
        {
            ToDo todo = _context.ToDos.Find(id);
            todo.IsActive = false;
            _context.ToDos.Update(todo);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public IActionResult Add()
        {
            CategoryController categoryController = new CategoryController(_context);
            ViewData["categories"] = categoryController.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Add(ToDo todo)
        {
            _context.ToDos.Add(todo);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}
