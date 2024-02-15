using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;
using static System.Net.WebRequestMethods;

namespace WhereToServices
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient blobServiceClient;
        CloudBlobClient cloudBlobClient;

        public BlobService(BlobServiceClient blobServiceClient, CloudBlobClient cloudBlobClient)
        {
            this.blobServiceClient = blobServiceClient;
            this.cloudBlobClient = cloudBlobClient;
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

        public async Task<string> GenerateSasTokenForUserFileName(string guidFileName)
        {
            //var containerClient = blobServiceClient.GetBlobContainerClient("feedbackphotos");
            //BlobClient blobClient = containerClient.GetBlobClient(guidFileName);

            //await blobClient.UploadAsync(new MemoryStream(Array.Empty<byte>()), true);

            //BlobSasPermissions sasPermissions = BlobSasPermissions.Write;

            //// Generate the SAS token
            //var sasToken = blobClient.GenerateSasUri(sasPermissions, DateTimeOffset.UtcNow.AddHours(1)).AbsoluteUri;

            CloudBlobContainer container = cloudBlobClient.GetContainerReference("feedbackphotos");

            var blobRef = container.GetBlockBlobReference(guidFileName);
            // Upload the empty blob
            await blobRef.UploadTextAsync("");

            CloudBlockBlob blob = (CloudBlockBlob)blobRef;
            string sasToken = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Write,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1) // set the expiry time for the SAS token
            });

            return $"{blob.Uri}{sasToken}";
        }

        public async Task UploadPhotoBySas(string token, UploadPhotoUsingSasModel content)
        {
            StorageCredentials credentials = new StorageCredentials(token);

            // Create a blob URI object with the SAS token
            CloudBlockBlob blob = new CloudBlockBlob(new Uri(content.BlobUrl), credentials);

            // Upload the file content to the blob
            using (var stream = new MemoryStream(content.fileContent))
            {
                await blob.UploadFromStreamAsync(stream);
            }
        }
    }
}
