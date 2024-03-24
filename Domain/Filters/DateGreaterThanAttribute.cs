using Domain.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        // Set the name of the property to compare
        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        // Validate the date comparison
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;

            var comparisonValue = (DateTime)validationContext
                .ObjectType.GetProperty(_comparisonProperty).GetValue(validationContext.ObjectInstance);

            if (currentValue < comparisonValue)
            {
                return new ValidationResult(ErrorMessage = Errors.DateValidation);
            }

            return ValidationResult.Success;
        }
    }
}
