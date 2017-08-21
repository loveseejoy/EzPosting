using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace EZPost.EntityFramework.Repositories
{
    public abstract class EZPostRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<EZPostDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected EZPostRepositoryBase(IDbContextProvider<EZPostDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class EZPostRepositoryBase<TEntity> : EZPostRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected EZPostRepositoryBase(IDbContextProvider<EZPostDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
