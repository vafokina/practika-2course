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

    public partial class Record
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public Nullable<int> CentreId { get; set; }
        public System.DateTime DateAndTime { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения.")]
        public string Status { get; set; }
    
        public virtual Package Package { get; set; }
        public virtual DistributionCentre DistributionCentre { get; set; }
    }
}
