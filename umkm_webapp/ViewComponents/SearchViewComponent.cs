using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Models;

namespace umkm_webapp.ViewComponents
{
    [ViewComponent(Name = "Search")]
    public class SearchViewComponent : ViewComponent
    {
        private DatabaseContext db;
        public SearchViewComponent(DatabaseContext _db)
        {
            this.db = _db;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            string keyword = HttpContext.Request.Query["keyword"].ToString();
            List<Category> categories = db.Categories.Where(c => c.Status && c.Parent == null).ToList();
            return View("Index", new InvokeResult() { keyword = keyword, categories = categories });
        }

        public class InvokeResult
        {
            public string keyword { get; set; }
            public List<Category> categories { get; set; }

        }

    }
}
