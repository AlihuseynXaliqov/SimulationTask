using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulationTask.Areas.Manage.Helper.DTOs.Agent;
using SimulationTask.DAL;
using SimulationTask.Helper.Exception;
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

        public AgentController(AppDbContext appDbContext, IWebHostEnvironment web, IMapper mapper)
        {
            AppDbContext = appDbContext;
            this.web = web;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var agents = AppDbContext.Users.ToList();
            return View(agents);
        }

        public IActionResult Create()
        {
            ViewBag.Position = AppDbContext.Positions.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAgentDto dto)
        {
            ViewBag.Position = AppDbContext.Positions.ToList();

            if (dto.formFile == null)
            {
                ModelState.AddModelError("formFile", "Şəkil seçilməyib.");
                return View(dto);
            }


            if (!dto.formFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("formFile", "Düzgün şəkil formatı daxil edin.");
                return View(dto);
            }

            if (dto.formFile.Length >= 2097152)
            {
                ModelState.AddModelError("formFile", "Şəklin ölçüsü maksimum 2MB olmalıdır.");
                return View(dto);
            }

            dto.ImageUrl = dto.formFile.Upload(web.WebRootPath, "Upload/Agent");

            // ModelState yoxlaması
            /* if (!ModelState.IsValid)
             {
                 return View(dto);
             }*/

            var user = mapper.Map<User>(dto);
            AppDbContext.Users.Add(user);
            AppDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var aggent = AppDbContext.Users.FirstOrDefault(x => x.Id == id);
            AppDbContext.Users.Remove(aggent);
            AppDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Update(int id)
        {
            if (id <= 0)
            {
                throw new NegativeIdException();
            }
            ViewBag.Position = AppDbContext.Positions.ToList();
            var aggent = AppDbContext.Users.Include(x => x.Position).FirstOrDefault(x => x.Id == id);
            var newAggent = mapper.Map<UpdateAgentDto>(aggent);
            return View(newAggent);

        }

        [HttpPost]
        public IActionResult Update(UpdateAgentDto dto)
        {

            ViewBag.Position = AppDbContext.Positions.ToList();

           /* if (!ModelState.IsValid)
            {
                return View();
            }*/

            var aggent = AppDbContext.Users.FirstOrDefault(x => x.Id == dto.Id);
            if (dto.formFile != null)
            {
                if (!dto.formFile.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("formFile", "Düzgün şəkil formatı daxil edin.");
                    return View(dto);
                }

                if (dto.formFile.Length >= 2097152)
                {
                    ModelState.AddModelError("formFile", "Şəklin ölçüsü maksimum 2MB olmalıdır.");
                    return View(dto);
                }
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    FileExtension.DeleteFile(web.WebRootPath, "Upload/Agent", dto.ImageUrl);
                }
                aggent.ImageUrl = dto.formFile.Upload(web.WebRootPath, "Upload/Agent");
                
            }
           aggent.Name= dto.Name;
            aggent.positionId = dto.positionId;

            var agent=mapper.Map<User>(aggent);
            AppDbContext.Users.Update(aggent);
            AppDbContext.SaveChanges();
            return RedirectToAction("Index");



        }






    }
}

