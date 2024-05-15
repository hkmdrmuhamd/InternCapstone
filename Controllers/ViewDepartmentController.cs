using InternCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternCapstone.ViewModels.ViewRecords;
using Microsoft.AspNetCore.Authorization;
using InternCapstone.Entity;
using InternCapstone.Data.Concrete.EfCore;

namespace InternCapstone.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ViewDepartmentController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly DatabaseContext _context;

        public ViewDepartmentController(UserManager<AppUser> userManager, DatabaseContext context)
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

            ViewBag.SubDivisions = await _context.Departments.Include(d => d.SubDivisions).ToListAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            int departmentId = Convert.ToInt32(id);
            // var department = await _context.Departments
            //                                .Include(d => d.SubDivisions)
            //                                .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
            var departments = await _context.Departments.Include(d => d.SubDivisions).ToListAsync();
            foreach (var department in departments)
            {
                if (department.DepartmentId == departmentId)
                {
                    foreach (var subDivision in department.SubDivisions.ToList())
                    {
                        _context.SubDivisions.Remove(subDivision);
                    }
                    _context.Departments.Remove(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(id);
        }
    }
}