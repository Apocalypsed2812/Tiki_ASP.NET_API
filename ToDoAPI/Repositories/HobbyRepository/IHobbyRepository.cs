using ToDoAPI.Models;

namespace ToDoAPI.Repositories.HobbyRepository
{
    public interface IHobbyRepository
    {
        public Task<List<HobbyModel>> GetAllHobbysAsync();
        public Task<HobbyModel> GetHobbyAsync(int id);
        public Task<int> AdđHobbysAsync(HobbyModel model);
        public Task UpdateHobbyAsync(int id, HobbyModel model);
        public Task DeleteHobbyAsync(int id);
    }
}
