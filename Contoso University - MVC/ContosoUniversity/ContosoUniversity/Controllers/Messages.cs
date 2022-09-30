using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    public class Messages : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
