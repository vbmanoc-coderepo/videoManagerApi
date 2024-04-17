using Azure.Storage.Blobs;
using FitAwayApi.Models;
using FitAwayApi.Repo;
using System.Text;
using System.Text.Json;

namespace FitAwayApi.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly Dictionary<string, IInsertStrategy> InsertStrategies;
        private readonly Dictionary<string, IUpdateStrategy> UpdateStrategies;
        private readonly Dictionary<string, IDeleteStrategy> DeleteStrategies;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("AzureBlobStorage:ConnectionString");
            _containerName = configuration.GetValue<string>("AzureBlobStorage:ContainerName");
            _blobServiceClient = new BlobServiceClient(connectionString);

            UpdateStrategies = new Dictionary<string, IUpdateStrategy>
            {
                { "categories", new CategoriesUpdateStrategy() },
                { "sceneSelection", new SceneSelectionUpdateStrategy() },
                { "videoSelection", new VideoSelectionUpdateStrategy() }
            };

            InsertStrategies = new Dictionary<string, IInsertStrategy>
            {
                { "categories", new CategoriesInsertStrategy() },
                { "sceneSelection", new SceneSelectionInsertStrategy() },
                { "videoSelection", new VideoSelectionInsertStrategy() }
            };

            DeleteStrategies = new Dictionary<string, IDeleteStrategy>
            {
                { "categories", new CategoriesDeleteStrategy() },
                { "sceneSelection", new SceneSelectionDeleteDeleteStrategy() },
                { "videoSelection", new VideoSelectionDeleteStrategy() }
            };
        }

        public async Task<string> GetJsonDataAsync(string fileName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient($"{fileName}.json");

            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadContentAsync();
                return response.Value.Content.ToString();
            }

            return null;
        }

        public async Task<bool> InsertJsonRowAsync(string fileName, dynamic newRowData)
        {
            if (!InsertStrategies.TryGetValue(fileName, out var strategy))
                throw new ArgumentException("No strategy found for the given file name.", nameof(fileName));

            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient($"{fileName}.json");

            if (!await blobClient.ExistsAsync())
                return false;


            var response = await blobClient.DownloadContentAsync();
            var content = response.Value.Content.ToString();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var JsonInsert = await strategy.InsertJsonRowAsync(content, newRowData, options);
            if (JsonInsert != null)
            {
                using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonInsert));
                await blobClient.UploadAsync(memoryStream, overwrite: true);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateJsonRowAsync(string fileName, dynamic updatedItem)
        {
            if (!UpdateStrategies.TryGetValue(fileName, out var strategy))
                throw new ArgumentException("No strategy found for the given file name.", nameof(fileName));

            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient($"{fileName}.json");

            if (!await blobClient.ExistsAsync())
                return false;


            var response = await blobClient.DownloadContentAsync();
            var content = response.Value.Content.ToString();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var JsonUpdate = await strategy.UpdateJsonRowAsync(content, updatedItem, options);
            if (JsonUpdate != null)
            {
                using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonUpdate));
                await blobClient.UploadAsync(memoryStream, overwrite: true);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteJsonRowAsync(string fileName, int id)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient($"{fileName}.json");

            if (!await blobClient.ExistsAsync())
                return false;

            var response = await blobClient.DownloadContentAsync();
            var jsonContent = response.Value.Content.ToString();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            string updatedJson = string.Empty;

            if (DeleteStrategies.TryGetValue(fileName, out var strategy))
            {
                updatedJson = await strategy.DeleteJsonRowAsync(jsonContent, id, options);
            }
            else
            {
                return false; // Strategy not found for the given fileName
            }

            using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(updatedJson));
            await blobClient.UploadAsync(memoryStream, true);
            return true;
        }


    }

}
