using AutoMapper;
using RecipeBook.Data.DTOs;
using RecipeBook.Models;

namespace RecipeBook.Data.Profiles
{
    public class StepProfile : Profile
    {
        public StepProfile()
        {
            CreateMap<Step, StepDTO>()
                .ForMember(dest =>
                    dest.RecipeId,
                    opt => opt.MapFrom(src => src.Recipe.Id));

            CreateMap<StepDTO, Step>();
        }
    }
}
