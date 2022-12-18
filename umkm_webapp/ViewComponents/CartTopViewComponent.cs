using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Models;

namespace umkm_webapp.ViewComponents
{
    [ViewComponent(Name = "CartTop")]
    public class CartTopViewComponent : ViewComponent
    {
        private DatabaseContext db;
        public CartTopViewComponent(DatabaseContext _db)
        {
            this.db = _db;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Index");
        }

        

    }
}
