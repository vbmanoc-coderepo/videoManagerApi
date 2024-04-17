using System.Text.Json;

namespace FitAwayApi.Repo
{
    public interface IDeleteStrategy
    {
        Task<string> DeleteJsonRowAsync(string jsonContent, int id, JsonSerializerOptions options);
    }

}
