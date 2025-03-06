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

        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "The code must be exactly 6 digits.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "The code must be a 6-digit number.")]
        public string Code { get; set; }

        public DateTime ExpirationTime { get; set; }

        public bool IsExpired() => DateTime.UtcNow > ExpirationTime;
    }
}