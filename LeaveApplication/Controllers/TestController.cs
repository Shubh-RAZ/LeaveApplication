using LeaveApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveApplication.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var data = new TestViewModel
            {
                Name = "Shubham",
                DateOfBirth = new DateTime(1999,10,26)
            };
            return View(data);
        }
    }
}
