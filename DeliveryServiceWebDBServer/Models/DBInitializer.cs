using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DeliveryServiceWebDBServer.Models
{
    public class DBInitializer : CreateDatabaseIfNotExists<ModelDBContainer>
    {
        protected override void Seed(ModelDBContainer context)
        {
            context.Users.Add(new User { Login = "admin", Password = "54321", Admin = true });
            context.Users.Add(new User { Login = "user", Password = "12345", Admin = false });

            var tariffs = new List<Tariff>
            {
            new Tariff{ Name = "Срочный",  Formula = "P/1000*200" },
            new Tariff{ Name = "Обычный",  Formula = "P/1000*100" },
            };
            tariffs.ForEach(s => context.Tariffs.Add(s));
            context.SaveChanges();

            context.Countries.Add(new Country { NameCountry = "Россия" });
            context.SaveChanges();

            int id = context.Countries.First().Id;
            var regions = new List<Region>
            {
            new Region{ NameRegion = "Московская область", CountryId = id },
            new Region{ NameRegion = "Ленинградская область", CountryId = id },
            new Region{ NameRegion = "Пермский край", CountryId = id },
            };
            regions.ForEach(s => context.Regions.Add(s));
            context.SaveChanges();

            var cities = new List<City>
            {
            new City{ NameCity = "Москва", Region = regions[0] },
            new City{ NameCity = "Санкт-Петербург", Region = regions[1] },
            new City{ NameCity = "Пермь", Region = regions[2] },
            };
            cities.ForEach(s => context.Cities.Add(s));
            context.SaveChanges();

        }
        public static void Init(ModelDBContainer context)
        {
            context.Users.Add(new User { Login = "admin", Password = "54321", Admin = true });
            context.Users.Add(new User { Login = "user", Password = "12345", Admin = false });

            var tariffs = new List<Tariff>
            {
            new Tariff{ Name = "Срочный",  Formula = "P/1000*200" },
            new Tariff{ Name = "Обычный",  Formula = "P/1000*100" },
            };
            tariffs.ForEach(s => context.Tariffs.Add(s));
            context.SaveChanges();

            context.Countries.Add(new Country { NameCountry = "Россия" });
            context.SaveChanges();

            int id = context.Countries.First().Id;
            var regions = new List<Region>
            {
            new Region{ NameRegion = "Московская область", CountryId = id },
            new Region{ NameRegion = "Ленинградская область", CountryId = id },
            new Region{ NameRegion = "Пермский край", CountryId = id },
            };
            regions.ForEach(s => context.Regions.Add(s));
            context.SaveChanges();

            var cities = new List<City>
            {
            new City{ NameCity = "Москва", Region = regions[0] },
            new City{ NameCity = "Санкт-Петербург", Region = regions[1] },
            new City{ NameCity = "Пермь", Region = regions[2] },
            };
            cities.ForEach(s => context.Cities.Add(s));
            context.SaveChanges();

        }
    }
}