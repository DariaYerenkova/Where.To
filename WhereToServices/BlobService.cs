using Azure.Storage;
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
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

namespace WhereToServices
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient blobServiceClient;
        StorageSharedKeyCredential storageSharedKeyCredential;

        public BlobService(BlobServiceClient blobServiceClient, StorageSharedKeyCredential storageSharedKeyCredential)
        {
            this.blobServiceClient = blobServiceClient;
            this.storageSharedKeyCredential = storageSharedKeyCredential;
        }

        public Task DeleteBlobAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public string GetBlobSasUrl(string name)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("feedbackphotos");
            BlobClient blobClient = containerClient.GetBlobClient(name);

            var sasToken = GenerateReadWriteSasToken(false, name);
            var sasUrl = blobClient.Uri.AbsoluteUri + "?" + sasToken;

            return sasUrl;
        }

        public async Task UploadFileBlobAsync(string filePath, string fileName, string contentType)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("feedbackphotos");
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = contentType });
        }

        public string GenerateSasTokenForUserFileName(string guidFileName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("feedbackphotos");
            BlobClient blobClient = containerClient.GetBlobClient(guidFileName);

            var sasToken = GenerateReadWriteSasToken(true, guidFileName);
            var sasUrl = blobClient.Uri.AbsoluteUri + "?" + sasToken;

            return sasUrl;
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

        private string GenerateReadWriteSasToken(bool isWritable, string guidFileName)
        {
            BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = "feedbackphotos",
                BlobName = guidFileName,
                ExpiresOn = DateTime.UtcNow.AddHours(1),
            };
            blobSasBuilder.SetPermissions(isWritable ? BlobSasPermissions.Write : BlobSasPermissions.Read);
            return blobSasBuilder.ToSasQueryParameters(storageSharedKeyCredential).ToString();
        }
    }
}
