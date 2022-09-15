using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class MenuController : Controller
    {
        //Takes Data from Database using builder.Services from Program.cs

        private readonly RestaurantContext _context;
        public MenuController(RestaurantContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Menu> objMenuList = _context.Menus;
            return View(objMenuList);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Menu obj)

        {
            _context.Menus.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}