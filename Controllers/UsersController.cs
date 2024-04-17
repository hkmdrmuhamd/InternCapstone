using InternCapstone.Data.Abstract;
using InternCapstone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternCapstone.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public IActionResult Index(string username)
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
            return View(user);
        }
    }
}