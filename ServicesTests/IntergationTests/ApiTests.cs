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
using Refit;
using System.Diagnostics;

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
            var command = RequestFactory.CreateRegisterCommand(1, 1);

            var act = () => RegularApiClient.RegisterForTourApi.RegisterUserForTour(command);

            await act.ShouldNotThrowAsync();
        }

        [Fact]
        public async Task RemoveRegistration_Success()
        {
            var command = RequestFactory.CreateRegisterCommand(100, 1000);

            await RegularApiClient.RegisterForTourApi.RemoveUserFromTour(command).ShouldNotThrowAsync();
        }

        [Fact]
        public async Task RegisterUserForTour_Unsuccess()
        {
            var command = RequestFactory.CreateRegisterCommand(0, 1);

            var act = () => RegularApiClient.RegisterForTourApi.RegisterUserForTour(command);

            // Use ShouldThrowAsync to check if the call throws an ApiException
            var exception = await Assert.ThrowsAsync<ApiException>(act);

            // Assert specific properties of the ApiException
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
            Assert.Equal("{\"error\":\"The requested resource was not found.\"}", exception.Content);
        }

        [Fact]
        public async Task RegisterUserForTour_PerfomanceTest()
        {
            const int numberOfRuns = 70;
            var resultArrayMs = new long[numberOfRuns];
            var stopwatch = new Stopwatch();

            for (int i = 0; i < numberOfRuns; i++)
            {
                var command = RequestFactory.CreateRegisterCommand(1, 1);

                stopwatch.Restart();
                var act = () => RegularApiClient.RegisterForTourApi.RegisterUserForTour(command);
                stopwatch.Stop();

                resultArrayMs[i] = stopwatch.ElapsedMilliseconds;
            }

            //Assert
            resultArrayMs.Max().ShouldBeLessThan(300);
            resultArrayMs.Average().ShouldBeLessThan(30);
        }
    }
}
