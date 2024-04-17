using FitAwayApi.Models;
using System.Text.Json;

namespace FitAwayApi.Repo
{
    public class CategoriesInsertStrategy : IInsertStrategy
    {
        public async Task<string> InsertJsonRowAsync(string jsonContent, dynamic newRowData, JsonSerializerOptions options)
        {
            var wrapper = JsonSerializer.Deserialize<CategoriesWrapper>(jsonContent, options);
            if (wrapper == null)
            {
                wrapper = new CategoriesWrapper { Categories = new List<CategoryModel>() };
            }

            wrapper.Categories.Add(newRowData); // Assuming newRowData is of type CategoryModel
            return JsonSerializer.Serialize(wrapper, options);
        }
    }

}
