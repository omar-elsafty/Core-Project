using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_Project.Data;
using Core_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_Project.Controllers
{
    public class CatigoriesController : Controller
    {
        private ApplicationDbContext context;

        public CatigoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {  
            return View(context.Catigories.ToList());
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View("AddCategory");
        }
        
        [HttpPost]
        public IActionResult Add(Catigory catigory)
        {
            if (ModelState.IsValid)
            {
                context.Add(catigory);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("AddCategory");
        }
    }
}