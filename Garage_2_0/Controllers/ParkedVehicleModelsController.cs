using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_2_0.Models;

namespace Garage_2_0.Controllers
{
    public class ParkedVehicleModelsController : Controller
    {
        private readonly Garage_2_0Context _context;

        public ParkedVehicleModelsController(Garage_2_0Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicleModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkedVehicleModel.ToListAsync());
        }

        // GET: ParkedVehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await _context.ParkedVehicleModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicleModel == null)
            {
                return NotFound();
            }

            return View(parkedVehicleModel);
        }

        // GET: ParkedVehicleModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: ParkedVehicleModels/ParkVehicle
        public IActionResult ParkVehicle()
        {
            return View();
        }


        // POST: ParkedVehicleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,RegNr,Color,Brand,Model,NoWheels,FreeText,ParkedIn,ParkedOut")] ParkedVehicleModel parkedVehicleModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicleModel);
        }

        // POST: ParkedVehicleModels/ParkVehicle
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ParkVehicle([Bind("Id,Type,RegNr,Color,Brand,Model,NoWheels,FreeText")] ParkVehicleModel parkVehicleModel)
        {
            ParkedVehicleModel parkedVehicleModel = new ParkedVehicleModel();

            if (ModelState.IsValid)
            {
                parkedVehicleModel.Id = parkVehicleModel.Id;
                parkedVehicleModel.Type = parkVehicleModel.Type;
                parkedVehicleModel.RegNr = parkVehicleModel.RegNr;
                parkedVehicleModel.Color = parkVehicleModel.Color;
                parkedVehicleModel.Brand = parkVehicleModel.Brand;
                parkedVehicleModel.Model = parkVehicleModel.Model;
                parkedVehicleModel.NoWheels = parkVehicleModel.NoWheels;
                parkedVehicleModel.FreeText = parkVehicleModel.FreeText;
                parkedVehicleModel.ParkedIn = DateTime.Now;
                parkedVehicleModel.ParkedOut = DateTime.Parse("12/31/9999 23:59:59");

                _context.Add(parkedVehicleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkVehicleModel);
        }

        // GET: ParkedVehicleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await _context.ParkedVehicleModel.FindAsync(id);
            if (parkedVehicleModel == null)
            {
                return NotFound();
            }
            return View(parkedVehicleModel);
        }

        // POST: ParkedVehicleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,RegNr,Color,Brand,Model,NoWheels,FreeText,ParkedIn,ParkedOut")] ParkedVehicleModel parkedVehicleModel)
        {
            if (id != parkedVehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicleModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleModelExists(parkedVehicleModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicleModel);
        }

        // GET: ParkedVehicleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await _context.ParkedVehicleModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicleModel == null)
            {
                return NotFound();
            }

            return View(parkedVehicleModel);
        }

        // POST: ParkedVehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicleModel = await _context.ParkedVehicleModel.FindAsync(id);
            _context.ParkedVehicleModel.Remove(parkedVehicleModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleModelExists(int id)
        {
            return _context.ParkedVehicleModel.Any(e => e.Id == id);
        }
    }
}
