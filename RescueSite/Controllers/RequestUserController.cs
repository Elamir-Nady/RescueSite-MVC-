using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RescueSite.Const;
using RescueSite.Context;
using RescueSite.Entites;
using RescueSite.ViewModels;
using System.Linq;

namespace RescueSite.Controllers
{
    public class RequestUserController : Controller
    {
        public DataContext Context { get; }

        public RequestUserController(DataContext _Context)
        {
            Context = _Context;
        }
        public IActionResult Add()
        {
            if(HttpContext.Session.GetString("_User") ==null)
                return NotFound();

            return View();
        }
        public IActionResult View(int id)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();
            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 1 || id <=0)
                return NotFound();
            var request = Context.Requests.Where(x => x.Id == id).Include("RequestStatus").Include("Winch").FirstOrDefault();
            if (request == null)
                return NotFound();
            request.Winch.Booke = false;
            request.StatusId = 3;
            Context.Requests.Update(request);
            Context.SaveChanges();
            return RedirectToAction(nameof(UserRequests));
        }
        [HttpPost]
        public IActionResult View(Request request)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();
            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 1 || request==null)
                return NotFound();
            var winch = Context.Winchs.Where(x => x.Id == request.WinchId).FirstOrDefault();
            var user = Context.Users.Where(x => x.Id == request.UserId).FirstOrDefault();
            winch.Booke = false;
            request.Winch = winch;
            request.User = user;
            request.StatusId = 3;
            Context.Requests.Update(request);
            Context.SaveChanges();
            return RedirectToAction(nameof(UserRequests));
        }
        [HttpPost]
        public IActionResult Add(CreateRequestVM request)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if(role!=1|| request==null)
                return NotFound();
           Request request1 = new Request();
            request1.UserId = userId;
            request1.carNum = request.carNum;
            request1.CarDetails = request.CarDetails;
            request1.Location = request.Location;
            request1.WinchId = 1;

            Context.Requests.Add(request1);
            Context.SaveChanges();

            return RedirectToAction(nameof(UserRequests));
        }

        public IActionResult UserRequests()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 1 || userId<=0)
                return NotFound();
          var requests=  Context.Requests.Where(r=>r.UserId==userId).Include("RequestStatus").Include("Winch").ToList();


            return View(requests);
        }
    }
}
