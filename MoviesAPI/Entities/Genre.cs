﻿using System;
using System.ComponentModel.DataAnnotations;
using MoviesAPI.Validations;

namespace MoviesAPI.Entities {
  /**
   * This is the raw data type stored in the DB.
   * Validations are for the DB requirements???
   */
  public class Genre {
    public int Id { get; set; }

    [Required(ErrorMessage = "The field with name {0} is required.")]
    // [StringLength(10)] // maxlength
    [StringLength(maximumLength: 50, MinimumLength = 2)] // max + min length. Note the bizarre inconsistencies!
    [FirstLetterUppercase] // Our custom MoviesAPI.Validations.FirstLetterUppercaseAttribute validation
    public string Name { get; set; }
  }
}
