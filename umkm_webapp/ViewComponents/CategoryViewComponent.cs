using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Models;

namespace umkm_webapp.ViewComponents
{
    [ViewComponent(Name = "Category")]
    public class CategoryViewComponent : ViewComponent
    {
        private DatabaseContext db;
        public CategoryViewComponent(DatabaseContext _db)
        {
            this.db = _db;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories = db.Categories.Where(c => c.Status && c.Parent == null).ToList();
            return View("Index", categories);
        }
    }
}
