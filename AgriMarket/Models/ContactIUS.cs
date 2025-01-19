using System;
using System.ComponentModel.DataAnnotations;

namespace AgriMarket.Models
{
    public class ContactIUS
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters.")]
        public string Message { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;

        public string? Status { get; set; }
    }
}