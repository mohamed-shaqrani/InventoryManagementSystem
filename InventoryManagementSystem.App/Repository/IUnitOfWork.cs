using InventoryManagementSystem.App.Entities;

namespace InventoryManagementSystem.App.Repository;
public interface IUnitOfWork
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    Task<int> SaveChangesAsync();
}
