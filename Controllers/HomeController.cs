using Contact_Manger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contact_Manger.Controllers
{
    public class HomeController : Controller
    {
        private ContactContext context { get; set; }

        public HomeController(ContactContext ctx) => context = ctx;

        public IActionResult Index()
        {
            var contacts = context.Contacts
                .Include(c => c.Category)  // Add this line!
                .OrderBy(m => m.Lastname)
                .ToList();
            return View(contacts);
        }
    }
}