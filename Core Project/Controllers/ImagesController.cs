using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core_Project.Data;
using Core_Project.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Core_Project.Controllers
{
    public class ImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImagesController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Images.Include(i => i.Product);
            return View(await applicationDbContext.ToListAsync());
        }


        [Authorize(Roles = "Seller,Admin")]
        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }



        int id;
        // GET: Images/Create
        [Authorize(Roles = "Seller")]
        public IActionResult Create(int Id)
        {
            //ViewData["FK_ProductId"] = new SelectList(_context.Products, "Id", "Name");
            //ViewData["FK_ProductId"] = Id;
            id = Id;
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ProductImage,FK_ProductId")] Image image, int id)
        {
            if (ModelState.IsValid)
            {
                image.FK_ProductId = id;
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(image.ProductImage.FileName);
                string extension = Path.GetExtension(image.ProductImage.FileName);
                image.Name = fileName = fileName + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.ProductImage.CopyToAsync(fileStream);
                }

                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Products");
            }
            ViewData["FK_ProductId"] = new SelectList(_context.Products, "Id", "Name", image.FK_ProductId);
            return View(image);
        }


        [Authorize(Roles = "Seller,Admin")]
        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            ViewData["FK_ProductId"] = new SelectList(_context.Products, "Id", "Name", image.FK_ProductId);
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProductImage,FK_ProductId")] Image image)
        {
            if (id != image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.Id))
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
            ViewData["FK_ProductId"] = new SelectList(_context.Products, "Id", "Name", image.FK_ProductId);
            return View(image);
        }


        [Authorize(Roles = "Seller,Admin")]
        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Images.FindAsync(id);

            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", image.Name);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
