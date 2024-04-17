using FitAwayApi.Models;

namespace FitAwayApi.Services
{
    public interface IBlobStorageService
    {
        Task<string> GetJsonDataAsync(string fileName);
        Task<bool> InsertJsonRowAsync(string fileName, dynamic newRowData);
        Task<bool> UpdateJsonRowAsync(string fileName, dynamic updatedItem);
        Task<bool> DeleteJsonRowAsync(string fileName, int id);
    }
}