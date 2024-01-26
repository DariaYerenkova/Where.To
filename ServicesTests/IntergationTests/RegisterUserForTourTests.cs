using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess;
using WhereToDataAccess.Entities;

namespace ServicesTests.IntergationTests
{
    public class RegisterUserForTourTests 
    {
        WebApplicationFactory<Program> webHost;
        HttpClient httpClient;

        public RegisterUserForTourTests()
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

            httpClient = webHost.CreateClient();
        }

        [Fact]
        public async Task RegisterUserForTour_EndpointShouldReturnCreatedStatusCode()
        {
            // Arrange
            var userTourRequest = new UserTour
            {
                UserId = 1,
                TourId = 1
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(userTourRequest), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await httpClient.PostAsync("/api/Register", jsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task RemoveRegistration_EndpointShouldReturnCreatedStatusCode()
        {
            // Arrange
            var userTourRequest = new UserTour
            {
                UserId = 1,
                TourId = 1
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(userTourRequest), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await httpClient.PostAsync("/api/Register/RemoveRegistration", jsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
