using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_Project.Data;
using Core_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Core_Project.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IActionResult Index(string value)
        {
            List<Product> products = context.Products.Include(j=>j.Images).ToList();
            if (value != null)
            {
                products = Sort(value, products);
            }
            List<Catigory> catigories = context.Catigories.ToList();
            List<Tag> tags = context.Tags.ToList();
            

            IndexViewModel indexView = new IndexViewModel { Catigories = catigories, Products = products, Tags = tags };
            return View(indexView);
        }


        public List<Product> Sort(string value, List<Product> products)
        {
            List<Product> sortedList = new List<Product>();
            switch (value)
            {
                //case "1":
                //    sortedList = products.OrderBy(m => m.Discount).ToList();
                //    break;

                case "2":
                    sortedList = products.OrderBy(m => m.Price).ToList();
                    break;
                case "3":
                    sortedList = products.OrderByDescending(m => m.Price).ToList();
                    break;
                case "4":
                    products.Sort((x, y) => x.Name.CompareTo(y.Name));
                    sortedList = products;
                    break;

            }
            return sortedList;
        }


        public IActionResult Search(string value)
        {

            List<Product> products = context.Products.Where(m => m.Name == value).Include(p => p.Images).ToList();

            List<Catigory> catigories = context.Catigories.ToList();
            List<Tag> tags = context.Tags.ToList();

            IndexViewModel indexView = new IndexViewModel { Catigories = catigories, Products = products, Tags = tags };
            return View("Index", indexView);
        }

        public IActionResult SearchC(string value)
        {

            List<Product> products = context.Products.Where(m => m.Catigory.Name == value).Include(p => p.Images).ToList();

            List<Catigory> catigories = context.Catigories.ToList();
            List<Tag> tags = context.Tags.ToList();

            IndexViewModel indexView = new IndexViewModel { Catigories = catigories, Products = products, Tags = tags };
            return View("Index", indexView);
        }


        [Authorize(Roles = "Seller,Admin")]
        public IActionResult AddProduct()
        {
            CategoryViewModel viewMod = new CategoryViewModel
            {
                Catigories = context.Catigories.ToList(),
                

                PaymentTypes = context.PaymentTypes.ToList(),

            };
            return View("AddProduct", viewMod);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product, List<PaymentType> paymentTypes, List<string> tags)
        {
            if (ModelState.IsValid)
            {               
                context.Products.Add(product);

                foreach (var item in paymentTypes)
                {
                    if (item.IsSelected)
                    {
                        context.ProductPaymentTypes.Add(new ProductPaymentType { ProductId = product.Id, PaymentTypeId = item.Id });
                    }
                }


                if (tags.Count > 0)
                {
                    foreach (var item in tags)
                    {
                        Tag tag = context.Tags.ToList().Where(e => e.Name == item).FirstOrDefault();

                        if (tag == null)
                        {
                            tag = new Tag { Name = item };
                            context.Tags.Add(tag);
                        }

                        ProductTag productTag = new ProductTag { ProductId = product.Id, TagId = tag.Id };

                        context.ProductTags.Add(productTag);

                    }
                }
                context.SaveChanges();

                return RedirectToAction("Create", "Images", new { Id = product.Id }) ;
                
            }

            CategoryViewModel viewMod = new CategoryViewModel
            {
                Catigories = context.Catigories.ToList(),


                PaymentTypes = context.PaymentTypes.ToList(),

            };
            return View("AddProduct", viewMod);
        }


        [Authorize(Roles = "Seller,Admin,User")]
        public IActionResult ProductDetails(int id)
        {
            CategoryViewModel cvm = new CategoryViewModel
            {

                Product = context.Products.Where(e => e.Id == id).Include(p => p.Images).Include(p => p.Catigory).FirstOrDefault()
                
            };


            context.SaveChanges();
            return View(cvm);

        }

        [Authorize(Roles = "Seller,Admin")]
        public IActionResult EditProduct(int id)
        {

            CategoryViewModel viewMod = new CategoryViewModel
            {
                Catigories = context.Catigories.ToList(),


                PaymentTypes = context.PaymentTypes.ToList(),
                Product = context.Products.Find(id)

            };
            return View("EditProduct", viewMod);

        }

        [HttpPost]
        public  IActionResult EditProduct(int id, Product product, List<PaymentType> paymentTypes)
        {
            if (ModelState.IsValid)
            {
               
                foreach (var item in paymentTypes)
                {
                    if (item.IsSelected)
                    {
                        context.ProductPaymentTypes.Add(new ProductPaymentType { ProductId = product.Id, PaymentTypeId = item.Id });
                    }
                }

                context.Products.Update(product);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }




        [Authorize(Roles = "Seller,Admin")]
        //[HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {

            //try
            //{
                Product product = context.Products.FirstOrDefault(m=>m.Id==id);
            context.Images.Remove(context.Images.FirstOrDefault(i => i.FK_ProductId == id));
            context.Products.Remove(product);
            
                context.SaveChanges();
                return RedirectToAction(nameof(Index));

            //catch (Exception)
            //{
            //    return View("Error");
            //}

        }
    }
}



