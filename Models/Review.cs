using System.ComponentModel.DataAnnotations;

namespace proyecto2.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Comment { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}