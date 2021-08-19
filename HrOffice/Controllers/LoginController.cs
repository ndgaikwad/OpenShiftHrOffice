using HrOffice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HrOffice.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public List<UserModel> PutValue()
        {
            var users = new List<UserModel>
            {
                new UserModel{id=1,userName="nilesh",password="abc123"},
                new UserModel{id=1,userName="ravi",password="abc123"},
                new UserModel{id=1,userName="suraj",password="abc123"},
                new UserModel{id=1,userName="test",password="abc123"}
            };

            return users;
        }

        [HttpPost]
        public IActionResult Verify(UserModel usr)
        {
            var u = PutValue();

            var ue = u.Where(u => u.userName.Equals(usr.userName));
            var up = ue.Where(p => p.password.Equals(usr.password));

            if (up.Count() == 1)
            {

                ViewBag.message = "Login Sucess";
                return View("/Views/LoginEmp/IndexViews/Emp/Index");

            }
            else
            {
                ViewBag.message = "Login Failed";
                return View("Login");
            }
        }
    }
}
