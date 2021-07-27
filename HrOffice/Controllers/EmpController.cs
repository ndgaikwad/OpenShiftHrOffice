using HrOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HrOffice.Controllers
{
    public class EmpController : Controller
    {
        private readonly EmpContext _context;

        public EmpController(EmpContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }

        //GET : Employee/ create
        public IActionResult AddOrEdit(int id = 0)
        {
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
            var emp = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
