using System.Security.Claims;
using InternCapstone.Data.Abstract;
using InternCapstone.Data.Concrete.EfCore;
using InternCapstone.Entity;
using InternCapstone.Models;
using InternCapstone.ViewModels.Demand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternCapstone.Controllers
{
    [Authorize]
    public class HelpRequestController : Controller
    {
        private readonly DatabaseContext _context;
        public readonly UserManager<AppUser> _userManager;
        private readonly IDemandRepository _demandRepository;
        private readonly IUserRepository _userRepository;

        public HelpRequestController(DatabaseContext context, UserManager<AppUser> userManager, IDemandRepository demandRepository, IUserRepository userRepository)
        {
            _context = context;
            _userManager = userManager;
            _demandRepository = demandRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var clientUserName = await _context.Demands.Select(n => n.UserName).ToListAsync();
            var answerUserName = await _context.ChatBotAnswers.Select(n => n.UserName).ToListAsync();
            foreach (var username in answerUserName)
            {
                if (clientUserName.Contains(username))
                {
                    ViewBag.UserName = username;
                    ViewBag.DepartmentName = await _demandRepository.GetDepartmentNameByUserNameAsync(username);
                    ViewBag.Text = await _demandRepository.GetTextByUserNameAsync(username);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(DemandViewModel model)
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var departmentName = await _userRepository.GetDepartmentNameByUserNameAsync(userName);
            var names = await _context.ChatBotAnswers.Select(a => a.Answer).ToListAsync();
            var clientName = await _context.Demands.Select(a => a.UserName).ToListAsync();
            foreach (var isIn in clientName)
            {
                var demandId = await _demandRepository.GetIdByUserName(isIn);
                var demands = _context.Demands.Find(demandId);
                if (demands != null && demands.Status != "ok")
                {
                    demands.Status = "ok";
                    await _context.SaveChangesAsync();
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
                else
                {
                    ViewBag.ShowNewTaskMessage = false;
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
