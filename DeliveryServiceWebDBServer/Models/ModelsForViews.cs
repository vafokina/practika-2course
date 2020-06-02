using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace DeliveryServiceWebDBServer.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        [MinLength(3, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }

    public class OldNewPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        [MinLength(3, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        [MinLength(3, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        public string NewPassword { get; set; }
    }


    public class SearchIdModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class PackageListViewModel : System.Collections.IEnumerable
    {
        public IEnumerable<Package> Packages { get; set; }
        public string CurrentCategory { get; set; }
        public DistributionCentre CurrentCentre { get; set; }
        public PackageListViewModel(IEnumerable<Package> packages, string category, DistributionCentre centre)
        {
            Packages = packages;
            CurrentCategory = category;
            CurrentCentre = centre;
        }
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Packages).GetEnumerator();
        }
    }

    public class IdValueModel
    {
        public int? Id { get; set; }
        public string Value { get; set; }
    }

    public class ToWithFromIdModel
    {
        public int FromId { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Index { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Address { get; set; }
        public Nullable<int> CentreId { get; set; }
        public bool InformingSMS { get; set; }
        public bool InformingMail { get; set; }
    }

    public class ConfirmModel
    {
        public int Number { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        [MinLength(3, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        public string Password { get; set; }
        public string SavedObject { get; set; }
        public string ConfirmedAction { get; set; }
    }
    public class CitiesRegionsCountriesModel
    {
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Region> Regions { get; set; }
        public IEnumerable<Country> Countries { get; set; }
    }
}