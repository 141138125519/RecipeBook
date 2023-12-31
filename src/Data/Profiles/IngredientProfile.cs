using AutoMapper;
using RecipeBook.Data.DTOs;
using RecipeBook.Models;

namespace RecipeBook.Data.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDTO>()
                .ForMember(dest =>
                    dest.RecipeId,
                    opt => opt.MapFrom(src => src.Recipe.Id));

            CreateMap<IngredientDTO, Ingredient>();
        }
    }
}
