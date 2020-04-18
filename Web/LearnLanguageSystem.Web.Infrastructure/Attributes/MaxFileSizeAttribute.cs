namespace LearnLanguageSystem.Web.Infrastructure.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > this.maxFileSize)
                {
                    return new ValidationResult($"Maximum allowed file size is {this.maxFileSize} bytes.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
