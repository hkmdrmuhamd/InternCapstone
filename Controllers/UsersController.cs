using InternCapstone.ViewModels.User;
using InternCapstone.Data.Abstract;
using InternCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace InternCapstone.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }
            var user = _userRepository
                        .Users
                        .Include(i => i.SubDivisions)
                        .ThenInclude(j => j.Department)
                        .FirstOrDefault(x => x.UserName == username);
            if (user is null)
            {
                return NotFound();
            }
            var userList = await _userManager.GetUserAsync(User);
            if (userList != null)
            {
                ViewData["FullName"] = userList.FullName;
            }
            return View(user);
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(new ChangePasswordViewModel
                {
                    Id = user.Id,
                });
            }
            var userList = await _userManager.GetUserAsync(User);
            if (userList != null)
            {
                ViewData["FullName"] = userList.FullName;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordViewModel model)
        {
            if (id != model.Id)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    if (model.OldPassword != null)
                    {
                        var passwordCheckResult = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                        if (!passwordCheckResult)
                        {
                            ModelState.AddModelError("", "Eski parola yanlış.");
                            return View(model);
                        }
                        else
                        {
                            var result = await _userManager.UpdateAsync(user);

                            if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                            {
                                await _userManager.RemovePasswordAsync(user);
                                await _userManager.AddPasswordAsync(user, model.Password);
                                foreach (var cookie in Request.Cookies.Keys)
                                {
                                    Response.Cookies.Delete(cookie);
                                }
                                await HttpContext.SignOutAsync();
                                TempData["message"] = "Şifreniz değiştirilmiştir. Lütfen tekrar giriş yapınız.";
                                return RedirectToAction("SignIn", "Account");
                            }
                            else
                            {
                                foreach (IdentityError error in result.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                            }
                        }
                    }
                }
            }
            return View(model);
        }
    }
}