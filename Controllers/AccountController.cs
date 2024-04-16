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
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISubDivisionRepository _subDivisionRepository;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IDepartmentRepository departmentRepository, ISubDivisionRepository subDivisionRepository, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _departmentRepository = departmentRepository;
            _subDivisionRepository = subDivisionRepository;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> SignUp()
        {
            var departments = await _departmentRepository.Departments.Select(i => new SelectListItem { Value = i.DepartmentId.ToString(), Text = i.DepartmentName }).ToListAsync();
            var viewModel = new SignUpViewModel
            {
                Departments = departments
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetBranches(int DepartmentId)
        {
            // fieldId'ye bağlı olarak ilgili branşları getir ve JSON olarak döndür
            var branches = _subDivisionRepository.GetSubDivisionNamesByDepartmentIdAsync(DepartmentId).Result; // Asenkron metot olduğu için Result kullanıldı
            return Json(branches);
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
                    DepartmentId = model.DepartmentId,
                    SubDivision = model.SubDivision
                };
                if (model.Password != null)
                {
                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (model.SelectedRole != null)
                        {
                            await _userManager.AddToRoleAsync(user, model.SelectedRole);
                        }

                        if (user.Email != null)
                        {
                            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var url = Url.Action("ConfirmEmail", "Account", new { user.Id, token });

                            await _emailSender.SendEmailAsync(user.Email, "Hesabınızı Onaylayın", $"Lütfen e-mail hesabınızı onaylamak için linke <a href='http://localhost:5073{url}'>tıklayınız</a>");
                        }
                        TempData["message"] = "Email hesabınızdaki onay mailine tıklayınız";
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

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserName != null && model.Password != null)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (user != null)
                    {
                        await _signInManager.SignOutAsync();

                        if (!await _userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError("", "Lütfen e-posta adresinizi doğrulayın.");
                            return View(model);
                        }

                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                        if (result.Succeeded)
                        {
                            await _userManager.ResetAccessFailedCountAsync(user);
                            await _userManager.SetLockoutEndDateAsync(user, null);
                            return RedirectToAction("Index", "Home");
                        }
                        else if (result.IsLockedOut)
                        {
                            var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                            var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                            ModelState.AddModelError("", $"Hesabınız {timeLeft.Minutes} dakika boyunca kilitlenmiştir.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Şifre hatalı");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı hatalı");
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string Id, string token)
        {
            if (Id == null || token == null)
            {
                TempData["message"] = "Geçersiz token bilgisi";
                return View();
            }

            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData["message"] = "Hesabınız onaylandı";
                    return RedirectToAction("SignIn", "Account");
                }
            }
            TempData["message"] = "Kullanıcı Bulunamadı";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}