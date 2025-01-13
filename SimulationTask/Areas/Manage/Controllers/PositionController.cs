using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using SimulationTask.Areas.Manage.Helper.DTOs;
using SimulationTask.Areas.Manage.Helper.DTOs.Position;
using SimulationTask.DAL;
using SimulationTask.Models;

namespace SimulationTask.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        AppDbContext appContext;
        private readonly IMapper mapper;

        public PositionController(AppDbContext appContext, IMapper mapper)
        {
            this.appContext = appContext;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var positions = appContext.Positions.ToList();
            return View(positions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatePositionDto dto)
        {
            var position = mapper.Map<Position>(dto);
            appContext.Add(position);
            appContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            if (id == null) return NotFound("Tapilmadi");
            var position = appContext.Positions.FirstOrDefault(x => x.Id == id);
            return View(position);

        }

        [HttpPost]
        public IActionResult Update(UpdatePositionDto dto)
        {
            if (!ModelState.IsValid) return View();
            var oldPosition = appContext.Positions.FirstOrDefault(x=>x.Id == dto.Id);

            oldPosition.PositionName = dto.PositionName;
            appContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete (int id)
        {
            if (!ModelState.IsValid) return View();
            var oldPosition = appContext.Positions.FirstOrDefault(x => x.Id == id);
            appContext.Remove(oldPosition);
            appContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }




    }
}
