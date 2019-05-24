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

        //GET: ParkedVehicleModels
        public async Task<IActionResult> Index()
        {
            var m = await _context.Vehicles.ToListAsync();
            return View(m);
        }
       
        // GET: ParkedVehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await _context.Vehicles
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
        public async Task<IActionResult> Create([Bind("Id,Type,RegNr,Color,Brand,Model,NoWheels,FreeText,ParkedIn,ParkedOut")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // POST: ParkedVehicleModels/ParkVehicle
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ParkVehicle([Bind("Id,Type,RegNr,Color,Brand,Model,NoWheels,FreeText")] ParkVehicleViewModel parkVehicleModel)
        {
            Vehicle vehicle = new Vehicle();

            Vehicle sameRegNr = _context.Vehicles.FirstOrDefault(v => v.RegNr == parkVehicleModel.RegNr); 
            if (ModelState.IsValid && sameRegNr==null)
            {
                vehicle.Id = parkVehicleModel.Id;
                vehicle.Type = parkVehicleModel.Type;
                vehicle.RegNr = parkVehicleModel.RegNr;
                vehicle.Color = parkVehicleModel.Color;
                vehicle.Brand = parkVehicleModel.Brand;
                vehicle.Model = parkVehicleModel.Model;
                vehicle.NoWheels = parkVehicleModel.NoWheels;
                vehicle.FreeText = parkVehicleModel.FreeText;
                vehicle.ParkedIn = DateTime.Now;
                vehicle.ParkedOut = DateTime.Parse("9999-12-31");

                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Provide error feed-back
            return View(parkVehicleModel);
        }

        // GET: ParkedVehicleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await _context.Vehicles.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,RegNr,Color,Brand,Model,NoWheels,FreeText,ParkedIn,ParkedOut")] Vehicle parkedVehicleModel)
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

        // POST/GET??: ParkedVehicleModels/CheckOut/5
        public async Task<IActionResult> CheckOut(int id)
        {
            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.ParkedOut = DateTime.Now; 

            try
            {
                _context.Update(vehicle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkedVehicleModelExists(vehicle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return View(vehicle);
        }

        // GET: ParkedVehicleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await _context.Vehicles
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
            var parkedVehicleModel = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(parkedVehicleModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleModelExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
