using DragDrop.Domain.Entities;
using DragDrop.Domain.Interfaces;
using DragDrop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DragDrop.Infrastructure.Repositories;

public class DragItemRepository : IDragItemRepository
{
    private readonly ApplicationDbContext _context;

    public DragItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DragItem>> GetAllAsync()
        => await _context.DragItems.OrderBy(i => i.Order).ToListAsync();

    public async Task<IEnumerable<DragItem>> GetByColumnIdAsync(string columnId)
        => await _context.DragItems
            .Where(i => i.ColumnId == columnId)
            .OrderBy(i => i.Order)
            .ToListAsync();

    public async Task<DragItem?> GetByIdAsync(int id)
        => await _context.DragItems.FindAsync(id);

    public async Task AddAsync(DragItem item)
        => await _context.DragItems.AddAsync(item);

    public Task UpdateAsync(DragItem item)
    {
        _context.DragItems.Update(item);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.DragItems.FindAsync(id);
        if (item is not null)
            _context.DragItems.Remove(item);
    }

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
