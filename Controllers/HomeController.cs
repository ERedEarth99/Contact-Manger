using Contact_Manger.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contact_Manger.Controllers //Changed from .Models to .Controllers
{
    public class HomeController : Controller // Changed from ErrorViewModel to HomeController : Controller
    {

        private ContactContext context { get; set; }

        public HomeController(ContactContext ctx) => context = ctx;

        public IActionResult Index()
        {
            var contacts = context.Contacts.OrderBy(m => m.Lastname).ToList();
            return View(contacts);
        }
    }
}