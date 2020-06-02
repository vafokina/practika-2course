using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryServiceWebDBServer.Models
{
    public class Constructor
    {
        public string[] SelectedTables { get; set; }
        public string[] Attributes { get; set; }
        public string[] SelectedAttributes { get; set; }
        public string[] References { get; set; }
        public string[] SelectedReferences { get; set; }

        public string[] GetAttributes()
        {
            List<string> list = new List<string>();
            foreach (string t in SelectedTables)
            {
                switch (t)
                {
                    case "Packages":
                        list.AddRange(AttributesPackages);
                        continue;
                    case "Persons":
                        list.AddRange(AttributesFromTo);
                        continue;
                    case "Centres":
                        list.AddRange(AttributesCentres);
                        continue;
                    case "Cities":
                        list.AddRange(AttributesCities);
                        continue;
                    case "Regions":
                        list.AddRange(AttributesRegions);
                        continue;
                    case "Countries":
                        list.AddRange(AttributesCountries);
                        continue;
                    case "Tariffs":
                        list.AddRange(AttributesTariffs);
                        continue;
                }
            }
            return list.ToArray();
        }
        public string[] GetReferences()
        {
            List<string> list = new List<string>();
            foreach (string t in SelectedAttributes)
            {
                if (Helper.Check(t, "^Номер"))
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
                "НомерСтраны",
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
"Эл.почта",
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
"НомерЦентраЗаписи",
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
    }
}