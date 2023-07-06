using ToDoAPI.Models;

namespace ToDoAPI.Services.Hobby
{
    public interface IHobbyService
    {
        public Task<List<HobbyModel>> GetAllHobbys();
        public Task<HobbyModel> GetHobbyById(int id);
        public Task<HobbyModel> AddNewHobby(HobbyModel model);
        public Task<List<HobbyModel>> UpdateHobby(int id, HobbyModel model);
        public Task<List<HobbyModel>> DeleteHobby(int id);
    }
}
