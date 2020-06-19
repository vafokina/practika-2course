using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Web.Mvc;
using DeliveryServiceWebDBServer.Controllers;
using System.Globalization;

namespace DeliveryServiceWebDBServer.Models
{
    static public class ParserXML
    {
        const bool IsNecessaryNameForPublicFace = false;
        const bool IsNecessaryNumber = false;
        const bool IsNecessaryIndex = false;
        const bool IsNecessaryDeclaredValue = false;
        static private string RegisterStatus = Properties.Settings.Default.Statuses.Cast<string>().ToList()[0];

        private static Dictionary<int, string> citiesFullNames = new Dictionary<int, string>();
        private static Dictionary<int, string> cities = new Dictionary<int, string>();
        private static Dictionary<int, string> centres = new Dictionary<int, string>();
        private static Dictionary<int, string> tariffs = new Dictionary<int, string>();
        private static List<string> descriptions = new List<string>();

        static private string surnameFrom, nameFrom, midnameFrom, companyFrom, phoneFrom,
                mailFrom, indexFrom, addressFrom, cityFrom,
            surnameTo, nameTo, midnameTo, companyTo, phoneTo,
                mailTo, indexTo, addressTo, cityTo,
            description, tariff;

        static private bool informingMailFrom, informingSMSFrom,
            informingMailTo, informingSMSTo;

        static private double? valueDouble, costDouble;
        static private int? numberInt,
            idCentreFrom, idCentreTo, idCityFrom, idCityTo, idTariff;
        static private double? weightDouble, widthtDouble, heightDouble, lengthDouble;

        static private int countMistakes, countSuccess;
        static private string Mistakes;
        static private int curCount;


