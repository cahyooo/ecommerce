using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                ViewBag.countItems = 0;
                ViewBag.Total = 0;
            }
            else
            {
                ViewBag.countItems = cart.Count;
                ViewBag.Total = cart.Sum(it => it.Price * it.Quantity);
            }
            
            return View();
        }

        [Route("")]
        [Route("indexkosong")]
        public IActionResult IndexKosong()
        {
            return View();
        }

        [HttpGet]
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

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = exists(id, cart);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Cart");
        }

        [Route("checkout")]
        public IActionResult Checkout()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            if (user == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            else
            {
                var customer = db.Accounts.SingleOrDefault(a => a.Username.Equals(user.Value));
                //Create an Invoice

                var invoice = new Invoice()
                {
                    Name = "Invoice Online",
                    Created = DateTime.Now,
                    Status = 1,
                    AccountId = customer.Id

                };

                db.Invoices.Add(invoice);
                db.SaveChanges();

                //Create Invoice Details
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                foreach (var item in cart)
                {
                    var invoiceDetails = new InvoiceDetails
                    {
                        InvoiceId = invoice.Id,
                        ProductId = item.Id,
                        Price = item.Price,
                        Quantity = item.Quantity
                    };
                    db.InvoiceDetailses.Add(invoiceDetails);
                    db.SaveChanges();
                }

                //Remove Item
                HttpContext.Session.Remove("cart");
                return RedirectToAction("Thanks", "Cart");
            }
        }

        [Route("thanks")]
        public IActionResult Thanks()
        {
            return View("Thanks", "Cart");

        }

        [HttpPost]
        [Route("buy/{id}")]
        public IActionResult Buy(int id, int quantity)
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
                    Quantity = quantity
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
                        Quantity = quantity
                    });
                }
                else
                {
                    cart[index].Quantity += quantity;

                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(int [] quantity)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (var i = 0; i < cart.Count; i++)
            {
                cart[i].Quantity = quantity[i];
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Cart");
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
