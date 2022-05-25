using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RescueSite.Const;
using RescueSite.Context;
using RescueSite.Entites;
using System.Linq;

namespace RescueSite.Controllers
{
    public class RequestAdminController : Controller
    {
        public DataContext Context { get; }

        public RequestAdminController(DataContext _Context)
        {
            Context = _Context;
        }
        public IActionResult UserRequests()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            var requests = Context.Requests.Where(r => r.StatusId == 1).Include("User").ToList();


            return View(requests);
        }

        public IActionResult FinishedRequests()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            var requests = Context.Requests.Where(r => r.StatusId == 3).Include("User").ToList();


            return View(requests);
        }

        public IActionResult RejectedRequests()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            var requests = Context.Requests.Where(r => r.StatusId == 4).Include("User").ToList();


            return View(requests);
        }

        public IActionResult Reject(int id)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            var request = Context.Requests.Where(r => r.StatusId == 1&&r.Id==id).FirstOrDefault();
            if(request == null)
                return NotFound();
            request.StatusId = 4;


            Context.Requests.Update(request);
            Context.SaveChanges();


            return RedirectToAction(nameof(UserRequests));
        }

        public IActionResult ApproveRequest(int id)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            var request = Context.Requests.Where(r => r.StatusId == 1 && r.Id == id).Include("User").FirstOrDefault();
            if (request == null)
                return NotFound();


            var Winchs = Context.Winchs.Where(w => w.Id > 1 && w.Booke == false).ToList();
            ViewBag.Winchs = new SelectList(Winchs, "Id", "WinchNumber");


            return View(request);
        }
        [HttpPost]
        public IActionResult ApproveRequest(Request request)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            if (request == null)
                return NotFound();
            request.StatusId = 2;
            var user = Context.Users.Where(u => u.Id == request.User.Id).FirstOrDefault();
            var winch = Context.Winchs.Where(u => u.Id == request.WinchId).FirstOrDefault();
            request.User = user;
            winch.Booke = true;
            Context.Winchs.Update(winch);
            request.Winch=winch;
            Context.Requests.Update(request);
            Context.SaveChanges();


            return RedirectToAction(nameof(UserRequests));
        }

        public IActionResult AllUsers()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            var Users = Context.Users.Where(u => u.RoleId == 1).Include("Role"); ;
            if (Users == null)
                return NotFound();

            return View(Users);
        }
        public IActionResult Admins()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            var Admins = Context.Users.Where(u => u.RoleId == 2).Include("Role");
            if (Admins == null)
                return NotFound();

            return View(Admins);
        }

    }
}
