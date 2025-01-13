using Microsoft.AspNetCore.Mvc;
using SimulationTask.DAL;
using SimulationTask.Models;
using System.Diagnostics;

namespace SimulationTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext dbContext;

        public HomeController(ILogger<HomeController> logger,AppDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<User> users = dbContext.Users.ToList();
            ViewBag.Position = dbContext.Positions.ToList();
            return View(users);
        }

       
    }
}
