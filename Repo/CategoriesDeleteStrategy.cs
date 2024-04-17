using FitAwayApi.Models;
using System.Text.Json;

namespace FitAwayApi.Repo
{
    public class CategoriesDeleteStrategy : IDeleteStrategy
    {
        public async Task<string> DeleteJsonRowAsync(string jsonContent, int id, JsonSerializerOptions options)
        {
            var wrapper = JsonSerializer.Deserialize<CategoriesWrapper>(jsonContent, options);
            var itemToRemove = wrapper?.Categories?.FirstOrDefault(c => c.Id == id);
            if (itemToRemove != null)
            {
                wrapper.Categories.Remove(itemToRemove);
                return JsonSerializer.Serialize(wrapper, options);
            }

            return jsonContent; // Return original content if item not found
        }
    }

}
