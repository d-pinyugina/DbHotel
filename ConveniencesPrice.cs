using System;
using System.ComponentModel.DataAnnotations;

namespace DbHotel
{
    public class ConveniencesPrice
    {
        [Key]
        public ConveniencesEnumeration Convenience { get; set; }
        public Decimal Price { get; set; }
    }
}
