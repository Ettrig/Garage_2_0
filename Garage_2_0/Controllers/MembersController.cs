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
    public class MembersController : Controller
    {
        private readonly Garage_2_0Context _context;

        public MembersController(Garage_2_0Context context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
                return View(await _context.Members
                    .Include(m => m.Vehicles)
                    .OrderBy(m => m.Name)
                    .ToListAsync());
        }

        public ActionResult IndexSort(string sortOrder)            // sort columns ascendiong/descending
        {
            ViewBag.NamnSortParm = String.IsNullOrEmpty(sortOrder) ? "Namn" : "";

            var members = from m in _context.Members select m;

            switch (sortOrder)
            {
                case "Namn":
                    members = members.OrderBy(m => m.Name);
                    break;
                    //Vi behöver också sortera på antalet fordon
            }
            return View(nameof(Index), members.Include(m => m.Vehicles).ToList());
        }

        // GET: RegNr                                           // sökning regnr, searchTerm innehåller sökvärdet, funktionen anropas från index.cshtml
        [HttpPost]
        public ActionResult Index(string searchTerm = null)
        {
            var model =
                _context.Members
                .OrderByDescending(m => m.Name)
                .Where(m => searchTerm == null || m.Name.StartsWith(searchTerm) || m.Name.Contains(searchTerm))
                .Include(m => m.Vehicles)
                .ToList();
            return View(model);
        }

        public ActionResult Index1(string columnToSort, Garage_2_0Context.MembersSortState sortState )            // sort columns ascendiong/descending
        {

            var members = from m in _context.Members select m;
            members = members.Include(m => m.Vehicles);

            if (columnToSort == "Namn")
            {
                if (sortState == Garage_2_0Context.MembersSortState.NamnAscend)
                {
                    members = members.OrderByDescending(m => m.Name);
                    ViewBag.sortState = Garage_2_0Context.MembersSortState.NamnDescend;
                }
                else
                {
                    members = members.OrderBy(m => m.Name);
                    ViewBag.sortState = Garage_2_0Context.MembersSortState.NamnAscend;
                }
            }
            else if (columnToSort == "Antal Fordon")
            {
                if (sortState == Garage_2_0Context.MembersSortState.AntalFordonAscend)
                {
                    members = members.OrderByDescending(m => m.Vehicles.Count);
                    ViewBag.sortState = Garage_2_0Context.MembersSortState.AntalFordonDescend;
                }
                else
                {
                    members = members.OrderBy(m => m.Vehicles.Count);
                    ViewBag.sortState = Garage_2_0Context.MembersSortState.AntalFordonAscend;
                }
            }
            return View(nameof(Index), members.ToList());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Vehicles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
