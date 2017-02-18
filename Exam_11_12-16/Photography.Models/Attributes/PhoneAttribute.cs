namespace Photography.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class PhoneValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string phone = value as string;

            if (phone == null)
            {
                return new ValidationResult("Phone must be string type!");
            }

            string pattern = @"\+\d{1,3}\/\d{8,10}\b";

            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(phone))
            {
                return new ValidationResult("Phone doesn't match the regex pattern!");
            }

            return ValidationResult.Success;
        }
    }
}