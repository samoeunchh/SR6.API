using System;
using System.ComponentModel.DataAnnotations;

namespace SR6.DataLayer
{
    public class Brand
    {
        [Key]
        public Guid BrandId { get; set; }
        [Required(ErrorMessage = "Brand name field is required")]
        [MaxLength(50)]
        public string BrandName { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
