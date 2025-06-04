using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace proyecto2.Models
{
    public class Reply
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
        [Required]
        public DateTime Created { get; set; } = DateTime.Now;
        public string? ReplyPicPath { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
    }
}