using System;
using System.ComponentModel.DataAnnotations;
using MoviesAPI.Validations;

namespace MoviesAPI.Entities {
  /**
   * This is the raw data type stored in the DB.
   * Validations are for the DB requirements???
   */
  // public class Genre : IValidatableObject {
  public class Genre {
    public int Id { get; set; }

    // Validations exist in the Dto's
    // All the validatons here are ignored on a POST request!
    // I can make these conditions fail all I like, but they never fail.
    // The validations for the GenreCreationDto are performed if I have them, but not these.
    // This is weird, as they work in his vids, but maybe as the course progressed, there was a change made that
    // disabled validations here???  Oh, maybe it is the actual addition of the Dto's that did it. 
    // The DB itself only has a restriction that the column is nvarchar(50)

    // [Required(ErrorMessage = "The field with name {0} is required.")]
    // [StringLength(10)] // maxlength
    // [StringLength(maximumLength: 3, MinimumLength = 2)] // max + min length. Note the bizarre inconsistencies!
    // [FirstLetterUppercase] // Our custom MoviesAPI.Validations.FirstLetterUppercaseAttribute validation
    public string Name { get; set; }

    // This doesn't work either. 
    // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
    //   if (!string.IsNullOrEmpty(Name)) {
    //     var firstLetter = Name[0].ToString();
    //     if (firstLetter != firstLetter.ToUpper()) {
    //       yield return new ValidationResult("First letter should be uppercase", new string[] { nameof(Name) });
    //     }
    //   }
    // }
  }
}