using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.Interfaces
{
    public interface IBlobService
    {
        Task<BlobDownloadResult> GetBlobAsync(string name);
        Task UploadFileBlobAsync(string filePath, string fileName, string contentType);
        Task DeleteBlobAsync(string filePath);
    }
}
