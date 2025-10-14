using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Contact_Manger.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ContactContext context;
        public CategoryController(ContactContext ctx) => context = ctx;

        // LIST all categories
        public IActionResult Index()
        {
            var categories = context.Categories
                .Include(c => c.Contacts)
                .OrderBy(c => c.Name)
                .ToList();
            return View(categories);
        }

        // VIEW details (for filtering)
        public IActionResult Details(int id)
        {
            var category = context.Categories
                .Include(c => c.Contacts)
                .FirstOrDefault(c => c.CategoryId == id);

            if (category == null) return NotFound();
            return View(category);
        }

        // No Add, Edit, or Delete actions needed because categories are static (seeded data)
    }
}