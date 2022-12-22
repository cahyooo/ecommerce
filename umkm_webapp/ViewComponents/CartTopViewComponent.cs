using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Helpers;
using umkm_webapp.Models;

namespace umkm_webapp.ViewComponents
{
    [ViewComponent(Name = "CartTop")]
    public class CartTopViewComponent : ViewComponent
    {
        

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                ViewBag.countItems = 0;
                ViewBag.Total = 0;
            }

            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                ViewBag.countItems = cart.Count;
                ViewBag.Total = cart.Sum(it => it.Price * it.Quantity);
            }
            
            return View("Index");
        }

        

    }
}
