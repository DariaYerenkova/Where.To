using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public class BlobStorageModel
    {
        public string FilePath { get; set; }
        public string FileContent { get; set; }
        public string FileName { get; set; }
    }
}
