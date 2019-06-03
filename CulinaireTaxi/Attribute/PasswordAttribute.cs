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
                return new ValidationResult("Je wachtwoord moet minimaal 8 tekens lang zijn!");
            }
            else if (password.Length > MAX_LENGTH)
            {
                return new ValidationResult("Je wachtwoord mag niet langer zijn dan 128 tekens!");
            }

            return ValidationResult.Success;
        }

    }

}
