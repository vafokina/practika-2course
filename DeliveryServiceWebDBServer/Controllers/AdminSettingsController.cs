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
                    User temp; AccountReference temp1; DistributionCentre temp3; City temp4; Region temp5; Country temp6; Tariff temp7;
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
                        case "CreateCity":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 3) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp4 = new City();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp4.NameCity = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp4.RegionId = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            return RedirectToAction("CreateCityConfirmed", "AdminSettings", temp4);
                        case "EditCity":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 4) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp4 = new City();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp4.Id = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp4.NameCity = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[3]);
                            if (cur_lenght > 0) temp4.RegionId = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            return RedirectToAction("EditCityConfirmed", "AdminSettings", temp4);
                        case "DeleteCity":
                            return RedirectToAction("DeleteCityConfirmed", "AdminSettings", new { id = Convert.ToInt32(model.SavedObject) });
                        case "CreateRegion":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 3) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp5 = new Region();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp5.NameRegion = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp5.CountryId = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            return RedirectToAction("CreateRegionConfirmed", "AdminSettings", temp5);
                        case "EditRegion":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 4) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp5 = new Region();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp5.Id = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp5.NameRegion = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[3]);
                            if (cur_lenght > 0) temp5.CountryId = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            return RedirectToAction("EditRegionConfirmed", "AdminSettings", temp5);
                        case "DeleteRegion":
                            return RedirectToAction("DeleteRegionConfirmed", "AdminSettings", new { id = Convert.ToInt32(model.SavedObject) });
                        case "CreateCountry":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 2) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp6 = new Country();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp6.NameCountry = model.SavedObject.Substring(pos, cur_lenght);
                            return RedirectToAction("CreateCountryConfirmed", "AdminSettings", temp6);
                        case "EditCountry":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 3) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp6 = new Country();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp6.Id = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp6.NameCountry = model.SavedObject.Substring(pos, cur_lenght);
                            return RedirectToAction("EditCountryConfirmed", "AdminSettings", temp6);
                        case "DeleteCountry":
                            return RedirectToAction("DeleteCountryConfirmed", "AdminSettings", new { id = Convert.ToInt32(model.SavedObject) });
                        case "CreateTariff":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 4) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp7 = new Tariff();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp7.Name = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp7.Formula = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[3]);
                            if (cur_lenght > 0) temp7.Comment = model.SavedObject.Substring(pos, cur_lenght);
                            return RedirectToAction("CreateTariffConfirmed", "AdminSettings", temp7);
                        case "EditTariff":
                            pos = Helper.GetPosOfSplit(model.SavedObject, 5) + 1;
                            l = model.SavedObject.Substring(0, pos);
                            lenghts = l.Split('/');

                            temp7 = new Tariff();
                            cur_lenght = Convert.ToInt32(lenghts[1]);
                            if (cur_lenght > 0) temp7.Id = Convert.ToInt32(model.SavedObject.Substring(pos, cur_lenght));
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[2]);
                            if (cur_lenght > 0) temp7.Name = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[3]);
                            if (cur_lenght > 0) temp7.Formula = model.SavedObject.Substring(pos, cur_lenght);
                            pos += cur_lenght;
                            cur_lenght = Convert.ToInt32(lenghts[4]);
                            if (cur_lenght > 0) temp7.Comment = model.SavedObject.Substring(pos, cur_lenght);
                            return RedirectToAction("EditTariffConfirmed", "AdminSettings", temp7);
                        case "DeleteTariff":
                            return RedirectToAction("DeleteTariffConfirmed", "AdminSettings", new { id = Convert.ToInt32(model.SavedObject) });
                    }
                    //return RedirectToAction(model.Action, "AdminSettings", );
                }
                else
                {
                    ModelState.AddModelError("", "Пароль введен неверно");
                    if (model.Number <= 0) return RedirectToAction("Logoff", "Account");
                }
            }
            return RedirectToAction("ConfirmRepeat", "AdminSettings", model);
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
                    string temp = "/" + user.Login.Length + "/" + user.Password.Length + "/" + user.Admin.ToString().Length + "/";
                    temp += user.Login;
                    temp += user.Password;
                    temp += user.Admin.ToString();
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
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                TempData["alertMessage"] = "Аккаунт успешно удален.";
            }
            catch (Exception)
            {
                TempData["alertMessage"] = "Обнаружена ошибка. Возможно у удаляемого объекта есть зависисмые от него элементы.";
            }
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
                }
                else
                {
                    ModelState.AddModelError("", "Аккаунт службы доставки может иметь только один закрепленный за ним пункт выдачи," +
               " аккаунт интернет-магазина может иметь несколько закрепленных за ним отправителей");
                }
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
            try
            {
                AccountReference reference = db.AccountReferences.Find(id);
                db.AccountReferences.Remove(reference);
                db.SaveChanges();
                TempData["alertMessage"] = "Взаимосвязь успешно удалена.";
            }
            catch (Exception)
            {
                TempData["alertMessage"] = "Обнаружена ошибка. Возможно у удаляемого объекта есть зависисмые от него элементы.";
            }
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
                    string temp;
                    if (centre.Description != null)
                        temp = "/" + centre.Index.Length + "/" + centre.CityId.ToString().Length + "/" + centre.Address.Length + "/" + centre.Description.Length.ToString() + "/";
                    else temp = "/" + centre.Index.Length + "/" + centre.CityId.ToString().Length + "/" + centre.Address.Length + "/0/";
                    temp += centre.Index;
                    temp += centre.CityId.ToString();
                    temp += centre.Address;
                    if (centre.Description != null) temp += centre.Description;
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
                    string temp;
                    if (centre.Description != null)
                        temp = "/" + centre.Id.ToString().Length + "/" + centre.Index.Length + "/" + centre.CityId.ToString().Length + "/" + centre.Address.Length + "/" + centre.Description.Length.ToString() + "/";
                    else temp = "/" + centre.Id.ToString().Length + "/" + centre.Index.Length + "/" + centre.CityId.ToString().Length + "/" + centre.Address.Length + "/0/";
                    temp += centre.Id.ToString();
                    temp += centre.Index;
                    temp += centre.CityId.ToString();
                    temp += centre.Address;
                    if (centre.Description != null) temp += centre.Description;
                    return RedirectToAction("Confirm", new { act = "EditCentre", obj = temp });
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
            try
            {
                DistributionCentre centre = db.DistributionCentres.Find(id);
                db.DistributionCentres.Remove(centre);
                db.SaveChanges();
                TempData["alertMessage"] = "Пункт выдачи успешно удален.";
            }
            catch (Exception)
            {
                TempData["alertMessage"] = "Обнаружена ошибка. Возможно у удаляемого объекта есть зависисмые от него элементы.";
            }
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
        public ActionResult CreateCity()
        {
            List<IdValueModel> regions = new List<IdValueModel>();
            regions.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (Region p in db.Regions)
            {
                string str1 = p.NameRegion + ", " + p.Country.NameCountry;
                regions.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.RegionId = new SelectList(regions, "Id", "Value", null);
            return View();
        }
        [HttpPost]
        public ActionResult CreateCity([Bind(Include = "Id,NameCity,RegionId")]City city)
        {
            if (ModelState.IsValid)
            {
                if (db.Cities.Where(a => a.NameCity == city.NameCity && a.RegionId == city.RegionId).Count() == 0)
                {
                    string temp;
                    temp = "/" + city.NameCity.Length + "/" + city.RegionId.ToString().Length + "/";
                    temp += city.NameCity;
                    temp += city.RegionId.ToString();
                    return RedirectToAction("Confirm", new { act = "CreateCity", obj = temp });
                }
                else { ModelState.AddModelError("", "Такой город уже существует"); }
            }
            List<IdValueModel> regions = new List<IdValueModel>();
            regions.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (Region p in db.Regions)
            {
                string str1 = p.NameRegion + ", " + p.Country.NameCountry;
                regions.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.RegionId = new SelectList(regions, "Id", "Value", null);
            return View(city);
        }
        public ActionResult CreateCityConfirmed(City city)
        {
            db.Cities.Add(city);
            db.SaveChanges();
            TempData["alertMessage"] = "Город успешно создан.";
            return RedirectToAction("CitiesSettings");
        }
        public ActionResult EditCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            List<IdValueModel> regions = new List<IdValueModel>();
            regions.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (Region p in db.Regions)
            {
                string str1 = p.NameRegion + ", " + p.Country.NameCountry;
                regions.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.RegionId = new SelectList(regions, "Id", "Value", null);
            return View(city);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCity([Bind(Include = "Id,NameCity,RegionId")] City city)
        {
            if (ModelState.IsValid)
            {
                if (db.Cities.Where(a => a.NameCity == city.NameCity && a.RegionId == city.RegionId && a.Id != city.Id).Count() == 0)
                {
                    string temp;
                    temp = "/" + city.Id.ToString().Length + "/" + city.NameCity.Length + "/" + city.RegionId.ToString().Length + "/";
                    temp += city.Id.ToString();
                    temp += city.NameCity;
                    temp += city.RegionId.ToString();
                    return RedirectToAction("Confirm", new { act = "EditCity", obj = temp });
                }
                else { ModelState.AddModelError("", "Такой Город уже существует"); }
            }
            List<IdValueModel> regions = new List<IdValueModel>();
            regions.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (Region p in db.Regions)
            {
                string str1 = p.NameRegion + ", " + p.Country.NameCountry;
                regions.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.RegionId = new SelectList(regions, "Id", "Value", null);
            return View(city);
        }
        public ActionResult EditCityConfirmed(City city)
        {
            db.Entry(city).State = EntityState.Modified;
            db.SaveChanges();
            TempData["alertMessage"] = "Город успешно изменен.";
            return RedirectToAction("CitiesSettings");
        }
        public ActionResult DeleteCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return RedirectToAction("Confirm", new { act = "DeleteCity", obj = id.ToString() });
        }
        public ActionResult DeleteCityConfirmed(int id)
        {
            try
            {
                City city = db.Cities.Find(id);
                db.Cities.Remove(city);
                db.SaveChanges();
                TempData["alertMessage"] = "Город успешно удален.";
            }
            catch (Exception)
            {
                TempData["alertMessage"] = "Обнаружена ошибка. Возможно у удаляемого объекта есть зависисмые от него элементы.";
            }
            return RedirectToAction("CitiesSettings");
        }
        public ActionResult CreateRegion()
        {
            List<IdValueModel> countries = new List<IdValueModel>();
            countries.Add(new IdValueModel { Id = null, Value = "(Не выбрана)" });
            foreach (Country p in db.Countries)
            {
                string str1 = p.NameCountry;
                countries.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.CountryId = new SelectList(countries, "Id", "Value", null);
            return View();
        }
        [HttpPost]
        public ActionResult CreateRegion([Bind(Include = "Id,Nameregion,CountryId")]Region region)
        {
            if (ModelState.IsValid)
            {
                if (db.Cities.Where(a => a.NameCity == region.NameRegion && a.RegionId == region.CountryId).Count() == 0)
                {
                    string temp;
                    temp = "/" + region.NameRegion.Length + "/" + region.CountryId.ToString().Length + "/";
                    temp += region.NameRegion;
                    temp += region.CountryId.ToString();
                    return RedirectToAction("Confirm", new { act = "CreateRegion", obj = temp });
                }
                else { ModelState.AddModelError("", "Такой регион уже существует"); }
            }
            List<IdValueModel> countries = new List<IdValueModel>();
            countries.Add(new IdValueModel { Id = null, Value = "(Не выбрана)" });
            foreach (Country p in db.Countries)
            {
                string str1 = p.NameCountry;
                countries.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.CountryId = new SelectList(countries, "Id", "Value", null);
            return View(region);
        }
        public ActionResult CreateRegionConfirmed(Region region)
        {
            db.Regions.Add(region);
            db.SaveChanges();
            TempData["alertMessage"] = "Регион успешно создан.";
            return RedirectToAction("CitiesSettings");
        }
        public ActionResult EditRegion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            List<IdValueModel> countries = new List<IdValueModel>();
            countries.Add(new IdValueModel { Id = null, Value = "(Не выбрана)" });
            foreach (Country p in db.Countries)
            {
                string str1 = p.NameCountry;
                countries.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.CountryId = new SelectList(countries, "Id", "Value", null);
            return View(region);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRegion([Bind(Include = "Id,NameRegion,CountryId")] Region region)
        {
            if (ModelState.IsValid)
            {
                if (db.Regions.Where(a => a.NameRegion == region.NameRegion && a.CountryId == region.CountryId && a.Id != region.Id).Count() == 0)
                {
                    string temp;
                    temp = "/" + region.Id.ToString().Length + "/" + region.NameRegion.Length + "/" + region.CountryId.ToString().Length + "/";
                    temp += region.Id.ToString();
                    temp += region.NameRegion;
                    temp += region.CountryId.ToString();
                    return RedirectToAction("Confirm", new { act = "EditRegion", obj = temp });
                }
                else { ModelState.AddModelError("", "Такой регион уже существует"); }
            }
            List<IdValueModel> countries = new List<IdValueModel>();
            countries.Add(new IdValueModel { Id = null, Value = "(Не выбрана)" });
            foreach (Country p in db.Countries)
            {
                string str1 = p.NameCountry;
                countries.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.CountryId = new SelectList(countries, "Id", "Value", null);
            return View(region);
        }
        public ActionResult EditRegionConfirmed(Region region)
        {
            db.Entry(region).State = EntityState.Modified;
            db.SaveChanges();
            TempData["alertMessage"] = "Регион успешно изменен.";
            return RedirectToAction("CitiesSettings");
        }
        public ActionResult DeleteRegion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return RedirectToAction("Confirm", new { act = "DeleteRegion", obj = id.ToString() });
        }
        public ActionResult DeleteRegionConfirmed(int id)
        {
            try
            {
                Region region = db.Regions.Find(id);
                db.Regions.Remove(region);
                db.SaveChanges();
                TempData["alertMessage"] = "Регион успешно удален.";
            }
            catch (Exception)
            {
                TempData["alertMessage"] = "Обнаружена ошибка. Возможно у удаляемого объекта есть зависисмые от него элементы.";
            }
            return RedirectToAction("CitiesSettings");
        }
        public ActionResult CreateCountry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCountry([Bind(Include = "Id,NameCountry")]Country country)
        {
            if (ModelState.IsValid)
            {
                if (db.Countries.Where(a => a.NameCountry == country.NameCountry).Count() == 0)
                {
                    string temp;
                    temp = "/" + country.NameCountry.Length + "/";
                    temp += country.NameCountry;
                    return RedirectToAction("Confirm", new { act = "CreateCountry", obj = temp });
                }
                else { ModelState.AddModelError("", "Такая страна уже существует"); }
            }
            return View(country);
        }
        public ActionResult CreateCountryConfirmed(Country country)
        {
            db.Countries.Add(country);
            db.SaveChanges();
            TempData["alertMessage"] = "Страна успешно создана.";
            return RedirectToAction("CitiesSettings");
        }
        public ActionResult EditCountry(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(country);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCountry([Bind(Include = "Id,NameCountry")] Country country)
        {
            if (ModelState.IsValid)
            {
                if (db.Countries.Where(a => a.NameCountry == country.NameCountry && a.Id != country.Id).Count() == 0)
                {
                    string temp;
                    temp = "/" + country.Id.ToString().Length + "/" + country.NameCountry.Length + "/";
                    temp += country.Id.ToString();
                    temp += country.NameCountry;
                    return RedirectToAction("Confirm", new { act = "EditCountry", obj = temp });
                }
                else { ModelState.AddModelError("", "Такая страна уже существует"); }
            }
            return View(country);
        }
        public ActionResult EditCountryConfirmed(Country country)
        {
            db.Entry(country).State = EntityState.Modified;
            db.SaveChanges();
            TempData["alertMessage"] = "Страна успешно изменена.";
            return RedirectToAction("CitiesSettings");
        }
        public ActionResult DeleteCountry(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return RedirectToAction("Confirm", new { act = "DeleteCountry", obj = id.ToString() });
        }
        public ActionResult DeleteCountryConfirmed(int id)
        {
            try
            {
                Country country = db.Countries.Find(id);
                db.Countries.Remove(country);
                db.SaveChanges();
                TempData["alertMessage"] = "Страна успешно удалена.";
            }
            catch (Exception)
            {
                TempData["alertMessage"] = "Обнаружена ошибка. Возможно у удаляемого объекта есть зависисмые от него элементы.";
            }
            return RedirectToAction("CitiesSettings");
        }
        public ActionResult TariffsSettings()
        {
            return View(db.Tariffs.AsEnumerable());
        }
        public ActionResult CreateTariff()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTariff([Bind(Include = "Id,Name,Comment,Formula")]Tariff tariff)
        {
            if (ModelState.IsValid)
            {
                if (db.Tariffs.Where(a => a.Name == tariff.Name).Count() == 0)
                {
                    string temp;
                    if (tariff.Comment != null)
                        temp = "/" + tariff.Name.Length + "/" + tariff.Formula.Length + "/" + tariff.Comment.Length + "/";
                    else temp = "/" + tariff.Name.Length + "/" + tariff.Formula.Length + "/0/";
                    temp += tariff.Name;
                    temp += tariff.Formula;
                    if (tariff.Comment != null) temp += tariff.Comment;
                    return RedirectToAction("Confirm", new { act = "CreateTariff", obj = temp });
                }
                else { ModelState.AddModelError("", "Тариф с таким названием уже существует"); }
            }
            return View(tariff);
        }
        public ActionResult CreateTariffConfirmed(Tariff tariff)
        {
            db.Tariffs.Add(tariff);
            db.SaveChanges();
            TempData["alertMessage"] = "Тариф успешно создан.";
            return RedirectToAction("TariffsSettings");
        }
        public ActionResult EditTariff(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tariff tariff = db.Tariffs.Find(id);
            if (tariff == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(tariff);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTariff([Bind(Include = "Id,Name,Comment,Formula")] Tariff tariff)
        {
            if (ModelState.IsValid)
            {
                if (db.Tariffs.Where(a => a.Name == tariff.Name && a.Id != tariff.Id).Count() == 0)
                {
                    string temp;
                    if (tariff.Comment != null)
                        temp = "/" + tariff.Id.ToString().Length + "/" + tariff.Name.Length + "/" + tariff.Formula.Length + "/" + tariff.Comment.Length + "/";
                    else temp = "/" + tariff.Id.ToString().Length + "/" + tariff.Name.Length + "/" + tariff.Formula.Length + "/0/";
                    temp += tariff.Id;
                    temp += tariff.Name;
                    temp += tariff.Formula;
                    if (tariff.Comment != null) temp += tariff.Comment;
                    return RedirectToAction("Confirm", new { act = "EditTariff", obj = temp });
                }
                else { ModelState.AddModelError("", "Такой тариф уже существует"); }
            }
            return View(tariff);
        }
        public ActionResult EditTariffConfirmed(Tariff tariff)
        {
            db.Entry(tariff).State = EntityState.Modified;
            db.SaveChanges();
            TempData["alertMessage"] = "Тариф успешно изменен.";
            return RedirectToAction("TariffsSettings");
        }
        public ActionResult DeleteTariff(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tariff tariff = db.Tariffs.Find(id);
            if (tariff == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return RedirectToAction("Confirm", new { act = "DeleteTariff", obj = id.ToString() });
        }
        public ActionResult DeleteTariffConfirmed(int id)
        {
            try
            {
                Tariff tariff = db.Tariffs.Find(id);
                db.Tariffs.Remove(tariff);
                db.SaveChanges();
                TempData["alertMessage"] = "Тариф успешно удален.";
            }
            catch (Exception)
            {
                TempData["alertMessage"] = "Обнаружена ошибка. Возможно у удаляемого объекта есть зависисмые от него элементы.";
            }
            return RedirectToAction("TariffsSettings");
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