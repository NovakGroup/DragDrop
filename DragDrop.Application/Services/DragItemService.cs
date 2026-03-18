using DragDrop.Application.DTOs;
using DragDrop.Application.Interfaces;
using DragDrop.Domain.Entities;
using DragDrop.Domain.Interfaces;

namespace DragDrop.Application.Services;

public class DragItemService : IDragItemService
{
    private readonly IDragItemRepository _repository;

    public DragItemService(IDragItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DragItemDto>> GetAllItemsAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<IEnumerable<DragItemDto>> GetItemsByColumnAsync(string columnId)
    {
        var items = await _repository.GetByColumnIdAsync(columnId);
        return items.Select(MapToDto);
    }

    public async Task<DragItemDto?> GetItemByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<DragItemDto> CreateItemAsync(DragItemDto dto)
    {
        var item = MapToEntity(dto);
        item.CreatedAt = DateTime.UtcNow;
        await _repository.AddAsync(item);
        await _repository.SaveChangesAsync();
        return MapToDto(item);
    }

    public async Task UpdateItemAsync(DragItemDto dto)
    {
        var item = await _repository.GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException($"DragItem with id {dto.Id} not found.");

        item.Title = dto.Title;
        item.Description = dto.Description;
        item.Order = dto.Order;
        item.ColumnId = dto.ColumnId;
        item.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(item);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteItemAsync(int id)
    {
        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
    }

    public async Task MoveItemAsync(int id, string targetColumnId, int newOrder)
    {
        var item = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"DragItem with id {id} not found.");

        item.ColumnId = targetColumnId;
        item.Order = newOrder;
        item.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(item);
        await _repository.SaveChangesAsync();
    }

    private static DragItemDto MapToDto(DragItem item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        Description = item.Description,
        Order = item.Order,
        ColumnId = item.ColumnId,
        CreatedAt = item.CreatedAt,
        UpdatedAt = item.UpdatedAt
    };

    private static DragItem MapToEntity(DragItemDto dto) => new()
    {
        Id = dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        Order = dto.Order,
        ColumnId = dto.ColumnId
    };
}
