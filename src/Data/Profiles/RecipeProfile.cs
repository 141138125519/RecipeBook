using AutoMapper;
using RecipeBook.Data.DTOs;
using RecipeBook.Models;

namespace RecipeBook.Data.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.Ingredients, act => act.MapFrom(src => src.Ingredients));

            CreateMap<RecipeDTO, Recipe>();
        }
    }
}
