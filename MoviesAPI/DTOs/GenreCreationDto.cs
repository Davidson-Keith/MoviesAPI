using System.ComponentModel.DataAnnotations;
using MoviesAPI.Validations;

namespace MoviesAPI.DTOs;

/**
 * This is the data transferred from the user via a POST request to create a new Genre row in the DB. 
 * Validations are for testing the user input data.
 * Failed validation returns an error to the front end form???
 */
public class GenreCreationDto {
  [Required(ErrorMessage = "The field with name {0} is required.")]
  // [StringLength(10)] // maxlength
  // [StringLength(maximumLength: 50, MinimumLength = 2)] // max + min length. Note the bizarre inconsistencies!
  [StringLength(maximumLength: 5, MinimumLength = 4)] // test BadRequestBehavior
  [FirstLetterUppercase] // Our custom MoviesAPI.Validations.FirstLetterUppercaseAttribute validation
  public string Name { get; set; }
}