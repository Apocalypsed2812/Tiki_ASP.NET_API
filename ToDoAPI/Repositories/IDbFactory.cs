using ToDoAPI.Data;

namespace ToDoAPI.Repositories
{
    public interface IDbFactory : IDisposable
    {
        HobbyContext Init();
    }
}
