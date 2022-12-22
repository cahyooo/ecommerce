using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Models;

namespace umkm_webapp.ViewComponents
{
    [ViewComponent(Name = "Login")]
    public class LoginViewComponent : ViewComponent
    {
        


        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View("Login");
        }
    }
}
