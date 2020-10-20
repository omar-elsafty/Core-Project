using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace Core_Project.Controllers
{
    public class HomeController : Controller
    {
        RoleManager<IdentityRole> roleManager;

        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;

        }



        public async Task<IActionResult> Index()
        {
            IdentityRole admin = new IdentityRole("Admin");
            IdentityRole user = new IdentityRole("User");
            IdentityRole seller = new IdentityRole("Seller");

            await roleManager.CreateAsync(admin);
            await roleManager.CreateAsync(user);
            await roleManager.CreateAsync(seller);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
