using FitAwayApi.Models;
using System.Text.Json;

namespace FitAwayApi.Repo
{
    public class VideoSelectionInsertStrategy : IInsertStrategy
    {
        public async Task<string> InsertJsonRowAsync(string jsonContent, dynamic newRowData, JsonSerializerOptions options)
        {
            var wrapper = JsonSerializer.Deserialize<VideosWrapper>(jsonContent, options);
            if (wrapper == null || wrapper.Videos == null)
            {
                wrapper = new VideosWrapper { Videos = new List<VideoModel>() };
            }

            string rowDataJson = JsonSerializer.Serialize(newRowData, options);
            VideoModel rowDataTyped = JsonSerializer.Deserialize<VideoModel>(rowDataJson, options);

            wrapper.Videos.Add(rowDataTyped); // Assuming newRowData is of type Video

            return JsonSerializer.Serialize(wrapper, options);
        }
    }

}
