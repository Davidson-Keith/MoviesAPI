namespace MoviesAPI.DTOs;

/**
 * This is the data transferred to the user via a GET request to read a Genre row from the DB.
 * No validations are required for reading data
 */
public class GenreDto {
  public int Id { get; set; }
  public string Name { get; set; }

}