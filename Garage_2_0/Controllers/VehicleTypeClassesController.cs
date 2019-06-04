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
    public class VehicleTypeClassesController : Controller
    {
        private readonly Garage_2_0Context _context;

        public VehicleTypeClassesController(Garage_2_0Context context)
        {
            _context = context;
        }

        // GET: VehicleTypeClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleTypeClass.ToListAsync());
        }

        // GET: VehicleTypeClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleTypeClass = await _context.VehicleTypeClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleTypeClass == null)
            {
                return NotFound();
            }

            return View(vehicleTypeClass);
        }

        // GET: VehicleTypeClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleTypeClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Price")] VehicleTypeClass vehicleTypeClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleTypeClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleTypeClass);
        }

        // GET: VehicleTypeClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleTypeClass = await _context.VehicleTypeClass.FindAsync(id);
            if (vehicleTypeClass == null)
            {
                return NotFound();
            }
            return View(vehicleTypeClass);
        }

        // POST: VehicleTypeClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Price")] VehicleTypeClass vehicleTypeClass)
        {
            if (id != vehicleTypeClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleTypeClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleTypeClassExists(vehicleTypeClass.Id))
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
            return View(vehicleTypeClass);
        }

        // GET: VehicleTypeClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleTypeClass = await _context.VehicleTypeClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleTypeClass == null)
            {
                return NotFound();
            }

            return View(vehicleTypeClass);
        }

        // POST: VehicleTypeClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleTypeClass = await _context.VehicleTypeClass.FindAsync(id);
            _context.VehicleTypeClass.Remove(vehicleTypeClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleTypeClassExists(int id)
        {
            return _context.VehicleTypeClass.Any(e => e.Id == id);
        }
    }
}
