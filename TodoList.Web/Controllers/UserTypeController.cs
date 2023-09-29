using Microsoft.AspNetCore.Mvc;
using TodoList.Data;
using TodoList.Models;
using TodoList.Repository.Shared.Abstract;

namespace TodoList.Web.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult GetAll()
        {
            return Json(_unitOfWork.UserTypes.GetAll().ToList());
        }
    }
}