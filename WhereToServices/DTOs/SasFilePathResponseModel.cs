using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public class SasFilePathResponseModel
    {
        public string FilePatht { get; set; }
        public string SasToken { get; set; }
        public string GuidFileName { get; set; }
    }
}
