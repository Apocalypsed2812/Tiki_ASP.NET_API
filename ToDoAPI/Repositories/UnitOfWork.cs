using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;

namespace ToDoAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HobbyContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(HobbyContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return _context.Set<T>();
        }

        public IMapper GetMapper()
        {
            return _mapper;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
