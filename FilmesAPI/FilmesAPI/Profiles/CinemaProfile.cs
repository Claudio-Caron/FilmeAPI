using AutoMapper;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class CinemaProfile:Profile
{
    public CinemaProfile()
    {
        CreateMap<Cinema, ReadCinemaDto>();
        CreateMap<CreateCinemaDto, Cinema>();

        CreateMap<UpdateCinemaDto, Cinema>();
        
    }
}
