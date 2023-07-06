using ToDoAPI.Data;

namespace ToDoAPI.Repositories
{
    public class DbFactory : Disposable, IDbFactory
    {
        HobbyContext context;
        //private readonly DbContextOptions<HobbyContext> options;

        //public DbFactory(DbContextOptions<HobbyContext> options)
        //{
            //this.options = options;
        //}

        public HobbyContext Init()
        {
            //return context ?? (context = new HobbyContext(options));
            return null;
        }
        protected override void DisposeCore()
        {
            if(context != null)
            {
                context.Dispose();
            }
        }
    }
}
