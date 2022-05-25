using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RescueSite.Const;
using RescueSite.Context;
using RescueSite.Entites;
using System.Linq;

namespace RescueSite.Views.RequestAdmin
{
    public class WinchController : Controller
    {
        public DataContext Context { get; }
        public WinchController(DataContext _Context)
        {
            Context = _Context;
        }
        public IActionResult AddWinch()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();

            return View();
        }


        [HttpPost]
        public IActionResult AddWinch(Winch winch)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            if(winch == null)
                return NotFound();
            Context.Winchs.Add(winch);
            Context.SaveChanges();
            return RedirectToAction(nameof(AllWinches));
        }

        public IActionResult AllWinches()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();

            var winch = Context.Winchs.Where(w=>w.Id>1).ToList();
            if(winch==null)
                return NotFound();

            return View(winch);
        }

        public IActionResult BookedWinches()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();

            var winch = Context.Winchs.Where(w => w.Id > 1&&w.Booke==true).ToList();
            if (winch == null)
                return NotFound();

            return View(winch);
        }

        public IActionResult UnBookedWinches()
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();

            var winch = Context.Winchs.Where(w => w.Id > 1 && w.Booke == false).ToList();
            if (winch == null)
                return NotFound();

            return View(winch);
        }

        public IActionResult EditWinche(int id)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            if (id < 2)
                return NotFound();

            var winch = Context.Winchs.Where(w => w.Id == id).FirstOrDefault();
            if (winch == null)
                return NotFound();
       

            return View(winch);
        }


        [HttpPost]
        public IActionResult EditWinche(Winch winch)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
    
            if (winch == null)
                return NotFound();
            //var winch2 = Context.Winchs.Where(w => w.Id == winch.Id).FirstOrDefault();
            //if (winch2 == null)
            //    return NotFound();

            //winch2.WinchNumber = winch.WinchNumber;
            //winch2.Phone=winch.Phone;
            //winch2.Details = winch.Details;
            //winch2.Booke= winch.Booke;

            Context.Winchs.Update(winch);
            Context.SaveChanges();




            return RedirectToAction(nameof(AllWinches));
        }

        public IActionResult DeleteWinches(int id)
        {
            if (HttpContext.Session.GetString("_User") == null)
                return NotFound();

            var userId = Auth.UserId;
            var role = Auth.RoleId;
            if (role != 2 || userId <= 0)
                return NotFound();
            if (id < 2)
                return NotFound();

            var winch = Context.Winchs.Where(w => w.Id == id ).FirstOrDefault();
            if (winch == null)
                return NotFound();
            Context.Winchs.Remove(winch);
            Context.SaveChanges();

            return RedirectToAction(nameof(AllWinches));
        }
    }
}
