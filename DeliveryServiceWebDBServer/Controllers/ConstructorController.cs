using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliveryServiceWebDBServer.Controllers
{
    [Authorize(Roles = "admin")]
    public class ConstructorController : Controller
    {
        public static Models.Constructor constructor = new Models.Constructor();
        // GET: Constructor
        public ActionResult Index() // отображение таблиц
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string[] table) // получение выбранных таблиц
        {
            if (table == null || table.Count() == 0)
            {
                ModelState.AddModelError("", "Необходимо выбрать хотя бы один объект");
                return View(table);
            }
            constructor.SelectedTables = table;
            constructor.Attributes = constructor.GetAttributes();
            return View("Index2", constructor); // отображение атрибутов
        }
        [HttpPost]
        public ActionResult Index2(Models.Constructor t) // получение выбранных атрибутов
        {
            constructor.SelectedAttributes = t.SelectedAttributes;
            constructor.References = constructor.GetReferences();
            if (constructor.SelectedTables.Length == 1) return View("Index4", constructor);
            return View("Index3", constructor); // отображение ссылок
        }
        [HttpPost]
        public ActionResult Index3(Models.Constructor t) //получение выбранных ссылок
        {
            constructor.SelectedReferences = t.SelectedReferences;
            constructor.Conditions = new List<string[]>();
            return View("Index4", constructor); //отображение окна условий
        }
        [HttpPost]
        public ActionResult Index4(Models.Constructor t)
        {
            string[] temp;
            if (t.DataType == "int")
            {
                temp = new string[4] { "int", t.NameCondition, t.IntCondition.ToString(), t.Sign };
            }
            else if (t.DataType == "double")
            {
                temp = new string[4] { "double", t.NameCondition, t.DoubleCondition.ToString(), t.Sign };
            }
            else if (t.DataType == "datetime")
            {
                temp = new string[4] { "datetime", t.NameCondition, t.DataTimeCondition.ToString(), t.Sign };
            }
            else
            {
                temp = new string[4] { "string", t.NameCondition, t.StringCondition.ToString(), null };
            }
            constructor.Conditions.Add(temp);
            return View("Index5", constructor); //отображение окна условий
        }
        public ActionResult Index5(Models.Constructor t)
        {
            string str = "SELECT ";
            for (int i = 0; i < constructor.SelectedAttributes.Length; i++)
            {
                str += constructor.TranslateAttributes(constructor.SelectedAttributes[i]);
                if (i == constructor.SelectedAttributes.Length - 1) str += " ";
                else str += ", ";
            }
            str += "FROM ";
            for (int i = 0; i < constructor.SelectedTables.Length; i++)
            {
                str += constructor.TranslateTables(constructor.SelectedTables[i]);
                if (i == constructor.SelectedTables.Length - 1) str += " ";
                else str += ", ";
            }
            if (constructor.Conditions.Count() > 0 || constructor.SelectedReferences.Length > 0)
            {
                str += "WHERE ";
                if (constructor.Conditions.Count() > 0)
                    for (int i = 0; i < constructor.Conditions.Count(); i++)
                    {
                        string[] el = constructor.Conditions[i];
                        str += constructor.TranslateAttributes(el[1]) + " ";
                        if (el[3] == null) str += "= N\"" + el[2] + "\"";
                        else if (el[3] == "=") str += "= " + el[2];
                        else str += el[3] + " @" + i + " ";
                        str += " ";
                        if (constructor.SelectedReferences.Length > 0 || i != constructor.Conditions.Count() - 1) str += "AND ";
                    }
                if (constructor.SelectedReferences.Length > 0)
                    for (int i = 0; i < constructor.SelectedReferences.Length; i++)
                    {
                        str += constructor.TranslateReferences(constructor.SelectedReferences[i]) + " ";
                        if (i != constructor.SelectedReferences.Length - 1) str += " AND ";
                    }
            }

            TempData["alertMessage"] = str;

            Models.ModelDBContainer db = new Models.ModelDBContainer();
            try
            {
                //if (constructor.Conditions.Count() > 0)
                //    for (int i = 0; i < constructor.Conditions.Count(); i++)
                //    {
                //        System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@name", "%Samsung%");
                //    }

                var res = db.Database.SqlQuery<object>(str);
            }
            catch (Exception ex)
            {
                TempData["alertMessage"] = str + " ERROR!!!";
            }
            db.Dispose();
            return View("Index5", constructor);
        }
    }
}