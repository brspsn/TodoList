using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoList.Data;
using TodoList.Models;
using TodoList.Repository.Abstract;
using TodoList.Repository.Shared.Abstract;

namespace TodoList.Web.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToDoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            _unitOfWork.ToDos.DeleteById(id);
            _unitOfWork.Save();

            return Ok();

        }

        public IActionResult GetAll()
        {


            return Json(_unitOfWork.ToDos.GetAll(t => t.IsActive == true && t.UserId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).Include(t => t.Category).ToList());
        }

        [HttpPost]
        public IActionResult SetIsActive(int id)
        {


            _unitOfWork.ToDos.SetIsActive(id);
            _unitOfWork.Save();

            return Ok();
        }


        [HttpPost]
        public IActionResult Add(ToDo todo)
        {

            _unitOfWork.ToDos.Add(todo);
            _unitOfWork.Save();



            return Ok();

        }
    }
}