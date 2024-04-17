using FitAwayApi.Models;
using System.Text.Json;

namespace FitAwayApi.Repo
{
    public class SceneSelectionUpdateStrategy : IUpdateStrategy
    {
        public async Task<string> UpdateJsonRowAsync(string jsonContent, dynamic rowData, JsonSerializerOptions options)
        {
            //Custom logic for updating sceneSelection.json

            var wrapper = JsonSerializer.Deserialize<ScenesWrapper>(jsonContent, options);
            if (wrapper == null || wrapper.Scenes == null) return null;

            string rowDataJson = JsonSerializer.Serialize(rowData, options);
            SceneModel rowDataTyped = JsonSerializer.Deserialize<SceneModel>(rowDataJson, options);

            var itemToUpdate = wrapper.Scenes.FirstOrDefault(item => item.Id == rowDataTyped.Id);

            if(itemToUpdate != null)
            {
                var index = wrapper.Scenes.IndexOf(itemToUpdate);
                wrapper.Scenes[index] = rowDataTyped;
                return JsonSerializer.Serialize(wrapper);
            }

            return null;
        }
    }

}
