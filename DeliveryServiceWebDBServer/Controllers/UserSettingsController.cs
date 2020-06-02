using DeliveryServiceWebDBServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliveryServiceWebDBServer.Controllers
{
    [Authorize(Roles = "user")]
    public class UserSettingsController : Controller
    {
        private ModelDBContainer db = new ModelDBContainer();

        public ActionResult Index()
        {
            ViewBag.AccountReferences = db.AccountReferences.Where(a => a.User.Login == User.Identity.Name);
            return View(new OldNewPasswordModel());
        }

        public ActionResult DeleteReference(int id)
        {
            AccountReference reference = db.AccountReferences.Find(id);
            db.AccountReferences.Remove(reference);
            db.SaveChanges();
            TempData["alertMessage"] = "Зависимость удалена.";
            return RedirectToAction("Index", "UserSettings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword(OldNewPasswordModel passwords) // oldPass, newPass
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Where(a => a.Login == User.Identity.Name).First();
                if (user.Password == passwords.OldPassword)
                {
                    user.Password = passwords.NewPassword;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["alertMessage"] = "Пароль учетной записи успешно изменен.";
                    return RedirectToAction("Index", "UserSettings");
                }
                else
                {
                    ModelState.AddModelError("", "Текущий пароль введен неверно");
                }
            }
            ViewBag.AccountReferences = db.AccountReferences.Where(a => a.User.Login == User.Identity.Name);
            return View("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
