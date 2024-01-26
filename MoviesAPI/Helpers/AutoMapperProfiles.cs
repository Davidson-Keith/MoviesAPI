using AutoMapper;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;

namespace MoviesAPI.Helpers;

public class AutoMapperProfiles : Profile {
  public AutoMapperProfiles() {
    // Creates a map from GenreDTO to Genre, and a reverse map from Genre to GenreDTO.
    CreateMap<GenreDto, Genre>().ReverseMap();
    CreateMap<GenreCreationDto, Genre>().ReverseMap();
  }
}