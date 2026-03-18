using DragDrop.Application.DTOs;

namespace DragDrop.Application.Interfaces;

public interface IDragItemService
{
    Task<IEnumerable<DragItemDto>> GetAllItemsAsync();
    Task<IEnumerable<DragItemDto>> GetItemsByColumnAsync(string columnId);
    Task<DragItemDto?> GetItemByIdAsync(int id);
    Task<DragItemDto> CreateItemAsync(DragItemDto dto);
    Task UpdateItemAsync(DragItemDto dto);
    Task DeleteItemAsync(int id);
    Task MoveItemAsync(int id, string targetColumnId, int newOrder);
}
