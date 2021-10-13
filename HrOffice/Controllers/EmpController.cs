﻿using HrOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HrOffice.Controllers
{
    public class EmpController : Controller
    {
        private EmpContext _context;

        public EmpController(EmpContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index(string currentFilter,
            string searchString, int? pageNumber)
        {
            //return View(await _context.Employee.ToListAsync());

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            /*session managment using 3 types session, view data/ view bag , & tempdata as below */
            //HttpContext.Session.Set("CurrentFilter", searchString)
            //HttpContext.Session.TryGetValue
            //ViewData["CurrentFilter"] = searchString; 
            //ViewData["CurrentFilter"] = searchString;
            //TempData.Add()

            _context.Database.GetDbConnection().ChangeDatabase("hrdbdev");
            var emps = from e in _context.Employee
                       select e;

            if (!String.IsNullOrEmpty(searchString))
                emps = emps.Where(e => e.Name.Contains(searchString));

            int pageSize = 5;
            return View(await PaginatedList<Emp>.CreateAsync(emps, pageNumber ?? 1, pageSize));
        }

        //GET : Employee/ create
        public IActionResult AddOrEdit(int id = 0)
        {
            _context.Database.GetDbConnection().ChangeDatabase("hrdbdev");

            if (id == 0)
                return View(new Emp());
            else
                return View(_context.Employee.Find(id));
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("EmpId,Name,EmailId,Department,Location")] Emp emp)
        {
            _context.Database.GetDbConnection().ChangeDatabase("hrdbdev");
            if (ModelState.IsValid)
            {
                if (emp.EmpId == 0)
                    _context.Add(emp);
                else
                    _context.Update(emp);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            _context.Database.GetDbConnection().ChangeDatabase("hrdbdev");
            var emp = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
