using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
//using ToDoAPI.Migrations;

namespace ToDoAPI.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.GetDbSet<T>();
        }
        public Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            return Task.CompletedTask;
            //await _unitOfWork.Commit();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                //await _unitOfWork.Commit();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task UpdateAsync(int id, T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
            //await _unitOfWork.Commit();
        }
    }
}
