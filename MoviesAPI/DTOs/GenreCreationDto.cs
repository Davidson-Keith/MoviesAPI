using System.ComponentModel.DataAnnotations;
using MoviesAPI.Validations;

namespace MoviesAPI.DTOs;

/**
 * This is the data transferred from the user via a POST request to create a new Genre row in the DB. 
 * Validations are for testing the user input data.
 * Failed validation returns an 400 Bad Request result to whatever called the POST request, with the validation errors in the
 * result body
 */
public class GenreCreationDto {
  [Required(ErrorMessage = "The field {0} is required.")]
  // [StringLength(10)] // maxlength
  // [StringLength(maximumLength: 50, MinimumLength = 2)] // max + min length. Note the bizarre inconsistencies!
  [StringLength(maximumLength: 5, MinimumLength = 4)] // test BadRequestBehavior
  [FirstLetterUppercase] // Our custom MoviesAPI.Validations.FirstLetterUppercaseAttribute validation
  public string Name { get; set; }
}