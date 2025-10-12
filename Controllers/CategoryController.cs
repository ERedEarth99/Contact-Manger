using Microsoft.AspNetCore.Mvc;

namespace Contact_Manger.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
