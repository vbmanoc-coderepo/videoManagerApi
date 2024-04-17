using System.Text.Json;

namespace FitAwayApi.Repo
{
    public interface IUpdateStrategy
    {
        Task<string> UpdateJsonRowAsync(string jsonContent, dynamic rowData, JsonSerializerOptions options);
    }
}
