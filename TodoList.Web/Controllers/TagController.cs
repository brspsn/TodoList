using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Repository.Shared.Abstract;

namespace TodoList.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            
            var result=_unitOfWork.Tags.GetAll().Select(x=> new
            {
                x.Name,
                count=x.ToDos.Count
            }).Where(a=>a.count>3).ToList();

            return Json(result);
        }
        public IActionResult Add(Tag tag)
        {
            _unitOfWork.Tags.Add(tag);
            _unitOfWork.Save();
            return Ok();
        }
        

    }
}
