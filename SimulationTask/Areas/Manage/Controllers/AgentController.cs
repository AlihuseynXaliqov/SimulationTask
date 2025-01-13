using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulationTask.Areas.Manage.Helper.DTOs;
using SimulationTask.DAL;
using SimulationTask.Helper.File;
using SimulationTask.Models;

namespace SimulationTask.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AgentController : Controller
    {
        AppDbContext AppDbContext;
        private readonly IWebHostEnvironment web;
        private readonly IMapper mapper;

        public AgentController(AppDbContext appDbContext,IWebHostEnvironment web,IMapper mapper)
        {
            AppDbContext = appDbContext;
            this.web = web;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAgentDto dto)
        {
            if (!dto.formFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("IFormFile", "Duzgun shekil formati daxil edin");
            }
            if (dto.formFile.Length >= 2097152)
            {
                ModelState.AddModelError("FormFile", "Shekilin olcusu max 2mb olmalidi");
                return View();
            }

            dto.ImageUrl = dto.formFile.Upload(web.WebRootPath, "Upload/Slider");


            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var position = mapper.Map<User>(dto);
            AppDbContext.Users.Add(position);
            AppDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }




    }
}

