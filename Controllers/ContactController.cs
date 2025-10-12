using Microsoft.AspNetCore.Mvc;

namespace Contact_Manger.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
