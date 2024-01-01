using RecipeBook.Data.DTOs;

namespace RecipeBook.Repositories.StepRepository
{
    public interface IStepRepository
    {
        public List<StepDTO> GetAll();
        public StepDTO? GetIfExists(int id);
        public void Add(StepDTO step);
        public void Update(StepDTO step);
        public void Delete(int id);
    }
}
