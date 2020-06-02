using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Aspose.Words;
using DeliveryServiceWebDBServer.Models;

namespace DeliveryServiceWebDBServer.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles ="admin,user")]
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                // TempData["alertMessage"] = "ADMIN";
                return RedirectToAction("Index", "Admin", null);
            }
            else
            {
                // TempData["alertMessage"] = "USER";
                return RedirectToAction("Index", "User", null);
            }
        }

        public ActionResult Help()
        {
            try
            {
                // Путь к файлу
                string file_path = Server.MapPath("~/App_Data/help.pdf");
                // Тип файла
                string file_type = "application/octet-stream"; // универсальный тип
                // Имя файла (необязательно)
                string file_name = "help - DeliveryServiceWebDBServer.pdf";
                return File(file_path, file_type, file_name);
            }
            catch(System.Web.HttpCompileException)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
        }

        [Authorize(Roles = "admin,user")]
        public ActionResult Settings()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "AdminSettings", null);
            }
            else
            {
                return RedirectToAction("Index", "UserSettings", null);
            }
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost]
        public ActionResult Search(SearchIdModel searchId)
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Search", "Admin", searchId );
            }
            else
            {
                return RedirectToAction("Search", "User",  searchId );
            }
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost]
        public FileResult ExportFile(string editor)
        {
            try
            {
                // Create a unique file name
                string fileName = Guid.NewGuid() + ".docx";
                // Convert HTML text to byte array
                byte[] byteArray = Encoding.UTF8.GetBytes(editor.Contains("<html>") ? editor : "<html>" + editor + "</html>");
                // Generate Word document from the HTML
                MemoryStream stream = new MemoryStream(byteArray);
                Document document = new Document(stream);
                // Create memory stream for the Word file
                var outputStream = new MemoryStream();
                document.Save(outputStream, SaveFormat.Docx);
                outputStream.Position = 0;
                // Return generated Word file
                return File(outputStream, System.Net.Mime.MediaTypeNames.Application.Rtf, fileName);
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        [Authorize(Roles = "admin,user")]
        public ActionResult Import()
        {
            return View();
        }
        [Authorize(Roles = "admin,user")]
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                //string path = Server.MapPath("~/Uploads/");
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}

                //postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                //ViewBag.Message = "File uploaded successfully.";
            }

            return View();
        }
        [Authorize(Roles = "admin,user")]
        public ActionResult DownloadTemplate()
        {
            try
            {
                // Путь к файлу
                string file_path = Server.MapPath("~/App_Data/XMLTemplate.xml");
                // Тип файла
                string file_type = "application/octet-stream"; // универсальный тип
                // Имя файла (необязательно)
                string file_name = "XMLTemplate - DeliveryServiceWebDBServer.xml";
                return File(file_path, file_type, file_name);
            }
            catch (System.Web.HttpCompileException)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
        }
        [Authorize(Roles = "admin,user")]
        public ActionResult DownloadSchema()
        {
            try
            {
                // Путь к файлу
                string file_path = Server.MapPath("~/App_Data/XMLSchema.xsd");
                // Тип файла
                string file_type = "application/octet-stream"; // универсальный тип
                // Имя файла (необязательно)
                string file_name = "XMLSchema - DeliveryServiceWebDBServer.xsd";
                return File(file_path, file_type, file_name);
            }
            catch (System.Web.HttpCompileException)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}