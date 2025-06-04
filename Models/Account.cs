using System.ComponentModel.DataAnnotations;

namespace proyecto2.Models
{
    public enum AccountRole
    {
        Admin, User
    }

    public enum AccountStatus
    {
        Active, Suspended, Banned
    }

    public class Account
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public AccountRole AccountRole { get; set; } = AccountRole.User;
        [Required]
        public AccountStatus AccountStatus { get; set; } = AccountStatus.Active;
        public string? ProfilePicPath { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}