        public static string ParseXML(XDocument d)
        {
            LoadSource();
            countMistakes = 0;
            countSuccess = 0;
            curCount = 1;
            Mistakes = "";

            foreach (XElement add in d.Root.Elements())
            {
                var from = add.Element("from");
                var to = add.Element("to");

                surnameFrom = from.Attribute("surname")?.Value;
                nameFrom = from.Attribute("name")?.Value;
                midnameFrom = from.Attribute("midname")?.Value;
                companyFrom = from.Attribute("company")?.Value;
                phoneFrom = from.Attribute("phone")?.Value;
                mailFrom = from.Attribute("mail")?.Value;
                indexFrom = from.Attribute("index")?.Value;
                addressFrom = from.Attribute("address")?.Value;
                cityFrom = from.Attribute("city")?.Value;
                informingSMSFrom = Convert.ToBoolean(from.Attribute("informingSMS") == null ? "false" : from.Attribute("informingSMS").Value);
                informingMailFrom = Convert.ToBoolean(from.Attribute("informingMail") == null ? "false" : from.Attribute("informingMail").Value);

                surnameTo = to.Attribute("surname")?.Value;
                nameTo = to.Attribute("name")?.Value;
                midnameTo = to.Attribute("midname")?.Value;
                companyTo = to.Attribute("company")?.Value;
                phoneTo = to.Attribute("phone")?.Value;
                mailTo = to.Attribute("mail")?.Value;
                indexTo = to.Attribute("index")?.Value;
                addressTo = to.Attribute("address")?.Value;
                cityTo = to.Attribute("city")?.Value;
                informingSMSTo = Convert.ToBoolean(to.Attribute("informingSMS") == null ? "false" : to.Attribute("informingSMS").Value);
                informingMailTo = Convert.ToBoolean(to.Attribute("informingMail") == null ? "false" : to.Attribute("informingMail").Value);

                var temp = from.Attribute("idCentre");
                idCentreFrom = temp == null ? null : (int?)Convert.ToInt32(temp.Value);
                temp = to.Attribute("idCentre");
                idCentreTo = temp == null ? null : (int?)Convert.ToInt32(temp.Value);

                weightDouble = add.Attribute("weight") == null ? (double?)null : Convert.ToDouble(add.Attribute("weight").Value, CultureInfo.InvariantCulture);
                lengthDouble = add.Attribute("length") == null ? (double?)null : Convert.ToDouble(add.Attribute("length").Value, CultureInfo.InvariantCulture);
                widthtDouble = add.Attribute("width") == null ? (double?)null : Convert.ToDouble(add.Attribute("width").Value, CultureInfo.InvariantCulture);
                heightDouble = add.Attribute("height") == null ? (double?)null : Convert.ToDouble(add.Attribute("height").Value, CultureInfo.InvariantCulture);
                temp = add.Attribute("number");
                numberInt = temp == null ? null : (int?)Convert.ToInt32(temp.Value);
                temp = add.Attribute("value");
                valueDouble = temp == null ? null : (int?)Convert.ToDouble(temp.Value, CultureInfo.InvariantCulture);
                description = add.Attribute("description")?.Value;
                tariff = add.Attribute("tariff")?.Value;

                if (!CheckFromTo(ref surnameFrom, ref nameFrom, ref midnameFrom, ref companyFrom, ref phoneFrom, ref mailFrom, ref indexFrom,
                    ref addressFrom, ref idCentreFrom, out idCityFrom, cityFrom, informingSMSFrom, informingMailFrom))
                {
                    countMistakes++;
                    Mistakes += curCount + ", ";
                    curCount++;
                    continue;
                }
                if (!CheckFromTo(ref surnameTo, ref nameTo, ref midnameTo, ref companyTo, ref phoneTo, ref mailTo, ref indexTo,
                    ref addressTo, ref idCentreTo, out idCityTo, cityTo, informingSMSTo, informingMailTo))
                {
                    countMistakes++;
                    Mistakes += curCount + ", ";
                    curCount++;
                    continue;
                }
                if (!CheckPackage())
                {
                    countMistakes++;
                    Mistakes += curCount + ", ";
                    curCount++;
                    continue;
                }

                using (ModelDBContainer db = new ModelDBContainer())
                {
                    Person From = new Person
                    {
                        Name = nameFrom,
                        MiddleName = midnameFrom,
                        Surname = surnameFrom,
                        Company = companyFrom,
                        Phone = phoneFrom,
                        Mail = mailFrom,
                        Index = indexFrom,
                        CityId = idCityFrom,
                        Address = addressFrom,
                        CentreId = idCentreFrom,
                        InformingSMS = informingSMSFrom,
                        InformingMail = informingMailFrom
                    };
                    Person To = new Person
                    {
                        Name = nameTo,
                        MiddleName = midnameTo,
                        Surname = surnameTo,
                        Company = companyTo,
                        Phone = phoneTo,
                        Mail = mailTo,
                        Index = indexTo,
                        CityId = idCityTo,
                        Address = addressTo,
                        CentreId = idCentreTo,
                        InformingSMS = informingSMSTo,
                        InformingMail = informingMailTo
                    };
                    db.Persons.Add(From); db.Persons.Add(To);
                    db.SaveChanges();
                    Package package = new Package
                    {
                        PersonIdFrom = From.Id,
                        PersonIdTo = To.Id,
                        Weight = weightDouble,
                        Width = widthtDouble,
                        Length = lengthDouble,
                        Height = heightDouble,
                        DeclaredValue = valueDouble,
                        Cost = costDouble,
                        TariffId = idTariff,
                        Description = description,
                        NumberOfPackages = numberInt == null ? 1 : (int)numberInt
                    };
                    db.Packages.Add(package);
                    db.SaveChanges();
                    db.Records.Add(new Record { PackageId = package.Id, DateAndTime = DateTime.Now, Status = RegisterStatus });
                    db.SaveChanges();
                }
                countSuccess++;
                curCount++;
            }
            string message = "Всего записей об отправлениях: " + (curCount-1) +
                ". Успешно загружено: " + countSuccess + ". Не  загружено по причине ошибки: " + countMistakes + ".";
            if (Mistakes.Length > 0) message += " Порядковые номера записей с ошибками: " + Mistakes.Substring(0, Mistakes.Length - 2) + ".";
            return message;
        }
        private static void LoadSource()
        {
            using (ModelDBContainer db = new ModelDBContainer())
            {
                cities = new Dictionary<int, string>();
                foreach (City c in db.Cities)
                {
                    cities.Add(c.Id, c.NameCity);
                    citiesFullNames.Add(c.Id, Helper.GetCityString(c));
                }
                centres = new Dictionary<int, string>();
                foreach (DistributionCentre c in db.DistributionCentres)
                {
                    centres.Add(c.Id, Helper.GetCentreString(c));
                }
                tariffs = new Dictionary<int, string>();
                foreach (Tariff c in db.Tariffs)
                {
                    tariffs.Add(c.Id, c.Name);
                }
                descriptions = Properties.Settings.Default.Descriptions.Cast<string>().ToList();
            }
        }
        public static bool CheckFromTo(ref string surname, ref string name, ref string midname, ref string company, ref string phone,
                ref string mail, ref string index, ref string address, ref int? idCentre, out int? idCity, string city,
                bool InformingSMS, bool informingMail)
        {
            idCity = null;

            if (surname != null)
            {
                Helper.DelExtraSpace(ref surname);
                if (surname == "") surname = null;
            }
            if (midname != null)
            {
                Helper.DelExtraSpace(ref midname);
                if (midname == "") midname = null;
            }
            if (name != null)
            {
                Helper.DelExtraSpace(ref name);
            if (name == "") name = null;
            }
            if (company != null)
            {
                Helper.DelExtraSpace(ref company);
            if (company == "") company = null;
            }
            if (phone != null)
            {
                Helper.DelExtraSpace(ref phone);
            if (phone == "") phone = null;
            }
            if (mail != null)
            {
                Helper.DelExtraSpace(ref mail);
            if (mail == "") mail = null;
            }
            if (index != null)
            {
                Helper.DelExtraSpace(ref index);
            if (index == "") index = null;
            }
            if (address != null)
            {
                Helper.DelExtraSpace(ref address);
            if (address == "") address = null;
            }
            if (city != null)
            {
                Helper.DelExtraSpace(ref city);
            if (city == "") city = null;
            }

            bool isPrivate, isCorier;
            if (company == null) isPrivate = true;
            else isPrivate = false;
            if (idCentre == null)
            {
                if (address == null) { return false; }
                isCorier = true;
            }
            else isCorier = false;

            if (isPrivate || IsNecessaryNameForPublicFace)
            {
                if (surname == null)
                { return false; }
                else if (!Helper.Check(surname, "^[a-zа-я ]+$"))
                { return false; }

                if (name == null)
                { return false; }
                else if (!Helper.Check(name, "^[a-zа-я ]+$"))
                { return false; }

                if (midname == null)
                { return false; }
                else if (!Helper.Check(midname, "^[a-zа-я ]+$"))
                { return false; }
            }

            if (!isPrivate)
            {
                if (company == null)
                { return false; }
            }

            if (IsNecessaryNumber && phone == null)
            { return false; }
            if (!IsNecessaryNumber && InformingSMS && phone == null)
            { return false; }
            if (phone != null && phone.Length > 0 && !Helper.Check(phone, "^[0-9+]+$"))
            { return false; }

            if (informingMail && mail == null) // MailNotice
            { return false; }
            if (mail != null && mail.Length > 0 && !Helper.Check(mail, @"^[0-9.a-zа-я-_]+@[0-9.a-zа-я-_]+.[0-9.a-zа-я-_]+$")) // MailNotice
            { return false; }

            if (isCorier)
            {
                if (city == null) { return false; }
                var c = cities.Where(a => a.Value == city);
                if (c.Count() != 1)
                {
                    c = citiesFullNames.Where(a => a.Value == city);
                    if (c.Count() != 1)
                    { return false; }
                    else
                    {
                        idCity = c.Single().Key;
                    }
                }
                else
                {
                        idCity = c.Single().Key;
                }
                if (IsNecessaryIndex && index == null)
                { return false; }
                if (index!=null && index.Length > 0)
                {
                    if (!Helper.Check(index, @"^[a-zа-я0-9-_/]+$"))
                    { return false; }
                }

                if (address == null)
                { return false; }
            }

            if (!isCorier)
            {
                if (idCentre == null) { return false; }
                else
                {
                    int id = (int)idCentre;
                    if (centres.Where(a => a.Key == id).Count() == 0)
                    { return false; }
                }
            }
            return true;
        }
        public static bool CheckPackage()
        {
            if (weightDouble != null && weightDouble <= 0) { return false; }
            if (heightDouble != null && heightDouble <= 0) { return false; }
            if (lengthDouble != null && lengthDouble <= 0) { return false; }
            if (widthtDouble != null && widthtDouble <= 0) { return false; }

            if (numberInt != null && numberInt <= 0) { return false; }
            if (numberInt != null && numberInt == 0)
            {
                numberInt = null;
            }

            if (valueDouble != null && valueDouble <= 0) { return false; }

            if (tariffs.Count == 0) tariff = null;
            if (tariff != null && tariff.Length == 0) tariff = null;
            if (tariff != null)
            {
                List<int> t = tariffs.Where(a => a.Value == tariff).Select(a => a.Key).ToList();
                if (t == null || t.Count == 0) { return false; }
                idTariff = t[0];
                if (idTariff != null) costDouble = Helper.EvaluateCost((int)idTariff, (double)weightDouble, (double)heightDouble, (double)lengthDouble, (double)widthtDouble);
            }
            else
            {
                idTariff = null;
                costDouble = null;
            }

if (descriptions.Count == 0) description = null;
            
            if (description != null)
            {
                if (description.Length == 0) description = null;
                else
                if (descriptions.Where(a => a == description).Count() != 1)
                { return false; }
            }
            return true;
        }
    }
}