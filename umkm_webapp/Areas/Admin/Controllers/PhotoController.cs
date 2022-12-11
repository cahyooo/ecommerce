using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Models;

namespace umkm_webapp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/product")]
    public class PhotoController : Controller
    {

        private DatabaseContext db = new DatabaseContext();

        private IHostingEnvironment ihostingEnvironment;

        public PhotoController(DatabaseContext _db, IHostingEnvironment _ihostingEnvironment)
        {
            db = _db;
            ihostingEnvironment = _ihostingEnvironment;
        }

        [HttpGet]
        [Route("index/{Id}")]
        public IActionResult Index(int Id)
        {
            ViewBag.Product = db.Products.Find(Id);
            ViewBag.Photos = db.Photos.Where(p => p.ProductId == Id);
            return View();
        }

        [HttpGet]
        [Route("add/{id}")]
        public IActionResult Add(int id)
        {
            ViewBag.Product = db.Products.Find(id);
            var photo = new Photo
            {
                ProductId = id
            };
            return View("Add", photo);
        }

        [HttpPost]
        [Route("add/{productid}")]
        public IActionResult Add(int productid, Photo photo, IFormFile fileUpload)
        {
            var fileName = DateTime.Now.ToString("MMddyyyyhhmmss") + fileUpload.FileName;
            var path = Path.Combine(this.ihostingEnvironment.WebRootPath, "product", fileName);
            var stream = new FileStream(path, FileMode.Create);
            fileUpload.CopyToAsync(stream);
            photo.Name = fileName;
            db.Photos.Add(photo);
            db.SaveChanges();
            return RedirectToAction("index", "photo", new { area = "admin", id = productid });
        }

        [HttpGet]
        [Route("delete/{id}/productId")]
        public IActionResult Delete(int id, int productId)
        {
            var photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }

        [HttpGet]
        [Route("edit/{id}/{productId}")]
        public IActionResult Edit(int id, int productId)
        {
            ViewBag.Product = db.Products.Find(productId);
            var photo = db.Photos.Find(id);
            return View("Edit", photo);
        }

        [HttpPost]
        [Route("edit/{photoId}/{productId}")]
        public IActionResult Edit(int photoId, int productId, Photo photo, IFormFile fileUpload)
        {
            var currentPhoto = db.Photos.Find(photo.Id);
            if (fileUpload != null && !string.IsNullOrEmpty(fileUpload.FileName))
            {
                var fileName = DateTime.Now.ToString("MMddyyyyhhmmss") + fileUpload.FileName;
                var path = Path.Combine(this.ihostingEnvironment.WebRootPath, "product", fileName);
                var stream = new FileStream(path, FileMode.Create);
                fileUpload.CopyToAsync(stream);
                currentPhoto.Name = fileName;
            }
            currentPhoto.Status = photo.Status;
            currentPhoto.Featured = photo.Featured;
            db.SaveChanges();
            return RedirectToAction("index", "photo", new { area = "admin", id = productId });

        }

        [HttpGet]
        [Route("SetFeatured/{id}/{productId}")]
        public IActionResult SetFeatured(int id, int productId)
        {
            var product = db.Products.Find(productId);
            product.Photos.ToList().ForEach(p =>
            {
                p.Featured = false;
                db.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            });
            var photo = db.Photos.Find(id);
            photo.Featured = true;
            db.SaveChanges();
            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }
    }
}