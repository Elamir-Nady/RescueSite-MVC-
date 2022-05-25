using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RescueSite.Context;
using RescueSite.Entites;
using RescueSite.ViewModels;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using RescueSite.Const;
using Microsoft.AspNetCore.Http;

namespace RescueSite.Controllers
{
    public class UserController : Controller
    {
        public DataContext Context { get; }
        public UserController(DataContext _Context)
        {
            Context = _Context;
        }
        public IActionResult SignUp()
        {
            var Roles = Context.Roles.ToList();
            ViewBag.Roles = new SelectList(Roles, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            if (user == null)
                return BadRequest("Faild");
            var md5 = new MD5CryptoServiceProvider();
            var pass = Encoding.ASCII.GetBytes(user.Password);
            var md5data = md5.ComputeHash(pass);
            var hashedPassword = ASCIIEncoding.GetEncoding("utf-8").GetString(md5data);
            user.Password = hashedPassword;
            user.RoleId = 1;
            Context.Users.Add(user);
            Context.SaveChanges();

            Auth.UserId = user.Id;
            Auth.RoleId = user.RoleId;
            Auth.Name = user.FullName;
            HttpContext.Session.SetString("_User", user.FullName);



            if (user.RoleId == 1)
                return Redirect("/RequestUser/UserRequests");
            else
                return Redirect("/RequestAdmin/UserRequests");
        }

      

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM loginVm)
        {
            if (loginVm == null)
                return BadRequest("Faild");
            var md5 = new MD5CryptoServiceProvider();
            var pass = Encoding.ASCII.GetBytes(loginVm.Password);
            var md5data = md5.ComputeHash(pass);
            var hashedPassword = ASCIIEncoding.GetEncoding("utf-8").GetString(md5data);
            loginVm.Password = hashedPassword;

            var user = Context.Users.Where(u => u.Email == loginVm.Email && u.Password == loginVm.Password).FirstOrDefault();
            if (user == null)
            {
                ViewData["login"] = "User Or Password Is Invalid";
                return View();
            }
            else
            {
                ViewData["login"] = null;

                Auth.UserId = user.Id;
                Auth.RoleId = user.RoleId;
                Auth.Name = user.FullName;
                HttpContext.Session.SetString("_User", user.FullName);



                if (user.RoleId == 1)
                    return Redirect("/RequestUser/UserRequests");
                else
                    return Redirect("/RequestAdmin/UserRequests");
            }
           return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Auth.UserId = 0;
            Auth.RoleId = 0;
            Auth.Name = null;
            return Redirect("/User/Login");
        }
    }
}
