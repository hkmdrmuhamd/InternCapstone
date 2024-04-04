using InternCapstone.Data.Abstract;
using InternCapstone.Entity;
using InternCapstone.Models;
using InternCapstone.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InternCapstone.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IDepartmentRepository _departmentRepository;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IDepartmentRepository departmentRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> SignUpAsync()
        {
            var departments = await _departmentRepository.Departments.Select(i => new SelectListItem { Value = i.DepartmentId.ToString(), Text = i.DepartmentName }).ToListAsync();
            var viewModel = new SignUpViewModel
            {
                Departments = departments
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (model.DepartmentId != null)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    DepartmentId = model.DepartmentId
                };
                if (model.Password != null)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}