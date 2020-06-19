using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Globalization;

namespace DeliveryServiceWebDBServer.Models
{
    public static class Helper
    {
        public static bool CheckAndDelExtraSpace(ref string s, string pattern)
        {
            bool ok = false;
            if (Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase)) ok = true;
            DelExtraSpace(ref s);
            return ok;
        }
        public static bool Check(string s, string pattern)
        {
            bool ok = false;
            if (Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase)) ok = true;
            return ok;
        }
        public static void DelExtraSpace(ref string s)
        {
            string temp = "";
            char? c = null;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ' && c == null) { continue; }
                if (s[i] == ' ' && c == ' ') { continue; }
                if (c == ' ') temp += c;
                c = s[i];
                if (c != ' ') temp += s[i];
            }
            s = temp;
        }
        public static void DelDoubleSpace(ref string s)
        {
            while (s.Contains("  ")) s = s.Replace("  ", " ");
        }
        public static void DelWrongSymbols(ref string s, string pattern)
        {
            string temp = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (Regex.IsMatch(s[i].ToString(), pattern, RegexOptions.IgnoreCase)) temp += s[i];
            }
            DelDoubleSpace(ref temp);
            s = temp;
        }
        public static int GetPosOfSplit(string s, int n = 1)
        {
            int pos = 0;
            int mn = 0; // сколько раз встретился разделитель
            // n - необходимое количество раз
            for (pos = 0; pos < s.Length; pos++)
            {
                if (s[pos] == '/') mn++;
                if (mn == n) break;
            }
            return pos;
        }
        public static string GetPersonString(Person p)
        {
            string str1 = "";

            if (p.Name != null) { str1 += p.Name + " "; }
            if (p.MiddleName != null) { str1 += p.MiddleName + " "; }
            if (p.Surname != null) { str1 += p.Surname + ", "; }
            if (p.Company != null) { str1 += p.Company + ", "; }
            if (p.CityId != null)
            {
                if (p.City.Region.Country.NameCountry != null) { str1 += p.City.Region.Country.NameCountry + ", "; }
                if (p.City.Region.NameRegion != null) { str1 += p.City.Region.NameRegion + ", "; }
                if (p.City.NameCity != null) { str1 += p.City.NameCity + ", "; }
                if (p.Address != null) { str1 += p.Address + " "; }
            }
            if (p.CentreId != null)
            {
                str1 += "Пункт выдачи: ";
                if (p.DistributionCentre.City.Region.Country != null) { str1 += p.DistributionCentre.City.Region.Country.NameCountry + ", "; }
                if (p.DistributionCentre.City.Region != null) { str1 += p.DistributionCentre.City.Region.NameRegion + ", "; }
                if (p.DistributionCentre.City != null) { str1 += p.DistributionCentre.City.NameCity + ", "; }
                if (p.DistributionCentre != null) { str1 += p.DistributionCentre.Address + " "; }
            }
            if (p.InformingMail) { str1 += "Оповещения по почте "; }
            if (p.InformingSMS) { str1 += "Оповещения по SMS"; }
            return str1;
        }
        public static string GetCentreString(DistributionCentre p)
        {
            string str2 = "";
            str2 = p.Address + ", " + p.City.NameCity + ", " + p.City.Region.NameRegion + ", " + p.City.Region.Country.NameCountry;
            return str2;
        }
        public static string GetCityString(City p)
        {
            string str2 = "";
            str2 =  p.NameCity + ", " + p.Region.NameRegion + ", " + p.Region.Country.NameCountry;
            return str2;
        }

        public static double? EvaluateCost(int id, double weight, double height, double length, double width, int number = 1)
        {
            double? cost = -1;
            using (ModelDBContainer db = new ModelDBContainer())
            {
                try
                {
                    string formula = db.Tariffs.Find(id).Formula;
                    NumberFormatInfo format = new NumberFormatInfo();
                    format.NumberDecimalSeparator = ".";
                    formula = formula.Replace("weight", weight.ToString(format));
                    formula = formula.Replace("height", height.ToString(format));
                    formula = formula.Replace("length", length.ToString(format));
                    formula = formula.Replace("width", width.ToString(format));
                    formula = formula.Replace("number", number.ToString());
                    var options = ScriptOptions.Default.WithImports("System.Math");
                    cost = CSharpScript.EvaluateAsync<double>(formula, options).Result;
                }
                catch(Exception)
                {
                    cost = null;
                }
            }
            return cost;
        }
    }
}

