using ToDoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Data;
using ToDoAPI.Repositories.HobbyRepository;

namespace ToDoAPI.Services.Hobby
{
    public class HobbyService : IHobbyService
    {
        private readonly IHobbyRepository _hobbyRepo;

        public HobbyService(IHobbyRepository repo)
        {
            _hobbyRepo = repo;
        }

        public async Task<List<HobbyModel>> GetAllHobbys()
        {
            var hobbys = await _hobbyRepo.GetAllHobbysAsync();
            return hobbys;
        }

        public async Task<HobbyModel> GetHobbyById(int id)
        {
            var hobby = await _hobbyRepo.GetHobbyAsync(id);
            return hobby;
        }

        public async Task<HobbyModel> AddNewHobby(HobbyModel model)
        {
            var newHobbyId = await _hobbyRepo.AdđHobbysAsync(model);
            var newHobby = await _hobbyRepo.GetHobbyAsync(newHobbyId);
            return newHobby;
        }

        public async Task<List<HobbyModel>> UpdateHobby(int id, HobbyModel model)
        {
            await _hobbyRepo.UpdateHobbyAsync(id, model);
            var hobbys = await _hobbyRepo.GetAllHobbysAsync();
            return hobbys;
        }

        public async Task<List<HobbyModel>> DeleteHobby(int id)
        {
            await _hobbyRepo.DeleteHobbyAsync(id);
            var hobbys = await _hobbyRepo.GetAllHobbysAsync();
            return hobbys;
        }
    }
}
