using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Helpers;
using umkm_webapp.Models;

namespace umkm_webapp.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public CartController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.countItems = cart.Count;
            ViewBag.Total = cart.Sum(it => it.Price * it.Quantity);
            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            var product = db.Products.Find(id);
            var photo = product.Photos.SingleOrDefault(ph => ph.Status && ph.Featured);
            var photoName = photo == null ? "no-image.png" : photo.Name;
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<Item>();
                cart.Add(new Item
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Photo = photoName,
                    Quantity = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
               var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = exists(id, cart);
                if (index == -1)
                {
                    cart.Add(new Item
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Photo = photoName,
                        Quantity = 1
                    });
                }
                else
                {
                    cart[index].Quantity++;
                    
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            }
            return RedirectToAction("Index","Cart");
        }

        private int exists(int id, List<Item> cart)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
