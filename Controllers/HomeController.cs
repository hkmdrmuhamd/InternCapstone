using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using InternCapstone.Data.Abstract;
using InternCapstone.Entity;
using InternCapstone.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

namespace InternCapstone.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly DatabaseContext _context;

    public HomeController(DatabaseContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Index()
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var departmentName = await _userRepository.GetDepartmentNameByUserNameAsync(userName);
        var names = await _context.ChatBotAnswers.Select(a => a.Answer).ToListAsync();
        if (names.Contains(departmentName))
        {
            ViewBag.ShowNewTaskMessage = true;
        }
        else
        {
            ViewBag.ShowNewTaskMessage = false;
        }
        return View();
    }
}
