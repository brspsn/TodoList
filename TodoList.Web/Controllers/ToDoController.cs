using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Claims;
using TodoList.Data;
using TodoList.Models;
using TodoList.Repository.Abstract;
using TodoList.Repository.Shared.Abstract;

namespace TodoList.Web.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ITodoRepositorty _repo;

        public ToDoController(ITodoRepositorty repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _repo.DeleteById(id);
            _repo.save();

            return Ok();

        }

        public IActionResult GetAll()
        {

            //return Json(_context.ToDos.Include(t => t.Category).Where(t => t.IsActive == true&& t.UserId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToList());

            return Json(_repo.GetAll(t => t.IsActive == true && t.UserId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).Include(t=>t.Category).ToList());
        }

        [HttpPost]
        public IActionResult SetIsActive(int id)
        {
            //ToDo todo = _context.ToDos.Find(id);
            //todo.IsActive = false;
            //_context.ToDos.Update(todo);
            //_context.SaveChanges();

            _repo.SetIsActive(id);
            _repo.save();

            return Ok();
        }

        [HttpPost]
        public IActionResult Add(ToDo todo)
        {
            //todo.UserId=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //_context.ToDos.Add(todo);
            //_context.SaveChanges();
            //return RedirectToAction("Index","Home");

            _repo.Add(todo);
            _repo.save();

            return Ok();

        }
    }
}
