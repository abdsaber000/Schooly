using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain.Entities
{
    public class ResetCode
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Code { get; set; }

        public DateTime ExpirationTime { get; set; }

        public bool IsExpired() => DateTime.UtcNow > ExpirationTime;
    }
}