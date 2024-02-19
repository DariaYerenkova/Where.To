using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToServices.DTOs;

namespace WhereToServices.Interfaces
{
    public interface IBlobService
    {
        string GetBlobSasUrl(string name);
        Task UploadFileBlobAsync(string filePath, string fileName, string contentType);
        Task DeleteBlobAsync(string filePath);
        string GenerateSasTokenForUserFileName(string userFileName);
        Task UploadPhotoBySas(string token, UploadPhotoUsingSasModel content);
    }
}
