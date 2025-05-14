using System.Diagnostics;
using LoginFromASPCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Threading.Tasks;

namespace LoginFromASPCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CodeFirstDbContext context;

        public HomeController(ILogger<HomeController> logger, CodeFirstDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> DispUserInfo()
        {
            
            var data = await context.UserTbls.ToListAsync();
            
            return View(data);
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null) 
            {
                return RedirectToAction("Dashboard", "Home");
            } else
            {

                return View();
            }
        }
                [HttpPost]
        public async Task<IActionResult> Login(UserTbl user)
        {
            var myUser = await context.UserTbls.Where(x=>x.Email==user.Email&&x.Password==user.Password).FirstOrDefaultAsync();
            if (myUser != null)
            {
                HttpContext.Session.SetString("UserSession", myUser.Email);
                return RedirectToAction("Dashboard");

            }else {
                ViewBag.Message = "Login Failed ...";
            }
                return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserTbl user)
        {
            if (ModelState.IsValid)
            {
            await context.UserTbls.AddAsync(user);
            await context.SaveChangesAsync();
            TempData["Success"] = "Register Form Data Insert Success in UserTble";
            }
            return View();
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserSession") != null )
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
                return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null )
            {
            HttpContext.Session.Remove("UserSession");
            return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
