using System.ComponentModel.DataAnnotations;
using MoviesAPI.Validations;

namespace MoviesAPI.DTOs;

public class GenreCreationDto {
  [Required(ErrorMessage = "The field with name {0} is required.")]
  // [StringLength(10)] // maxlength
  // [StringLength(maximumLength: 50, MinimumLength = 2)] // max + min length. Note the bizarre inconsistencies!
  [StringLength(maximumLength: 5, MinimumLength = 4)] // test BadRequestBehavior
  [FirstLetterUppercase] // Our custom MoviesAPI.Validations.FirstLetterUppercaseAttribute validation
  public string Name { get; set; }
}