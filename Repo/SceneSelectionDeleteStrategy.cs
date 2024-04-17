using FitAwayApi.Models;
using System.Text.Json;

namespace FitAwayApi.Repo
{
    public class SceneSelectionDeleteDeleteStrategy : IDeleteStrategy
    {
        public async Task<string> DeleteJsonRowAsync(string jsonContent, int id, JsonSerializerOptions options)
        {
            var wrapper = JsonSerializer.Deserialize<ScenesWrapper>(jsonContent, options);
            var itemToRemove = wrapper?.Scenes?.FirstOrDefault(c => c.Id == id);
            if (itemToRemove != null)
            {
                wrapper.Scenes.Remove(itemToRemove);
                return JsonSerializer.Serialize(wrapper, options);
            }

            return jsonContent; // Return original content if item not found
        }
    }

}
