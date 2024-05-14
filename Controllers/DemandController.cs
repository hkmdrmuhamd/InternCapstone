using System.Security.Claims;
using System.Text;
using InternCapstone.Data.Abstract;
using InternCapstone.Data.Concrete.EfCore;
using InternCapstone.Entity;
using InternCapstone.Models;
using InternCapstone.ViewModels.Demand;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InternCapstone.Controllers
{
    public class DemandController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly HttpClient _httpClient;

        public DemandController(DatabaseContext context, UserManager<AppUser> userManager, IUserRepository userRepository, HttpClient httpClient)
        {
            _context = context;
            _userManager = userManager;
            _userRepository = userRepository;
            _httpClient = httpClient;
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
        public async Task<IActionResult> Index(DemandViewModel model)
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var departmentName = await _userRepository.GetDepartmentNameByUserNameAsync(userName);
            if (userName != null && departmentName != null)
            {
                if (ModelState.IsValid)
                {
                    var demand = new Demand
                    {
                        Text = model.Text,
                        UserName = userName,
                        DepartmentName = departmentName,
                        Status = model.Status
                    };

                    _context.Demands.Add(demand);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return View(model);
                }
                return RedirectToAction("SendText", new { text = model.Text, username = userName });
            }
            return View();
        }

        public async Task<IActionResult> SendText(string Text, string username)
        {
            var url = "http://127.0.0.1:5000/api/receive-text";
            var json = "{\"text\": \"" + Text + "\"}";
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(url, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetText", new { userName = username });
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
        }

        public async Task<IActionResult> GetText(string userName)
        {
            var client = new HttpClient();
            var url = "http://127.0.0.1:5000/get_answer";
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            if (data != null)
            {
                var answer = data.answer.ToString();
                string utf8String = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(answer));
                var chatBot = new ChatBotAnswer
                {
                    Answer = utf8String,
                    UserName = userName
                };

                _context.ChatBotAnswers.Add(chatBot);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
