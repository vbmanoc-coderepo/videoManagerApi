using FitAwayApi.Models;
using System.Text.Json;

namespace FitAwayApi.Repo
{
    public class VideoSelectionUpdateStrategy : IUpdateStrategy
    {
        public async Task<string> UpdateJsonRowAsync(string jsonContent, dynamic rowData, JsonSerializerOptions options)
        {
            var wrapper = JsonSerializer.Deserialize<VideosWrapper>(jsonContent, options);
            if (wrapper == null || wrapper.Videos == null) return null;

            string rowDataJson = JsonSerializer.Serialize(rowData, options);
            VideoModel rowDataTyped = JsonSerializer.Deserialize<VideoModel>(rowDataJson, options);

            var videoToUpdate = wrapper.Videos.FirstOrDefault(video => video.Id == rowDataTyped.Id);

            if (videoToUpdate != null)
            {
                var index = wrapper.Videos.IndexOf(videoToUpdate);
                wrapper.Videos[index] = rowDataTyped;
                return JsonSerializer.Serialize(wrapper, options);
            }

            return null;

            //videoToUpdate.Title = rowData.Title;
            //videoToUpdate.Category = rowData.Category;
            //videoToUpdate.Url = rowData.Url;
            //videoToUpdate.Description = rowData.Description;
        }
    }

}
