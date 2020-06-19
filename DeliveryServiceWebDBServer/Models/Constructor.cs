using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeliveryServiceWebDBServer.Models
{


    public class Constructor
    {
        public string NameCondition { get; set; }
        public int? IntCondition { get; set; }
        public double? DoubleCondition { get; set; }
        public string StringCondition { get; set; }
        public string DataTimeCondition { get; set; }
        public string Sign { get; set; }
        public string DataType { get; set; }

        public string[] SelectedTables { get; set; }
        public string[] Attributes { get; set; }
        public string[] SelectedAttributes { get; set; }
        public string[] References { get; set; }
        public string[] SelectedReferences { get; set; }

        public List<string[]> Conditions { get; set; }

        public string[] GetAttributes()
        {
            List<string> list = new List<string>();
            foreach (string t in SelectedTables)
            {
                switch (t)
                {
                    case "Отправления":
                        list.AddRange(AttributesPackages);
                        continue;
                    case "ПолучателиОтправители":
                        list.AddRange(AttributesFromTo);
                        continue;
                    case "ПунктыВыдачи":
                        list.AddRange(AttributesCentres);
                        continue;
                    case "Города":
                        list.AddRange(AttributesCities);
                        continue;
                    case "Регионы":
                        list.AddRange(AttributesRegions);
                        continue;
                    case "Страны":
                        list.AddRange(AttributesCountries);
                        continue;
                    case "Тарифы":
                        list.AddRange(AttributesTariffs);
                        continue;
                }
            }
            return list.ToArray();
        }
        public string[] GetReferences()
        {
            List<string> list = new List<string>();
            foreach (string t in Attributes)
            {
                if (ReferencesRange.Contains(t))
                    list.Add(t);
            }
            return list.ToArray();
        }

        public string[] AttributesCities
        {
            get
            {
                string[] st = {
                "НомерГорода",
"ИмяГорода",
"НомерРегионаГорода"
            };
                return st;
            }
        }
        public string[] AttributesCountries
        {
            get
            {
                string[] st = {

"ИмяСтраны"
            };
                return st;
            }
        }
        public string[] AttributesCentres
        {
            get
            {
                string[] st = {
                "НомерПВ",
"ИндексПВ",
"НомерГородаПВ",
"АдресПВ",
"ОписаниеПВ"
                };
                return st;
            }
        }
        public string[] AttributesFromTo
        {
            get
            {
                string[] st = {
                "НомерКлиента",
"ИмяЧеловека",
"ОтчествоЧеловека",
"ФамилияЧеловека",
"ИмяКомпании",
"Телефон",
"ЭлектроннаяПочта",
"ИндексКлиента",
"НомерГородаКлиента",
"АдресКлиента",
"НомерПВКлиента"
            };
                return st;
            }
        }
        public string[] AttributesPackages
        {
            get
            {
                string[] st = {
                "НомерОтправления",
"НомерОтправителя",
"НомерПолучателя",
"НомерТарифаОтправления",
"ОписаниеОтправления",
"Вес",
"Длина",
"Ширина",
"Высота",
"КоличествоМест",
"Стоимость",
"ОбъявленнаяСтоимость"
            };
                return st;
            }
        }
        public string[] AttributesRecords
        {
            get
            {
                string[] st = {
                "НомерЗаписи",
"НомерОтправленияЗаписи",
"Статус",
"НомерПВЗаписи",
"ДатаВремяЗаписи"
            };
                return st;
            }
        }
        public string[] AttributesRegions
        {
            get
            {
                string[] st = {
                "НомерРегиона",
"ИмяРегиона",
"НомерСтраныРегиона"
            };
                return st;
            }
        }
        public string[] AttributesTariffs
        {
            get
            {
                string[] st = {
                "НомерТарифа",
"ИмяТарифа",
"ОписаниеТарифа",
"ФормулаТарифа"
            };
                return st;
            }
        }
        public string[] ReferencesRange
        {
            get
            {
                string[] st = {
                "НомерРегионаГорода",
"НомерГородаПВ",
"НомерГородаКлиента",
"НомерПВКлиента",
"НомерОтправителя",
"НомерПолучателя",
"НомерТарифаОтправления",
"НомерОтправленияЗаписи",
"НомерПВЗаписи",
"НомерСтраныРегиона"
            };
                return st;
            }
        }
        public string TranslateAttributes(string str)
        {
            //if (str == "НомерСсылки") return "ar.[Id]";
            //if (str == "НомерЛогинаСсылки") return "ar.[IdLogin]";
            //if (str == "НомерКлиентаСсылки") return "ar.[IdFromTo]";
            //if (str == "НомерЦентраСсылки") return "ar.[IdCentre]";

            if (str == "НомерГорода") return "ct.[Id]";
            if (str == "ИмяГорода") return "ct.[NameCity]";
            if (str == "НомерРегионаГорода") return "ct.[RegionId]";

            if (str == "НомерСтраны") return "c.[Id]";
            if (str == "ИмяСтраны") return "c.[NameCountry]";

            if (str == "НомерПВ") return "dc.[Id]";
            if (str == "ИндексПВ") return "dc.[Index]";
            if (str == "НомерГородаПВ") return "dc.[CityId]";
            if (str == "АдресПВ") return "dc.[Address]";
            if (str == "ОписаниеПВ") return "dc.[Description]";

            if (str == "НомерКлиента") return "ft.[Id]";
            if (str == "ИмяЧеловека") return "ft.[Name]";
            if (str == "ОтчествоЧеловека") return "ft.[MiddleName]";
            if (str == "ФамилияЧеловека") return "ft.[SurName]";
            if (str == "ИмяКомпании") return "ft.[Company]";
            if (str == "Телефон") return "ft.[Phone]";
            if (str == "Эл.почта") return "ft.[Mail]";
            if (str == "ИндексКлиента") return "ft.[Index]";
            if (str == "НомерГородаКлиента") return "ft.[CityId]";
            if (str == "АдресКлиента") return "ft.[Address]";
            if (str == "НомерПВКлиента") return "ft.[CentreId]";

            //if (str == "НомерЛогина") return "l.[IdLogin]";
            //if (str == "Логин") return "l.[Login]";
            //if (str == "Пароль") return "l.[Pass]";
            //if (str == "ПраваАдминистратора") return "l.[Admin]";

            if (str == "НомерОтправления") return "p.[Id]";
            if (str == "НомерОтправителя") return "p.[PersonIdFrom]";
            if (str == "НомерПолучателя") return "p.[PersonIdTo]";
            if (str == "НомерТарифаОтправления") return "p.[TariffId]";
            if (str == "ОписаниеОтправления") return "p.[Description]";
            if (str == "Вес") return "p.[Weight]";
            if (str == "Длина") return "p.[Lenght]";
            if (str == "Ширина") return "p.[Width]";
            if (str == "Высота") return "p.[Height]";
            if (str == "КоличествоМест") return "p.[NumberOfPackages]";
            if (str == "Стоимость") return "p.[Cost]";
            if (str == "ОбъявленнаяСтоимость") return "p.[DeclaredValue]";
           // if (str == "СМСОтправителю") return "p.[InformingSMSTo]"; if (str == "СМСПолучателю") return "p.[InformingSMSFrom]";
            //if (str == "ПисьмоОтправителю") return "p.[InformingEmailTo]"; if (str == "ПисьмоПолучателю") return "p.[InformingEmailFrom]";

            if (str == "НомерЗаписи") return "rec.[Id]";
            if (str == "НомерОтправленияЗаписи") return "rec.[PackageId]";
            if (str == "Статус") return "rec.[Status]";
            if (str == "НомерЦентраЗаписи") return "rec.[CentreId]";
            if (str == "ДатаВремяЗаписи") return "rec.[DateAndTime]";

            if (str == "НомерРегиона") return "r.[Id]";
            if (str == "ИмяРегиона") return "r.[NameRegion]";
            if (str == "НомерСтраныРегиона") return "r.[CountryId]";

            if (str == "НомерТарифа") return "t.[Id]";
            if (str == "ИмяТарифа") return "t.[Name]";
            if (str == "ОписаниеТарифа") return "t.[Comment]";
            if (str == "ЦенаТарифа") return "t.[Cost]";

            return str;
        }
        public string TranslateTables(string str)
        {
            if (str == "ОтправителиПолучатели") return "[Persons] ft";
            if (str == "Отправления") return "[Packages] p";
            if (str == "Страны") return "[Countries] c";
            if (str == "Регионы") return "[Regions] r";
            if (str == "Города") return "[Cities] ct";
            if (str == "Тарифы") return "[Tariffs] t";
            if (str == "ПунктыВыдачи") return "[DistributionCentres] dc";
            if (str == "ЗаписиОбОтслеживании") return "[Records] rec";
            return str;
        }
        public string TranslateReferences(string str)
        {
            if (str == "НомерРегионаГорода") return "r.[Id] = ct.[RegionId]";
            if (str == "НомерГородаПВ") return "ct.[Id] = dc.[CityId]";
            if (str == "НомерГородаКлиента") return "ct.[Id] = ft.[CityId]";
            if (str == "НомерПВКлиента") return "dc.[Id] = ft.[CentreId]";
            if (str == "НомерОтправителя") return "p.[PersonIdFrom] = ft.[Id]";
            if (str == "НомерПолучателя") return "p.[PersonIdTo] = ft.[Id]";
            if (str == "НомерТарифаОтправления") return "t.[Id] = p.[TariffId]";
            //if (str == "НомерОтправленияЗаписи") return "r.[IdRegion] = ct.[IdRegion]";
            //if (str == "НомерПВЗаписи") return "r.[IdRegion] = ct.[IdRegion]";
            if (str == "НомерСтраныРегиона") return "r.[CountryId] = c.[Id]";
            return str;
        }
    }
}
