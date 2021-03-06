//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeliveryServiceWebDBServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Region
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Region()
        {
            this.Cities = new HashSet<City>();
        }
    
        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        public string NameRegion { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        public int CountryId { get; set; }
    
        public virtual Country Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<City> Cities { get; set; }
    }
}
