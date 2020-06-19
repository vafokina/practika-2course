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
       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        public string Login { get; set; }

       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        [MinLength(3, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        public string Login { get; set; }

       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        public bool IsAdmin { get; set; }
    }

    public class OldNewPasswordModel
    {
       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        [MinLength(3, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        public string OldPassword { get; set; }

       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        [MinLength(3, ErrorMessage = "Допустимая длина пароля от 3 до 15 символов")]
        public string NewPassword { get; set; }
    }


    public class SearchIdModel
    {
       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
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
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        public string Index { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Address { get; set; }
        public Nullable<int> CentreId { get; set; }
       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        public bool InformingSMS { get; set; }
       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        public bool InformingMail { get; set; }
    }

    public class PersonWithPackageIdModel
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        public string Index { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Address { get; set; }
        public Nullable<int> CentreId { get; set; }
       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        public bool InformingSMS { get; set; }
       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
        public bool InformingMail { get; set; }
    }

    public class ConfirmModel
    {
        public int Number { get; set; }
       [Required(ErrorMessage ="Это поле обязательно для заполнения.")]
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