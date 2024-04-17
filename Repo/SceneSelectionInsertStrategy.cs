using FitAwayApi.Models;
using System.Text.Json;

namespace FitAwayApi.Repo
{
    public class SceneSelectionInsertStrategy : IInsertStrategy
    {
        public async Task<string> InsertJsonRowAsync(string jsonContent, dynamic newRowData, JsonSerializerOptions options)
        {
            var wrapper = JsonSerializer.Deserialize<ScenesWrapper>(jsonContent, options);

            if (wrapper == null)
            {
                wrapper = new ScenesWrapper { Scenes = new List<SceneModel>() };
            }

            string rowDataJson = JsonSerializer.Serialize(newRowData, options);
            SceneModel rowDataTyped = JsonSerializer.Deserialize<SceneModel>(rowDataJson, options);

            wrapper.Scenes.Add(rowDataTyped);
            return JsonSerializer.Serialize(wrapper, options);
        }
    }
}
