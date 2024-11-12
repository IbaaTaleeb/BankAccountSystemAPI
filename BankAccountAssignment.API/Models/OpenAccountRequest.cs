using BankAccountAssignment.API.Models.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccountAssignment.API.Models
{
    public class OpenAccountRequest
    {
        [Required]
        [StringLength(35, ErrorMessage = "Holder name cannot exceed 35 characters")]
        public string HolderName { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,13}$", ErrorMessage = "Phone number must be up to 13 digits")]
        public string AssociatedPhoneNumber { get; set; }

        [Required]
        [ValidateAge]
        public DateTime DateOfBirth { get; set; }
    }

    public class ModifyAccountDetailsRequest
    {
        [Required]
        [StringLength(35, ErrorMessage = "Holder name cannot exceed 35 characters")]
        public string HolderName { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,13}$", ErrorMessage = "Phone number must be up to 13 digits")]
        public string AssociatedPhoneNumber { get; set; }
    }

    public class TransactionRequest
    {
        [Required]
        [Range(5, 100, ErrorMessage = "Amount must be between 5KD and 100KD for deposits")]
        public decimal Amount { get; set; }
    }

    public class WithdrawRequest
    {
        [Required]
        [Range(5, 20, ErrorMessage = "Amount must be between 5KD and 20KD for withdrawals")]
        public decimal Amount { get; set; }
    }
}
