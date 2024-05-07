using System.Security.Claims;
using InternCapstone.Data.Abstract;
using InternCapstone.Data.Concrete.EfCore;
using InternCapstone.Entity;
using InternCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternCapstone.Controllers
{
    public class HelpRequestController : Controller
    {
        private readonly DatabaseContext _context;
        public readonly UserManager<AppUser> _userManager;
        private readonly IDemandRepository _demandRepository;

        public HelpRequestController(DatabaseContext context, UserManager<AppUser> userManager, IDemandRepository demandRepository)
        {
            _context = context;
            _userManager = userManager;
            _demandRepository = demandRepository;
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
    }
}
