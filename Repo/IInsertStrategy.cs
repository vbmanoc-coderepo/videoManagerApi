using System.Text.Json;

namespace FitAwayApi.Repo
{
    public interface IInsertStrategy
    {
        Task<string> InsertJsonRowAsync(string jsonContent, dynamic newRowData, JsonSerializerOptions options);
    }

}
