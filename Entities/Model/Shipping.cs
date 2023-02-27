using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    [Table("Shipping_Table")]
    public class Shipping
    {
      [Key]
      public int  ShippingId { get; set; }


      [Required(ErrorMessage = "ShippingName Is Required")]
      public string ShippingName { get; set; }


      [Required(ErrorMessage = "CountryId Is Required")]
      public int CountryId { get; set; }


      [Required(ErrorMessage = "CountryName Is Required")]
      public string CountryName { get; set; }
       

      [Required(ErrorMessage = "PriceTypeId Is Required")]
      public int  PriceTypeId { get; set; }

      [Required(ErrorMessage = "PriceTypeName Is Required")]
      public string PriceTypeName { get; set; }

       
      [Required(ErrorMessage = "ShippingCost Is Required")]
      public double ShippingCost { get; set; }


      [Required(ErrorMessage = "ShippingTime Is Required")]
      public string  ShippingTime { get; set; }


      [Required(ErrorMessage = "PaymentTypeId Is Required")]
      public int  PaymentTypeId { get; set; }

      [Required(ErrorMessage = "PaymentTypeName Is Required")]
      public string PaymentTypeName { get; set; }


      [Required(ErrorMessage = "CitySupported Is Required")]
      public int CitySupportedId { get; set; }


      [Required(ErrorMessage = "CitySupportedName Is Required")]
      public string CitySupportedName { get; set; }

    }
}
