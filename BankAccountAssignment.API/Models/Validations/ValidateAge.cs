using System.ComponentModel.DataAnnotations;

namespace BankAccountAssignment.API.Models.Validations {
    public class ValidateAge : ValidationAttribute {
        protected override ValidationResult IsValid(object? value, ValidationContext context) {
            DateTime dateOfBirth = Convert.ToDateTime(value);
            var age = DateTime.Today.Year - dateOfBirth.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Account holder must be at least 18 years old");
        }
    }

}
