using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RescueSite.Context;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using RescueSite.Const;
using RescueSite.Entites;
using Microsoft.AspNetCore.Http;

namespace RescueSite.Controllers
{
    public class AdminController : Controller
    {
        public DataContext Context { get; }
        public AdminController(DataContext _Context)
        {
            Context = _Context;
        }
      

        public IActionResult AddUsers()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            if (Auth.RoleId != 2)
                return NotFound();

            var Roles = Context.Roles.ToList();
            ViewBag.Roles = new SelectList(Roles, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddUsers(User user)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            if (user == null)
                return BadRequest("Faild");
            var md5 = new MD5CryptoServiceProvider();
            var pass = Encoding.ASCII.GetBytes(user.Password);
            var md5data = md5.ComputeHash(pass);
            var hashedPassword = ASCIIEncoding.GetEncoding("utf-8").GetString(md5data);
            user.Password = hashedPassword;
            Context.Users.Add(user);
            Context.SaveChanges();

            return Redirect("/RequestAdmin/UserRequests");
        }
    }
}
