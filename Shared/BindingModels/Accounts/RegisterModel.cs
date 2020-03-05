namespace OnlineMarket.Shared.BindingModels.Accounts
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterModel
    {
        private const int UsernameMinLength = 6;
        private const int UsernameMaxLength = 50;

        [Required]
        [StringLength(UsernameMaxLength,
            ErrorMessage = CommonConstants.StringLengthErrorMessage,
            MinimumLength = UsernameMinLength)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password),
            ErrorMessage = CommonConstants.CompareErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}
