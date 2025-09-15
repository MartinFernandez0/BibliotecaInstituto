using Microsoft.EntityFrameworkCore;

namespace Service.ExtensionMethod
{
    public static class MyExtensions
    {
        public static void TryAttach<TContext, TEntity>(this TContext context, TEntity entity)
        where TContext : DbContext
        where TEntity : class?
        {
            if (entity == null)
            {
                return;
            }
            if (!context.Set<TEntity>().Local.Any(e => e == entity))
            {
                context.Set<TEntity>().Attach(entity);
                return;
            }
            var entryAttached = context.Set<TEntity>().Local.FirstOrDefault(e => e == entity);
            entity = entryAttached;
            return;
        }
    }
}
