using Contact_Manger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contact_Manger.Controllers
{
    public class ContactController : Controller
    {
        private ContactContext context { get; set; }
        public ContactController(ContactContext ctx) => context = ctx;

        // LIST (GET)
        public IActionResult Index()
        {
            var contacts = context.Contacts
                .Include(c => c.Category)
                .OrderBy(c => c.Lastname)
                .ThenBy(c => c.Firstname)
                .ToList();
            return View(contacts);
        }

        // ADD (GET) - getting categories for selection
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = context.Categories.OrderBy(c => c.Name).ToList();
            ViewBag.Action = "Add";
            return View("Edit", new Contact());
        }

        // EDIT (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = context.Categories.OrderBy(c => c.Name).ToList();
            ViewBag.Action = "Edit";
            var contact = context.Contacts.Find(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // EDIT/ADD (POST)
        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = context.Categories.OrderBy(x => x.Name).ToList();
                return View(contact);
            }

            if (contact.ContactId == 0)
            {
                context.Contacts.Add(contact);
            }
            else
            {
                context.Contacts.Update(contact);
            }

            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // DELETE (GET - confirm)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = context.Contacts
                .Include(c => c.Category)
                .FirstOrDefault(c => c.ContactId == id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // DELETE (POST)
        [HttpPost]
        public IActionResult Delete(Contact contact)
        {
            var contactToDelete = context.Contacts.Find(contact.ContactId);
            if (contactToDelete != null)
            {
                context.Contacts.Remove(contactToDelete);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        // DETAILS (GET)
        [HttpGet]
        public IActionResult Details(int id)
        {
            var contact = context.Contacts
                .Include(c => c.Category)
                .FirstOrDefault(c => c.ContactId == id);

            if (contact == null) return NotFound();
            return View(contact);
        }


    }
}