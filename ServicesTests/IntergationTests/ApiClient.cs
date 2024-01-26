using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesTests.IntergationTests
{
    public class ApiClient
    {
        public IRegisterForTourApi RegisterForTourApi { get; }

        public ApiClient(HttpClient httpClient)
        {
            RegisterForTourApi = RestService.For<IRegisterForTourApi>(httpClient);
        }
    }
}
