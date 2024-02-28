using Azure;
using EmptyFiles;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Refit;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess;
using WhereToServices.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServicesTests.IntergationTests
{
    public class FeedbackApiTests
    {
        private static WebApplicationFactory<Program> webHost { get; set; }
        private ApiClient RegularApiClient { get; set; }

        private ApiClient GetRegularApiClient() => new(webHost.CreateDefaultClient());

        public FeedbackApiTests()
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
                        options.UseSqlServer("Server=tcp:wheretosqlserver.database.windows.net,1433;Initial Catalog=WhereTo;Persist Security Info=False;User ID=sqladmin;Password=SecretPassword1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                    });
                });
            });

            RegularApiClient = GetRegularApiClient();
        }

        [Fact]
        public async Task GetFeedback_ReturnsFeedback()
        {
            await RegularApiClient.FeedbackApi.GetFeedback(23).ShouldNotThrowAsync();

            var feedback = await RegularApiClient.FeedbackApi.GetFeedback(23);

            Assert.Equal(23, feedback.FeedbackId);
        }

        [Fact]
        public async Task GetFeedback_ReturnsNotFound()
        {
            var act = () => RegularApiClient.FeedbackApi.GetFeedback(123);

            var exception = await Assert.ThrowsAsync<ApiException>(act);

            // Assert specific properties of the ApiException
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
            Assert.Equal("{\"error\":\"The requested resource was not found.\"}", exception.Content);
        }
    }
}
