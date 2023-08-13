using System.ComponentModel.DataAnnotations;
using MoviesAPI.Entities;

namespace MoviesAPI.Validations; 

public class FirstLetterUppercaseAttribute: ValidationAttribute {
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
    // validationContext lets us access a particular object. However, we want this to be more generic,
    // not just for Genre.
    // A better option for validation specific to an entity, is for that entity to implement IValidatableObject
    // var genre = (Genre)validationContext.ObjectInstance; 
    
    if (value.Equals(null) || string.IsNullOrEmpty(value.ToString())) {
      return ValidationResult.Success;
    }
    var firstLetter = value.ToString()[0].ToString();
    if (firstLetter != firstLetter.ToUpper()) {
      return new ValidationResult("First letter should be uppercase.");
    }
    return ValidationResult.Success;
  }
}