using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public class UploadPhotoUsingSasModel
    {
        public byte[] fileContent { get; set; }
        public string BlobUrl { get; set; }
    }
}
