using System;
using System.ComponentModel.DataAnnotations;
using MoviesAPI.Validations;

namespace MoviesAPI.Entities {
  /**
   * This is the raw data type stored in the DB.
   * Validations are for the DB requirements???
   */
  public class Genre {
    public int Id { get; set; }

    // Validations moved to the Dto's
    // All these are ignored on a POST request??? I can set min/max length here to whatever, and it never fails.
    // Instead the validations for the GenreCreationDto are performed.
    // The DB itself only has a restriction that the column is nvarchar(50)
    // [Required(ErrorMessage = "The field with name {0} is required.")]
    // [StringLength(10)] // maxlength
    // [StringLength(maximumLength: 3, MinimumLength = 2)] // max + min length. Note the bizarre inconsistencies!
    // [FirstLetterUppercase] // Our custom MoviesAPI.Validations.FirstLetterUppercaseAttribute validation
    public string Name { get; set; }
  }
}
