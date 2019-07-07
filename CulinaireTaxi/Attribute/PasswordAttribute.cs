using System.ComponentModel.DataAnnotations;

namespace CulinaireTaxi.Attributes
{

    public class PasswordAttribute : ValidationAttribute
    {

        private const int MIN_LENGTH = 8;
        private const int MAX_LENGTH = 128;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string password = value as string;

            if (password == null)
            {
                return new ValidationResult("This attribute can only be applied to strings!");
            }

            if (password.Length < MIN_LENGTH)
            {
                return new ValidationResult($"Your password must be at least {MIN_LENGTH} characters long!");
            }
            else if (password.Length > MAX_LENGTH)
            {
                return new ValidationResult($"Your password cannot be longer than {MAX_LENGTH} characters!");
            }

            return ValidationResult.Success;
        }

    }

}
