using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccountAssignment.API.Data
{
    public class BankAccount
    {
        [Key]
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be a 10-digit unique number")]
        public string Number { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "Holder name cannot exceed 35 characters")]
        public string HolderName { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,13}$", ErrorMessage = "Phone number must be up to 13 digits")]
        public string AssociatedPhoneNumber { get; set; }

        public bool IsActive { get; set; } = true;

        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative")]
        public decimal Balance { get; set; }

        [Required]
        public DateTime CreationDate { get; private set; }

        [Required]
        [CustomValidation(typeof(BankAccount), nameof(ValidateAge))]
        public DateTime DateOfBirth { get; set; }

        // Constructor
        public BankAccount()
        {
            CreationDate = DateTime.Now;
        }

        public static ValidationResult ValidateAge(DateTime dateOfBirth, ValidationContext context)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 18 ? ValidationResult.Success : new ValidationResult("Account holder must be at least 18 years old");
        }
    }
}
