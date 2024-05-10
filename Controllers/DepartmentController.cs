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
        public DepartmentController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateDepartment(DepartmentViewModel model)
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