using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TodoListAPI.Entities
{
    public class User:IdentityUser<Guid>
    {
        [MaxLength(250)]
        [Required]
        public string Firstname { get; set; } = string.Empty;
        [MaxLength(250)]
        [Required]
        public string Lastname { get;set; } = string.Empty;
    }
}
