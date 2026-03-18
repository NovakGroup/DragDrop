using DragDrop.Domain.Entities;

namespace DragDrop.Domain.Interfaces;

public interface IDragItemRepository
{
    Task<IEnumerable<DragItem>> GetAllAsync();
    Task<IEnumerable<DragItem>> GetByColumnIdAsync(string columnId);
    Task<DragItem?> GetByIdAsync(int id);
    Task AddAsync(DragItem item);
    Task UpdateAsync(DragItem item);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}
