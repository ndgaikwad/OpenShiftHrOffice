using HrOffice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HrOffice.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmpContext _context;

        public LoginController(EmpContext context)
        {

            _context = context;

        }
        public IActionResult Login()
        {
            return View();
        }

        public List<Subscription> PutValue()
        {
            var subs = from e in _context.Subscriptions
                       select e;

            var users = subs.ToList();
            //{


            //new UserModel{EmailId="nil@test.com",UserName="nilesh",UserPassword="abc123",CompanyName = "OgilvyHealth", DBName = "hrdbdev", Role = "Admin"},
            //    new UserModel{EmailId="ravi@test.com",UserName="ravi",UserPassword="abc123",CompanyName = "OgilvyHealth", DBName = "hrdbdev" , Role = "Admin"},
            //    new UserModel{EmailId="mukesh@test.com",UserName="mukesh",UserPassword="abc123",CompanyName = "OgilvyHealth", DBName = "hrdbdev" , Role = "User"}
            //};

            return users;
        }

        [HttpPost]
        public IActionResult Verify(Subscription usr)
        {
            var u = PutValue();

            var ue = u.Where(u => u.EmailId.Equals(usr.EmailId));
            var up = ue.Where(p => p.UserPassword.Equals(usr.UserPassword));

            if (up.Count() == 1)
            {
                usr = up.First();
                CacheProfile cache = new CacheProfile();
                ViewBag.message = "Login Sucess";

                ViewBag.companyname = up.First().CompanyName.ToString();
                ViewBag.dbname = up.First().DBName.ToString();
                ViewBag.role = up.First().Role.ToString();

                if (up.First().Role.ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Subscription");
                else
                    return RedirectToAction("Index", "Emp");


                //return View("/Views/LoginEmp/IndexViews/Emp/Index");

            }
            else
            {
                ViewBag.NotValidUser = "Login Failed";
                //ViewBag.message = "Login Failed";
                return View("Login");
            }
        }
    }
}
