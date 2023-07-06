using AutoMapper;
using ToDoAPI.Models;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;

namespace ToDoAPI.Repositories.HobbyRepository
{
    public class HobbyRepository : IHobbyRepository
    {
        private readonly HobbyContext _context;
        private readonly IMapper _mapper;

        public HobbyRepository(HobbyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AdđHobbysAsync(HobbyModel model)
        {
            var newHobby = _mapper.Map<Hobby>(model);
            _context.Hobbys!.Add(newHobby);
            await _context.SaveChangesAsync();

            return newHobby.Id;
        }

        public async Task DeleteHobbyAsync(int id)
        {
            var deleteHobby = _context.Hobbys!.SingleOrDefault(b => b.Id == id);
            if (deleteHobby != null)
            {
                _context.Hobbys!.Remove(deleteHobby);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<HobbyModel>> GetAllHobbysAsync()
        {
            var hobbys = await _context.Hobbys!.ToListAsync();
            return _mapper.Map<List<HobbyModel>>(hobbys);
        }

        public async Task<HobbyModel> GetHobbyAsync(int id)
        {
            var hobby = await _context.Hobbys!.FindAsync(id);
            return _mapper.Map<HobbyModel>(hobby);
        }

        public async Task UpdateHobbyAsync(int id, HobbyModel model)
        {
            if (id == model.Id)
            {
                var updateHobby = _mapper.Map<Hobby>(model);
                _context.Hobbys!.Update(updateHobby);
                await _context.SaveChangesAsync();
            }
        }
    }
}
