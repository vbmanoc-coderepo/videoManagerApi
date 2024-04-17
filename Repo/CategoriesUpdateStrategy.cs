using FitAwayApi.Models;
using System.Text.Json;

namespace FitAwayApi.Repo
{
    public class CategoriesUpdateStrategy : IUpdateStrategy
    {
        public async Task<string> UpdateJsonRowAsync(string jsonContent, dynamic rowData, JsonSerializerOptions options)
        {
            var wrapper = JsonSerializer.Deserialize<CategoriesWrapper>(jsonContent, options);
            if (wrapper == null || wrapper.Categories == null) return null;

            string rowDataJson = JsonSerializer.Serialize(rowData, options);
            CategoryModel rowDataTyped = JsonSerializer.Deserialize<CategoryModel>(rowDataJson, options);

            var itemToUpdate = wrapper.Categories.FirstOrDefault(item => item.Id == rowDataTyped.Id);

            if (itemToUpdate != null)
            {
                var index = wrapper.Categories.IndexOf(itemToUpdate);
                wrapper.Categories[index] = rowDataTyped;

                return JsonSerializer.Serialize(wrapper);
            }

            return null;
        }
    }
}
