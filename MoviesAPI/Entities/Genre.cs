using System;
using System.ComponentModel.DataAnnotations;
using MoviesAPI.Validations;

namespace MoviesAPI.Entities {
  public class Genre : IValidatableObject {
    public int Id { get; set; }

    [Required(ErrorMessage = "The field with name {0} is required.")]
    // [StringLength(10)] // maxlength
    [StringLength(maximumLength: 10, MinimumLength = 2)] // max + min length. Note the bizarre inconsistencies!
    [FirstLetterUppercase] // Our custom validation.
    public string Name { get; set; }

    // [Range(18, 120)]
    // public int Age { get; set; } 
      
    // Note that using both model validation (what this method does), and attribute validation (e.g. [StringLength(10)]),
    // may result in not all errors being reported at once, since, if any attribute validation errors occur, then the
    // model validation won't run!!! 
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
      if (!string.IsNullOrEmpty(Name) && Name.Length >=2) {
        var secondLetter = Name[1].ToString();
        if (secondLetter != secondLetter.ToLower()) {
          // yield keyword indicates the method is an iterator block.
          // Note that the return value of this method is an IEnumerable<T>.
          // See https://www.c-sharpcorner.com/UploadFile/5ef30d/understanding-yield-return-in-C-Sharp/
          yield return new ValidationResult("Second letter should be lowercase.", new string[]{nameof(Name)});
        }
      }
    }
  }
}
