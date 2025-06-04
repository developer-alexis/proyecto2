using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace proyecto2.Models
{
    public enum PostType
    {
        Post, Announcement, Tutorial
    }
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public PostType PostType { get; set; } = PostType.Post;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? PostPicPath { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}