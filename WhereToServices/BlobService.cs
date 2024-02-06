using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToServices.Interfaces;

namespace WhereToServices
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            this.blobServiceClient = blobServiceClient;
        }

        public Task DeleteBlobAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<BlobDownloadResult> GetBlobAsync(string name)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("feedbackphotos");
            var blobClient = containerClient.GetBlobClient(name);
            var blobDownloadInfo = await blobClient.DownloadContentAsync();

            return blobDownloadInfo.Value;
        }

        public async Task UploadFileBlobAsync(string filePath, string fileName, string contentType)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("feedbackphotos");
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = contentType });
        }
    }
}
