using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using InternCapstone.Data.Abstract;
using InternCapstone.Entity;
using InternCapstone.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using InternCapstone.ViewModels.Demand;
using Microsoft.AspNetCore.Identity;
using InternCapstone.Models;

namespace InternCapstone.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IDemandRepository _demandRepository;
    private readonly DatabaseContext _context;
    private readonly UserManager<AppUser> _userManager;

    public HomeController(DatabaseContext context, IUserRepository userRepository, IDemandRepository demandRepository, UserManager<AppUser> userManager)
    {
        _context = context;
        _userRepository = userRepository;
        _demandRepository = demandRepository;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var departmentName = await _userRepository.GetDepartmentNameByUserNameAsync(userName);
        var names = await _context.ChatBotAnswers.Select(a => a.Answer).ToListAsync();
        var clientName = await _context.Demands.Select(a => a.UserName).ToListAsync();
        foreach (var isIn in clientName)
        {
            var status = await _demandRepository.GetStatusByUserNameAsync(isIn);
            if (names.Contains(departmentName) && status != "ok")
            {
                ViewBag.ShowNewTaskMessage = true;
            }
            else
            {
                ViewBag.ShowNewTaskMessage = false;
            }
        }
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            ViewData["FullName"] = user.FullName;
        }
        return View();
    }
}
