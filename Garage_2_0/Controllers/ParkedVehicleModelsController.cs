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
        private readonly Garage_2_0Context db;

        public ParkedVehicleModelsController(Garage_2_0Context context)
        {
            db = context;
        }

        // GET: RegNr                                           // sökning regnr, searchTerm innehåller sökvärdet, funktionen anropas från index.cshtml
        [HttpPost]
        public ActionResult Index(string searchTerm = null)
        {
            var model =
                db.Vehicles
                .OrderByDescending(v => v.RegNr)
                .Where(v => searchTerm == null || v.RegNr.StartsWith(searchTerm) || v.RegNr.Contains(searchTerm))
                .ToList();
            return View(model);
        }

        public ActionResult Index1(string sortOrder)            // sort columns ascendiong/descending
        {
            ViewBag.FordonstypSortParm = String.IsNullOrEmpty(sortOrder) ? "Fordonstyp" : "";
            ViewBag.RegnrSortParm = String.IsNullOrEmpty(sortOrder) ? "Regnr" : "";
            ViewBag.ColorSortParm = String.IsNullOrEmpty(sortOrder) ? "Color" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var vehicles = from v in db.Vehicles
                           select v;
            switch (sortOrder)
            {
                case "Fordonstyp":
                    vehicles = vehicles.OrderBy(v => v.Type);
                    break;
                case "Regnr":
                    vehicles = vehicles.OrderBy(v => v.RegNr);
                    break;
                case "Color":
                    vehicles = vehicles.OrderBy(v => v.Color);
                    break;
                default:
                    vehicles = vehicles.OrderBy(v => v.ParkedIn);
                    break;
            }
            return View(nameof(Index), vehicles.ToList());
        }

        //GET: ParkedVehicleModels
        public async Task<IActionResult> Index()
        {
            var m = await db.Vehicles.ToListAsync();
            return View(m);
        }
       
        // GET: ParkedVehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await db.Vehicles
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
                db.Add(vehicle);
                await db.SaveChangesAsync();
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

            if (RegNoIsParked(parkVehicleModel.RegNr))
                ModelState.AddModelError("RegNr", "Det finns redan ett fordon med det här registreringsnumret i garaget");

            if (ModelState.IsValid) 
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
                vehicle.ParkedOut = DateTime.Parse("9999-12-31"); // That is: has not checked out

                db.Add(vehicle);
                await db.SaveChangesAsync();
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

            var parkedVehicleModel = await db.Vehicles.FindAsync(id);
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
                    db.Update(parkedVehicleModel);
                    await db.SaveChangesAsync();
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

        // ParkedVehicleModels/CheckOut/5
        public async Task<IActionResult> CheckOut(int id)
        {
            var vehicle = await db.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var price = await db.Prices
                .FirstOrDefaultAsync(p => p.Type == vehicle.Type);
            if (price == null)
            {
                return NotFound();
            }

            vehicle.ParkedOut = DateTime.Now; 

            try
            {
                db.Update(vehicle);
                await db.SaveChangesAsync();
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

            var receipt = new ReceiptViewModel();
            receipt.Price = price.Price;
            receipt.ParkedIn = vehicle.ParkedIn;
            receipt.ParkedOut = DateTime.Now;
            receipt.RegNr = vehicle.RegNr; 
            receipt.Cost = (int) Math.Round((DateTime.Now - vehicle.ParkedIn).TotalHours * price.Price); 
            return View(receipt);
        }

        // GET: ParkedVehicleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await db.Vehicles
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
            var parkedVehicleModel = await db.Vehicles.FindAsync(id);
            db.Vehicles.Remove(parkedVehicleModel);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegNoIsParked( string license )
        {
            return db.Vehicles.Any(v => v.RegNr == license && v.ParkedOut == DateTime.Parse("9999-12-31"));
        }

        private bool ParkedVehicleModelExists(int id) // Based on old model name, accepts both parked and removed vehicles
        {
            return db.Vehicles.Any(e => e.Id == id);
        }
    }
}
