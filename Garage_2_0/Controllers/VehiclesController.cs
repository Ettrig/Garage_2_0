using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_2_0.Models;
using Garage_2_0.ViewModels;

namespace Garage_2_0.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage_2_0Context _context;

        public VehiclesController(Garage_2_0Context context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var model = await _context.Vehicles.Include(v => v.VehicleTypeClass).Select(v => new VehiclesViewModel
            {
                Id = v.Id,
                MemberName = v.Member.Name,
                Brand = v.Brand,
                Color = v.Color,
                Model = v.Model,
                ParkingTime = Extensions.VehicleExtension.ParkingTime(v),
                NoWheels = v.NoWheels,
                RegNr = v.RegNr,
                VehicleTypeClass = v.VehicleTypeClass,
                SearchTerm = ""
            }).OrderBy( v => v.RegNr ).ToListAsync();

            ViewBag.sortState = Garage_2_0Context.VehiclesSortState.RegNrAscend;

            return View(model);

        }

        public ActionResult Index1(string columnToSort, Garage_2_0Context.VehiclesSortState sortState)            // sort columns ascendiong/descending
        {
            var model = _context.Vehicles.Include(v => v.VehicleTypeClass).Select(v => new VehiclesViewModel
            {
                Id = v.Id,
                MemberName = v.Member.Name,
                Brand = v.Brand,
                Color = v.Color,
                Model = v.Model,
                ParkingTime = Extensions.VehicleExtension.ParkingTime(v),
                NoWheels = v.NoWheels,
                RegNr = v.RegNr,
                VehicleTypeClass = v.VehicleTypeClass,
                SearchTerm = ""
            });

            switch (columnToSort)
            {
                case "Member":
                    if (sortState == Garage_2_0Context.VehiclesSortState.MemberAscend)
                    {
                        model = model.OrderByDescending(v => v.MemberName);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.MemberDescend;
                    }
                    else
                    {
                        model = model.OrderBy(v => v.MemberName);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.MemberAscend;
                    }
                    break;
                case "Type":
                    if (sortState == Garage_2_0Context.VehiclesSortState.TypeAscend)
                    {
                        model = model.OrderByDescending(v => v.VehicleTypeClass.Type);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.TypeDescend;
                    }
                    else
                    {
                        model = model.OrderBy(v => v.VehicleTypeClass.Type);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.TypeAscend;
                    }
                    break;
                case "RegNr":
                    if (sortState == Garage_2_0Context.VehiclesSortState.RegNrAscend)
                    {
                        model = model.OrderByDescending(v => v.RegNr);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.RegNrDescend;
                    }
                    else
                    {
                        model = model.OrderBy(v => v.RegNr);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.RegNrAscend;
                    }
                    break;
                case "Color":
                    if (sortState == Garage_2_0Context.VehiclesSortState.ColorAscend)
                    {
                        model = model.OrderByDescending(v => v.Color);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.ColorDescend;
                    }
                    else
                    {
                        model = model.OrderBy(v => v.Color);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.ColorAscend;
                    }
                    break;
                case "Brand":
                    if (sortState == Garage_2_0Context.VehiclesSortState.BrandAscend)
                    {
                        model = model.OrderByDescending(v => v.Brand);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.BrandDescend;
                    }
                    else
                    {
                        model = model.OrderBy(v => v.Brand);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.BrandAscend;
                    }
                    break;
                case "Model":
                    if (sortState == Garage_2_0Context.VehiclesSortState.ModelAscend)
                    {
                        model = model.OrderByDescending(v => v.Model);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.ModelDescend;
                    }
                    else
                    {
                        model = model.OrderBy(v => v.Model);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.ModelAscend;
                    }
                    break;
                case "NoWheels":
                    if (sortState == Garage_2_0Context.VehiclesSortState.NoWheelsAscend)
                    {
                        model = model.OrderByDescending(v => v.NoWheels);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.NoWheelsDescend;
                    }
                    else
                    {
                        model = model.OrderBy(v => v.NoWheels);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.NoWheelsAscend;
                    }
                    break;
                case "ParkingTime":
                    if (sortState == Garage_2_0Context.VehiclesSortState.ParkingTimeAscend)
                    {
                        model = model.OrderByDescending(v => v.ParkingTime);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.ParkingTimeDescend;
                    }
                    else
                    {
                        model = model.OrderBy(v => v.ParkingTime);
                        ViewBag.sortState = Garage_2_0Context.VehiclesSortState.ParkingTimeAscend;
                    }
                    break;
            }   
            return View(nameof(Index), model.ToList());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            var members = from m in _context.Members select m;
            ViewBag.MemberSelectList = members.OrderBy(m=>m.Name).Select(m => new SelectListItem { Selected = false, Text = m.Name, Value = m.Id.ToString() }).ToList();

            var vehicleTypes = from t in _context.VehicleTypeClass select t;
            ViewBag.VehicleTypeSelectList = vehicleTypes.Select(t => new SelectListItem { Selected = false, Text = t.Type, Value = t.Id.ToString() }).ToList();

            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNr,Color,Brand,Model,NoWheels,MemberId,VehicleTypeClassId")] Vehicle vehicle)
        {
            if (RegNoIsParked(vehicle.RegNr))
                ModelState.AddModelError("RegNr", "Det finns redan ett fordon med det här registreringsnumret i garaget");

            if (ModelState.IsValid)
            {
                vehicle.ParkedIn = DateTime.Now;
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var members = from m in _context.Members select m;
            ViewBag.MemberSelectList = members.OrderBy(m => m.Name).Select(m => new SelectListItem { Selected = false, Text = m.Name, Value = m.Id.ToString() }).ToList();
            var vehicleTypes = from t in _context.VehicleTypeClass select t;
            ViewBag.VehicleTypeSelectList = vehicleTypes.Select(t => new SelectListItem { Selected = false, Text = t.Type, Value = t.Id.ToString() }).ToList();
            return View(vehicle);
        }

        private bool RegNoIsParked(object regNr)
        {
            throw new NotImplementedException();
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNr,Color,Brand,Model,NoWheels,ParkedIn,MemberId,VehicleTypeClassId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
        private bool RegNoIsParked(string license)
        {
            return _context.Vehicles.Any(v => v.RegNr == license);
        }
    }
}
