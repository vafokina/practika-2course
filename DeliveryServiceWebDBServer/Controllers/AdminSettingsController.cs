using DeliveryServiceWebDBServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DeliveryServiceWebDBServer.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminSettingsController : Controller
    {
        private ModelDBContainer db = new ModelDBContainer();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Confirm(string obj, string act)
        {
            return View(new ConfirmModel { Number = 3, ConfirmedAction = act, SavedObject = obj });
        }
        [HttpGet]
        public ActionResult ConfirmRepeat(ConfirmModel model)
        {
            return View("Confirm", model);
        }
        [HttpPost]
        public ActionResult Confirm([Bind(Include = "Number,Password,SavedObject,ConfirmedAction")]ConfirmModel model)
        {
            if (ModelState.IsValid)
            {
                model.Number = model.Number - 1;
                User user = db.Users.FirstOrDefault(u => u.Login == User.Identity.Name && u.Password == model.Password);
                if (user != null)
                {
                    User temp; AccountReference temp1; DistributionCentre temp3;
                    int cur_lenght, pos; string[] lenghts; string l;
                    switch (model.ConfirmedAction)
                    {
                        case "CreateUser":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 4) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp = new User();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp.Login = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp.Password = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[3]);
                            if (cur_lenght > 0) temp.Admin = Boolean.Parse(model.SavedObject.Substring(pos, cur_lenght));
                            return RedirectToAction("CreateUserConfirmed", "AdminSettings", temp);
                        case "EditUser":
                             pos = Helper.GetPosOfSplit(model.SavedObject, 5) + 1;
                             l = model.SavedObject.Substring(0, pos);
                             lenghts = l.Split('/');

                             temp = new User();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp.Id = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp.Login = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[3]);
                            if (cur_lenght > 0) temp.Password = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[4]);
                            if (cur_lenght > 0) temp.Admin = Boolean.Parse(model.SavedObject.Substring(pos, cur_lenght));
                            return RedirectToAction("EditUserConfirmed", "AdminSettings", temp);
                        case "DeleteUser":
                            return RedirectToAction("DeleteUserConfirmed", "AdminSettings", new { id = Convert.ToInt32(model.SavedObject) });
                        case "CreateReference":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 4) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp1 = new AccountReference();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp1.UserId = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp1.PersonId = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[3]);
                            if (cur_lenght > 0) temp1.CentreId = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            return RedirectToAction("CreateReferenceConfirmed", "AdminSettings", temp1);
                        case "DeleteReference":
                            return RedirectToAction("DeleteReferenceConfirmed", "AdminSettings", new { id = Convert.ToInt32(model.SavedObject) });
                        case "CreateCentre":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 5) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp3 = new DistributionCentre();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp3.Index = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp3.CityId = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[3]);
                            if (cur_lenght > 0) temp3.Address = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[4]);
                            if (cur_lenght > 0) temp3.Description = model.SavedObject.Substring(pos, cur_lenght);
                            return RedirectToAction("CreateCentreConfirmed", "AdminSettings", temp3);
                        case "EditCentre":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 6) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp3 = new DistributionCentre();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp3.Id = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp3.Index = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[3]);
                            if (cur_lenght > 0) temp3.CityId = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[4]);
                            if (cur_lenght > 0) temp3.Address = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[5]);
                            if (cur_lenght > 0) temp3.Description = model.SavedObject.Substring(pos, cur_lenght);
                            return RedirectToAction("EditCentreConfirmed", "AdminSettings", temp3);
                        case "DeleteCentre":
                            return RedirectToAction("DeleteCentreConfirmed", "AdminSettings", new { id = Convert.ToInt32(model.SavedObject) });
                    }
                    //return RedirectToAction(model.Action, "AdminSettings", );
                }
                else
                {
                    ModelState.AddModelError("", "Пароль введен неверно");
                    if (model.Number <= 0) return RedirectToAction("Logoff", "Account");
                }
            }
            return RedirectToAction("ConfirmRepeat", "AdminSettings", model );
        }

        public ActionResult AccountsSettings()
        {
            return View(db.Users.AsEnumerable());
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser([Bind(Include = "Id,Login,Password,Admin")]User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Where(a => a.Login == user.Login).Count() == 0)
                {
                    string temp = "/" + user.Login.Length + "/" + user.Password.Length+"/"+ user.Admin.ToString() .Length+"/";
                    temp+=user.Login;
                    temp+=user.Password;
                    temp+=user.Admin.ToString();
                    return RedirectToAction("Confirm", new { act = "CreateUser", obj = temp });
                }
                else { ModelState.AddModelError("", "Пользователь с таким логином уже существует"); }
            }
            return View(user);
        }
        public ActionResult CreateUserConfirmed(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            TempData["alertMessage"] = "Аккаунт успешно создан.";
            return RedirectToAction("AccountsSettings");
        }
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "Id,Login,Password,Admin")] User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Where(a => a.Login == user.Login && a.Id != user.Id).Count() == 0)
                {
                    string temp = "/" + user.Id.ToString().Length + "/" + user.Login.Length + "/" + user.Password.Length + "/" + user.Admin.ToString().Length + "/";
                    temp += user.Id.ToString();
                    temp += user.Login;
                    temp += user.Password;
                    temp += user.Admin.ToString();
                    return RedirectToAction("Confirm", new { act = "EditUser", obj = temp });
                }
                else { ModelState.AddModelError("", "Пользователь с таким логином уже существует"); }
            }
            return View(user);
        }
        public ActionResult EditUserConfirmed(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            TempData["alertMessage"] = "Аккаунт успешно изменен.";
            if (user.Login == User.Identity.Name) return RedirectToAction("Logoff", "Account");
            return RedirectToAction("AccountsSettings");
        }
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (user.Login == User.Identity.Name)
            {
                TempData["alertMessage"] = "Вы не можете удалить собственный аккаунт.";
                return RedirectToAction("AccountsSettings");
            }
            return RedirectToAction("Confirm", new { act = "DeleteUser", obj = id.ToString() });
        }
        public ActionResult DeleteUserConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            TempData["alertMessage"] = "Аккаунт успешно удален.";
            return RedirectToAction("AccountsSettings");
        }

        public ActionResult ReferencesSettings()
        {
            return View(db.AccountReferences.AsEnumerable());
        }
        public ActionResult CreateReference()
        {
            List<IdValueModel> centres = new List<IdValueModel>();
            centres.Add(new IdValueModel { Id = null, Value = "(Не выбрано)" });
            foreach (DistributionCentre p in db.DistributionCentres)
            {
                centres.Add(new IdValueModel { Id = p.Id, Value = Helper.GetCentreString(p) });
            }
            List<IdValueModel> persons = new List<IdValueModel>();
            persons.Add(new IdValueModel { Id = null, Value = "(Не выбрано)" });
            foreach (Person p in db.Persons)
            {
                persons.Add(new IdValueModel { Id = p.Id, Value = Helper.GetPersonString(p) });
            }
            ViewBag.CentreId = new SelectList(centres, "Id", "Value", null);
            ViewBag.PersonId = new SelectList(persons, "Id", "Value", null);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", null);
            return View();
        }
        [HttpPost]
        public ActionResult CreateReference([Bind(Include = "Id,UserId,PersonId,CentreId")]AccountReference reference)
        {
            if (ModelState.IsValid)
            {
                User userForRef = db.Users.Find(reference.UserId);
                if ((userForRef.Admin && reference.CentreId != null && reference.PersonId == null) ||
                    ((!userForRef.Admin) && reference.CentreId == null && reference.PersonId != null))
                {
                    if (!userForRef.Admin || userForRef.Admin && db.AccountReferences.Where(a => a.UserId == reference.UserId).Count() == 0)
                    {
                        string temp = "/" + reference.UserId.ToString().Length + "/" + reference.PersonId.ToString().Length + "/" + reference.CentreId.ToString().Length + "/";
                        temp += reference.UserId.ToString();
                        temp += reference.PersonId.ToString();
                        temp += reference.CentreId.ToString();
                        return RedirectToAction("Confirm", new { act = "CreateReference", obj = temp });
                    }
                    else { ModelState.AddModelError("", "За аккаунтом службы доставки уже закреплен пункт выдачи"); }
                } else { ModelState.AddModelError("", "Аккаунт службы доставки может иметь только один закрепленный за ним пункт выдачи," +
                    " аккаунт интернет-магазина может иметь несколько закрепленных за ним отправителей"); }
            }
            List<IdValueModel> centres = new List<IdValueModel>();
            centres.Add(new IdValueModel { Id = null, Value = "(Не выбрано)" });
            foreach (DistributionCentre p in db.DistributionCentres)
            {
                centres.Add(new IdValueModel { Id = p.Id, Value = Helper.GetCentreString(p) });
            }
            List<IdValueModel> persons = new List<IdValueModel>();
            persons.Add(new IdValueModel { Id = null, Value = "(Не выбрано)" });
            foreach (Person p in db.Persons)
            {
                persons.Add(new IdValueModel { Id = p.Id, Value = Helper.GetPersonString(p) });
            }
            ViewBag.CentreId = new SelectList(centres, "Id", "Value", null);
            ViewBag.PersonId = new SelectList(persons, "Id", "Value", null);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Login", null);
            return View(reference);
        }
        public ActionResult CreateReferenceConfirmed(AccountReference reference)
        {
            db.AccountReferences.Add(reference);
            db.SaveChanges();
            TempData["alertMessage"] = "Взаимосвязь успешно создана.";
            return RedirectToAction("ReferencesSettings");
        }
        public ActionResult DeleteReference(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountReference reference = db.AccountReferences.Find(id);
            if (reference == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return RedirectToAction("Confirm", new { act = "DeleteReference", obj = id.ToString() });
        }
        public ActionResult DeleteReferenceConfirmed(int id)
        {
            AccountReference reference = db.AccountReferences.Find(id);
            db.AccountReferences.Remove(reference);
            db.SaveChanges();
            TempData["alertMessage"] = "Взаимосвязь успешно удалена.";
            return RedirectToAction("ReferencesSettings");
        }

        public ActionResult CentresSettings()
        {
            return View(db.DistributionCentres.AsEnumerable());
        }
        public ActionResult CreateCentre()
        {
            List<IdValueModel> cities = new List<IdValueModel>();
            cities.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (City p in db.Cities)
            {
                string str1 = p.NameCity + ", " + p.Region.NameRegion + ", " + p.Region.Country.NameCountry;
                cities.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.CityId = new SelectList(cities, "Id", "Value", null);
            return View();
        }
        [HttpPost]
        public ActionResult CreateCentre([Bind(Include = "Id,Index,CityId,Address,Description")]DistributionCentre centre)
        {
            if (ModelState.IsValid)
            {
                if (db.DistributionCentres.Where(a => a.CityId == centre.CityId && a.Address == centre.Address).Count() == 0)
                {
                    string temp = "/" + centre.Index.Length + "/" + centre.CityId.ToString().Length + "/" + centre.Address.Length + "/" + centre.Description.Length + "/";
                    temp += centre.Index;
                    temp += centre.CityId.ToString();
                    temp += centre.Address;
                    temp += centre.Description;
                    return RedirectToAction("Confirm", new { act = "CreateCentre", obj = temp });
                }
                else { ModelState.AddModelError("", "Такой пункт выдачи уже существует"); }
            }
            List<IdValueModel> cities = new List<IdValueModel>();
            cities.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (City p in db.Cities)
            {
                string str1 = p.NameCity + ", " + p.Region.NameRegion + ", " + p.Region.Country.NameCountry;
                cities.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.CityId = new SelectList(cities, "Id", "Value", null);
            return View(centre);
        }
        public ActionResult CreateCentreConfirmed(DistributionCentre centre)
        {
            db.DistributionCentres.Add(centre);
            db.SaveChanges();
            TempData["alertMessage"] = "Пункт выдачи успешно создан.";
            return RedirectToAction("CentresSettings");
        }
        public ActionResult EditCentre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistributionCentre centre = db.DistributionCentres.Find(id);
            if (centre == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            List<IdValueModel> cities = new List<IdValueModel>();
            cities.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (City p in db.Cities)
            {
                string str1 = p.NameCity + ", " + p.Region.NameRegion + ", " + p.Region.Country.NameCountry;
                cities.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.CityId = new SelectList(cities, "Id", "Value", null);
            return View(centre);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCentre([Bind(Include = "Id,Index,CityId,Address,Description")] DistributionCentre centre)
        {
            if (ModelState.IsValid)
            {
                if (db.DistributionCentres.Where(a => a.CityId == centre.CityId && a.Address == centre.Address && a.Id != centre.Id).Count() == 0)
                {
                    string temp = "/" + centre.Id.ToString().Length + "/" + centre.Index.Length + "/" + centre.CityId.ToString().Length + "/" + centre.Address.Length + "/" + centre.Description.Length + "/";
                    temp += centre.Id.ToString();
                    temp += centre.Index;
                    temp += centre.CityId.ToString();
                    temp += centre.Address;
                    temp += centre.Description;
                    return RedirectToAction("Confirm", new { act = "EditUser", obj = temp });
                }
                else { ModelState.AddModelError("", "Такой пункт выдачи уже существует"); }
            }
            List<IdValueModel> cities = new List<IdValueModel>();
            cities.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (City p in db.Cities)
            {
                string str1 = p.NameCity + ", " + p.Region.NameRegion + ", " + p.Region.Country.NameCountry;
                cities.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.CityId = new SelectList(cities, "Id", "Value", null);
            return View(centre);
        }
        public ActionResult EditCentreConfirmed(DistributionCentre centre)
        {
            db.Entry(centre).State = EntityState.Modified;
            db.SaveChanges();
            TempData["alertMessage"] = "Пункт выдачи успешно изменен.";
            return RedirectToAction("CentresSettings");
        }
        public ActionResult DeleteCentre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistributionCentre centre = db.DistributionCentres.Find(id);
            if (centre == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return RedirectToAction("Confirm", new { act = "DeleteCentre", obj = id.ToString() });
        }
        public ActionResult DeleteCentreConfirmed(int id)
        {
            DistributionCentre centre = db.DistributionCentres.Find(id);
            db.DistributionCentres.Remove(centre);
            db.SaveChanges();
            TempData["alertMessage"] = "Пункт выдачи успешно удален.";
            return RedirectToAction("CentresSettings");
        }
        public ActionResult CitiesSettings()
        {
            return View(new CitiesRegionsCountriesModel
            {
                Cities = db.Cities.OrderBy(a => a.RegionId),
            Regions = db.Regions.OrderBy(a => a.CountryId),
                Countries = db.Countries.OrderBy(a => a.NameCountry)
            });
        }
        public ActionResult TariffsSettings()
        {
            return View();
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