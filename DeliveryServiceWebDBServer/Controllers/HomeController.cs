using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Aspose.Words;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Drawing;
using DeliveryServiceWebDBServer.Models;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml;

namespace DeliveryServiceWebDBServer.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "admin,user")]
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
            catch (System.Web.HttpCompileException)
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
                return RedirectToAction("Search", "Admin", searchId);
            }
            else
            {
                return RedirectToAction("Search", "User", searchId);
            }
        }

        [Authorize(Roles = "admin,user")]
        public ActionResult ExportFile(int id)
        {
            try
            {
                //string editor = "";
                //editor = "<table><th>1</th><th>2</th></table>";
                //// Create a unique file name
                //string fileName = Guid.NewGuid() + ".docx";
                //// Convert HTML text to byte array
                //byte[] byteArray = Encoding.UTF8.GetBytes(editor.Contains("<html>") ? editor : "<html>" + editor + "</html>");
                //// Generate Word document from the HTML
                //MemoryStream stream = new MemoryStream(byteArray);
                //Document document = new Document(stream);
                //// Create memory stream for the Word file
                //var outputStream = new MemoryStream();
                //document.Save(outputStream, SaveFormat.Docx);
                //outputStream.Position = 0;
                //// Return generated Word file
                //return File(outputStream, System.Net.Mime.MediaTypeNames.Application.Rtf, fileName);

                Package package = null;
                DateTime date;
                string addressFrom = "", addressTo;
                Person From, To;

                using (ModelDBContainer db = new ModelDBContainer())
                {
                    package = db.Packages.Find(id);
                    From = package.PersonFrom;
                    To = package.PersonTo;
                    if (package == null) throw new Exception("The package was not found");
                    var maxDate = package.Records.Select(a => a.DateAndTime).Max();
                    date = package.Records.Where(a => a.DateAndTime == maxDate).Single().DateAndTime;
                    if (From.CentreId != null)
                    {
                        DistributionCentre c = db.DistributionCentres.Find(From.CentreId);
                        addressFrom = c.City.Region.Country.NameCountry + ", " + c.City.Region.NameRegion + ", " + c.City.NameCity + ", " + c.Address;
                    }
                    else
                    {
                        addressFrom = From.City.Region.Country.NameCountry + ", " + From.City.Region.NameRegion + ", " + From.City.NameCity + ", " + From.Address;
                    }
                    if (To.CentreId != null)
                    {
                        DistributionCentre c = db.DistributionCentres.Find(To.CentreId);
                        addressTo = c.City.Region.Country.NameCountry + ", " + c.City.Region.NameRegion + ", " + c.City.NameCity + ", " + c.Address;
                    }
                    else
                    {
                        addressTo = To.City.Region.Country.NameCountry + ", " + To.City.Region.NameRegion + ", " + To.City.NameCity + ", " + To.Address;
                    }
                }
                string companyName = Properties.Settings.Default.CompanyName;

                // Creating a new document.
                WordDocument document = new WordDocument();
                //Adding a new section to the document.
                WSection section = document.AddSection() as WSection;
                //Set Margin of the section
                section.PageSetup.Margins.All = 50;
                //Set page size of the section
                //section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);

                //Create Paragraph styles
                WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
                style.CharacterFormat.FontName = "Calibri";
                style.CharacterFormat.FontSize = 11f;
                style.ParagraphFormat.BeforeSpacing = 0;
                style.ParagraphFormat.AfterSpacing = 8;
                style.ParagraphFormat.LineSpacing = 13.8f;

                style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
                style.ApplyBaseStyle("Normal");
                style.CharacterFormat.FontName = "Calibri Light";
                style.CharacterFormat.FontSize = 16f;
                // style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
                style.ParagraphFormat.BeforeSpacing = 12;
                style.ParagraphFormat.AfterSpacing = 0;
                style.ParagraphFormat.Keep = true;
                style.ParagraphFormat.KeepFollow = true;
                style.ParagraphFormat.OutlineLevel = Syncfusion.DocIO.OutlineLevel.Level1;
                IWParagraph paragraph = section.AddParagraph();

                paragraph.ApplyStyle("Heading 1");
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                WTextRange textRange = paragraph.AppendText(companyName) as WTextRange;
                textRange.CharacterFormat.FontSize = 14f;
                textRange.CharacterFormat.FontName = "Calibri";

                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("#" + package.Id) as WTextRange;
                textRange.CharacterFormat.FontSize = 14f;
                textRange.CharacterFormat.FontName = "Calibri";

                paragraph.ApplyStyle("Normal");
                //Appends table.
                IWTable table = section.AddTable();
                table.ResetCells(16, 4);
                table.TableFormat.Borders.BorderType = BorderStyle.Single;
                table.TableFormat.IsAutoResized = true;

                //Appends paragraph.
                paragraph = table[1, 0].AddParagraph();
                textRange = paragraph.AppendText("ФИО") as WTextRange;
                paragraph = table[2, 0].AddParagraph();
                textRange = paragraph.AppendText("Компания") as WTextRange;
                paragraph = table[3, 0].AddParagraph();
                textRange = paragraph.AppendText("Адрес") as WTextRange;
                paragraph = table[4, 0].AddParagraph();
                textRange = paragraph.AppendText("Телефон") as WTextRange;

                string name1 = (From.Name == null ? "" : From.Name) + " " + (From.MiddleName == null ? "" : From.MiddleName) + " " + (From.Surname == null ? "" : From.Surname);
                paragraph = table[1, 1].AddParagraph();
                textRange = paragraph.AppendText(name1) as WTextRange;
                paragraph = table[2, 1].AddParagraph();
                textRange = paragraph.AppendText(From.Company == null ? "":From.Company) as WTextRange;
                paragraph = table[3, 1].AddParagraph();
                textRange = paragraph.AppendText(addressFrom) as WTextRange;
                paragraph = table[4, 1].AddParagraph();
                textRange = paragraph.AppendText(From.Phone == null ? "" : From.Phone) as WTextRange;

                paragraph = table[6, 0].AddParagraph();
                textRange = paragraph.AppendText("ФИО") as WTextRange;
                paragraph = table[7, 0].AddParagraph();
                textRange = paragraph.AppendText("Компания") as WTextRange;
                paragraph = table[8, 0].AddParagraph();
                textRange = paragraph.AppendText("Адрес") as WTextRange;
                paragraph = table[9, 0].AddParagraph();
                textRange = paragraph.AppendText("Телефон") as WTextRange;

                string name2 = (To.Name == null ? "" : To.Name) + " " + (To.MiddleName == null ? "" : To.MiddleName) + " " + (To.Surname == null ? "" : To.Surname);
                paragraph = table[6, 1].AddParagraph();
                textRange = paragraph.AppendText(name2) as WTextRange;
                paragraph = table[7, 1].AddParagraph();
                textRange = paragraph.AppendText(To.Company == null ? "" : To.Company) as WTextRange;
                paragraph = table[8, 1].AddParagraph();
                textRange = paragraph.AppendText(addressTo) as WTextRange;
                paragraph = table[9, 1].AddParagraph();
                textRange = paragraph.AppendText(To.Phone == null ? "" : To.Phone) as WTextRange;

                paragraph = table[11, 0].AddParagraph();
                textRange = paragraph.AppendText("Дата " + date.ToShortDateString()) as WTextRange;
                paragraph = table[11, 1].AddParagraph();
                textRange = paragraph.AppendText("Время") as WTextRange;
                paragraph = table[12, 1].AddParagraph();
                textRange = paragraph.AppendText("Подпись") as WTextRange;

                paragraph = table[1, 2].AddParagraph();
                textRange = paragraph.AppendText("Объяв.стоимость") as WTextRange;
                paragraph = table[2, 2].AddParagraph();
                textRange = paragraph.AppendText("Описание") as WTextRange;
                paragraph = table[3, 2].AddParagraph();
                textRange = paragraph.AppendText("Вес") as WTextRange;
                paragraph = table[4, 2].AddParagraph();
                textRange = paragraph.AppendText("Размеры") as WTextRange;
                paragraph = table[5, 2].AddParagraph();
                textRange = paragraph.AppendText("Количество") as WTextRange;
                paragraph = table[6, 2].AddParagraph();
                textRange = paragraph.AppendText("Стоимость") as WTextRange;
                paragraph = table[7, 2].AddParagraph();
                textRange = paragraph.AppendText("Курьер") as WTextRange;
                paragraph = table[7, 3].AddParagraph();
                textRange = paragraph.AppendText("Подпись") as WTextRange;

                paragraph = table[1, 3].AddParagraph();
                textRange = paragraph.AppendText(package.DeclaredValue == null ? "" : (package.DeclaredValue.ToString() + " руб")) as WTextRange;
                paragraph = table[2, 3].AddParagraph();
                textRange = paragraph.AppendText(package.Description) as WTextRange;
                paragraph = table[3, 3].AddParagraph();
                textRange = paragraph.AppendText(package.Weight.ToString() + " кг") as WTextRange;
                paragraph = table[4, 3].AddParagraph();
                textRange = paragraph.AppendText(package.Width +"*" + package.Length +"*"+ package.Height + " см") as WTextRange;
                paragraph = table[5, 3].AddParagraph();
                textRange = paragraph.AppendText(package.NumberOfPackages.ToString()) as WTextRange;
                paragraph = table[6, 3].AddParagraph();
                textRange = paragraph.AppendText(package.Cost == null ? "" : (package.Cost.ToString() + " руб")) as WTextRange;

                paragraph = table[15, 2].AddParagraph();
                textRange = paragraph.AppendText("Дата        Время") as WTextRange;
                paragraph = table[15, 3].AddParagraph();
                textRange = paragraph.AppendText("Подпись") as WTextRange;
                paragraph = table[14, 2].AddParagraph();
                textRange = paragraph.AppendText("Получатель(ФИО)") as WTextRange;

                paragraph = table[0, 0].AddParagraph();
                textRange = paragraph.AppendText("Отправитель") as WTextRange; textRange.CharacterFormat.Bold = true;
                paragraph = table[5, 0].AddParagraph();
                textRange = paragraph.AppendText("Получатель") as WTextRange; textRange.CharacterFormat.Bold = true;
                paragraph = table[13, 0].AddParagraph();
                textRange = paragraph.AppendText("Примечания") as WTextRange; textRange.CharacterFormat.Bold = true;
                paragraph = table[0, 2].AddParagraph();
                textRange = paragraph.AppendText("Информация об отправлении") as WTextRange; textRange.CharacterFormat.Bold = true;
                paragraph = table[13, 2].AddParagraph();
                textRange = paragraph.AppendText("Подтверждение доставки") as WTextRange; textRange.CharacterFormat.Bold = true;

                //Specifies the horizontal merge
                table.ApplyHorizontalMerge(0, 0, 1);
                table.ApplyHorizontalMerge(5, 0, 1);
                table.ApplyHorizontalMerge(13, 0, 1);
                table.ApplyHorizontalMerge(0, 2, 3);
                table.ApplyHorizontalMerge(13, 2, 3);

                //Appends paragraph.
                section.AddParagraph();

                //Saves the Word document to  MemoryStream
                MemoryStream stream = new MemoryStream();
                document.Save(stream, FormatType.Docx);
                stream.Position = 0;
                string fileName = Guid.NewGuid() + ".docx";
                //Download Word document in the browser
                return File(stream, "application/msword", fileName);

            }
            catch (Exception exp)
            {
                TempData["alertMessage"] = "Произошла ошибка при создании файла. Возможно не хватает информации об отправлении.";
                return RedirectToAction("Index");
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

                XDocument d = new XDocument();
                try
                {
                    d = XDocument.Load(postedFile.InputStream);
                }
                catch (FileNotFoundException)
                {
                    TempData["alertMessage"] = "Файл не может быть быть прочтен.";
                    return RedirectToAction("Index", "Home");
                }
                catch (XmlException ex)
                {
                    TempData["alertMessage"] = "Файл содержит ошибку в синтаксисе XML. " + ex.Message;
                    return RedirectToAction("Index", "Home");
                }
                if (!CheckValidation(d)) return RedirectToAction("Index", "Home");

                string message = ParserXML.ParseXML(d);
                TempData["alertMessage"] = message;
                return RedirectToAction("Index", "Home");
            }

            TempData["alertMessage"] = "Файл не был загружен.";
            return RedirectToAction("Index", "Home");
        }

        private bool CheckValidation(XDocument d)
        {
            string schema_path = Server.MapPath("~/App_Data/XMLSchema.xsd");
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            try
            {
                schemaSet.Add(null, schema_path);
            }
            catch (FileNotFoundException)
            {
                TempData["alertMessage"] = "Файл XML схемы не найден.";
                return false;
            }

            try
            {
                d.Validate(schemaSet, MyValidationEventHandler);
            }
            catch (XmlSchemaValidationException ex)
            {
                TempData["alertMessage"] = "Загруженный файл не соответствует шаблону (не валиден XML схеме). " + ex.Message;
                return false;
            }
            return true;

        }
        static private void MyValidationEventHandler(object o, ValidationEventArgs vea)
        {
            throw (new XmlSchemaValidationException(vea.Message));
        }
        private void ParseXML()
        {

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