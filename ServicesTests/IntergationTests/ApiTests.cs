using Azure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess;
using Shouldly;

namespace ServicesTests.IntergationTests
{
    public class ApiTests
    {
        private static WebApplicationFactory<Program> webHost { get; set; }
        private ApiClient RegularApiClient { get; set; }

        public ApiTests()
        {
            webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builer =>
            {
                builer.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<WhereToDataContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<WhereToDataContext>(options =>
                    {
                        options.UseInMemoryDatabase("delivery_db");
                    });
                });
            });

            RegularApiClient = GetRegularApiClient();
        }

        private ApiClient GetRegularApiClient() => new(webHost.CreateDefaultClient());

        [Fact]
        public async Task RegisterUserForTour_Success()
        {
            var command = RequestFactory.CreateCommand(1, 1);

            await RegularApiClient.RegisterForTourApi.RegisterUserForTour(command).ShouldNotThrowAsync();
        }

        [Fact]
        public async Task RemoveRegistration_Success()
        {
            var command = RequestFactory.CreateCommand(1, 1);

            await RegularApiClient.RegisterForTourApi.RemoveUserFromTour(command).ShouldNotThrowAsync();
        }
    }
}
