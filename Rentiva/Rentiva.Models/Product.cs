using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentiva.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display (Name="Product Name")]
        [MaxLength(50)]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

    }
}
