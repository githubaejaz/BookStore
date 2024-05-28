using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(3,ErrorMessage = "Category Name should be 5 characters atleast")]
        [MaxLength(10)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Required]
        [Range(1,1000,ErrorMessage = "Value Must be between 1 to 1000")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
