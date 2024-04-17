using FitAwayApi.Models;
using System.Text.Json;

namespace FitAwayApi.Repo
{
    public class VideoSelectionDeleteStrategy : IDeleteStrategy
    {
        public async Task<string> DeleteJsonRowAsync(string jsonContent, int id, JsonSerializerOptions options)
        {
            var wrapper = JsonSerializer.Deserialize<VideosWrapper>(jsonContent, options);
            var itemToRemove = wrapper?.Videos?.FirstOrDefault(c => c.Id == id);
            if (itemToRemove != null)
            {
                wrapper.Videos.Remove(itemToRemove);
                return JsonSerializer.Serialize(wrapper, options);
            }

            return jsonContent; // Return original content if item not found
        }
    }
}
