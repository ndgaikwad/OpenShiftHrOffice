using HrOffice.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HrOffice.Controllers
{
    public class SubscriptionController : Controller
    {
        private EmpContext _context;

        public SubscriptionController(EmpContext context)
        {
            //context.Database.GetDbConnection().ChangeDatabase(ViewData["dbName"].ToString());
            _context = context;

        }

        // GET: Subscription
        public async Task<IActionResult> Index(string currentFilter,
            string searchString, int? pageNumber)
        {

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["SubCurrentFilter"] = searchString;
            var subs = from e in _context.Subscriptions
                       select e;

            if (!String.IsNullOrEmpty(searchString))
                subs = subs.Where(e => e.UserName.Contains(searchString));

            int pageSize = 5;
            return View(await PaginatedList<Subscription>.CreateAsync(subs, pageNumber ?? 1, pageSize));
        }

        //GET : Subscription/ create
        public IActionResult NewOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Subscription());
            else
                return View(_context.Subscriptions.Find(id));
        }

        // POST: Subscription/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("SubscriptionId,CompanyName,UserName,EmailId,UserPassword,DBName,Role")] Subscription sub)
        {
            if (ModelState.IsValid)
            {
                if (sub.SubscriptionId == 0)
                    _context.Add(sub);
                else
                    _context.Update(sub);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sub);
        }

        // GET: Subscription/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var sub = await _context.Subscriptions.FindAsync(id);
            _context.Subscriptions.Remove(sub);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
