using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Models;
using X.PagedList;

namespace umkm_webapp.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ProductController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            
            var product = db.Products.Find(id);
            var featuredPhoto = product.Photos.SingleOrDefault(p => p.Status && p.Featured);
            ViewBag.Product = product;
            ViewBag.FeaturedPhoto = featuredPhoto == null ? "no-image.png" : featuredPhoto.Name;
            ViewBag.ProductImages = product.Photos.Where(p => p.Status).ToList();

            var featuredproduct = db.Products.OrderByDescending(p => p.Id).Where(p => p.Status && p.Featured).ToList();
            ViewBag.FeaturedProducts = featuredproduct;
            var relatedProduct = db.Products.Where(p => p.CategoryId == product.CategoryId && p.Id != id && p.Status).Take(4).ToList();
            ViewBag.CountRelatedProducts = relatedProduct.Count(p => p.Status);
            ViewBag.RelatedProducts = relatedProduct;

            

            return View("Details");
        }

        [Route("category/{id}")]
        public IActionResult Category(int id, int? page)
        {
            var pageNumber = page ?? 1;
            var category = db.Categories.Find(id);
            ViewBag.Category = category;
            ViewBag.CountProducts = category.Products.Count(p => p.Status);
            ViewBag.Products = category.Products.Where(c => c.Status).ToList().ToPagedList(pageNumber, 6);
            return View("Category");
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string keyword, int? page)
        {
             var pageNumber = page ?? 1;
            var products = db.Products.Where(p => p.Name.Contains(keyword) && p.Status).ToList();
            ViewBag.Keyword = keyword;
            ViewBag.CountProducts = products.Count;
            ViewBag.Products = products.ToPagedList(pageNumber, 6);
            
            return View("Search");
        }
    }
}
