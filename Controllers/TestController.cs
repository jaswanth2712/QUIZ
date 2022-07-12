using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QUIZ.Models;
namespace QUIZ.Controllers
{
    public class TestController : Controller
    {
        public QuestionerContext context;
        public TestController(QuestionerContext Context)
        {
            context = Context;

        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(LoginModel l)
        {

            var a = context.Users.FirstOrDefault(x => x.UserName == l.UserName);
            if (a!=null)
            {
                if (a.PassWord == l.Password)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,a.UserName),
                    new Claim(ClaimTypes.Role,a.Role)
                };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);



                    if (a.Role.Equals("Admin"))
                    {


                        return RedirectToAction("AdminPage");
                    }
                    //Admin Page
                    else
                    {
                        return RedirectToAction("UserPage");
                    }
                    //User Page
                }
                else
                {
                    ModelState.AddModelError("Password", "Password is incorrect");
                    return View();                 //password Mismatch     
                }
            }
            else
            {
                ModelState.AddModelError("UserName", "User Name Does not Exists");
                return View();
            }
           

        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel r)
        {
            var temp = from s in context.Users select s.UserName;
            foreach (var i in temp)
            {
                if (i.ToString().Equals(r.UserName))
                {
                    ModelState.AddModelError("UserName", "UserName already Exists");
                    return View();
                }
            }
            if (ModelState.IsValid)
            {

                User reg = new User()
                {
                    UserName = r.UserName,
                    PassWord = r.Password,
                    Role = "User"

                };
                context.Users.Add(reg);
                context.SaveChanges();
                TempData["InsMsg"] = "<script>alert('Account Created Successfully')</script>";
                return View("Index");
            }

            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPage()
        {
            
            ///TempData["UserName"]= User.Identity.Name;
            return View();
        }
        [Authorize(Roles = "Admin,User")]

        public IActionResult UserPage()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

    }
}