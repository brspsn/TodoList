using Microsoft.AspNetCore.Mvc;
using TodoList.Data;
using TodoList.Models;
using TodoList.Repository.Shared.Abstract;

namespace TodoList.Web.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly IRepository<UserType> _repo;

        public UserTypeController(IRepository<UserType> repo)
        {
            _repo = repo;
        }

        public IActionResult GetAll()
        {
            return Json(_repo.GetAll());
        }
    }
}
