using InternCapstone.Data.Concrete.EfCore;
using InternCapstone.Entity;
using InternCapstone.Models;
using InternCapstone.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternCapstone.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        public readonly DatabaseContext _context;
        private readonly UserManager<AppUser> _userManager;
        public DepartmentController(DatabaseContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["FullName"] = user.FullName;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(DepartmentViewModel model)
        {
            var department = new Department
            {
                DepartmentName = model.DepartmentName
            };
            foreach (var subDivision in model.SubDivisions)
            {
                department.SubDivisions.Add(new SubDivision
                {
                    SubDivisionName = subDivision.SubDivisionName
                });
            }
            _context.Departments.Add(department);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}