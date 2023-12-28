using AutoMapper;
using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.WhereTo_BookingEntities;
using WhereToDataAccess.WhereTo_BookingInterfaces;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;
using User = WhereToDataAccess.WhereTo_BookingEntities.User;

namespace WhereToServices
{
    public class BookingService : IBookingService
    {
        private readonly IQueueMessageSubscriber<WhereToBookingMessage> queueMessageSubscriber;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly HttpClient client;
        private User createdUser;

        public BookingService(IQueueMessageSubscriber<WhereToBookingMessage> queueMessageSubscriber, IUnitOfWork uow, IMapper mapper, HttpClient client)
        {
            this.queueMessageSubscriber = queueMessageSubscriber;
            this.uow = uow;
            this.mapper = mapper;
            this.client = client;
        }

        public async Task InitBookingProcess()
        {
            var bookingModel = await queueMessageSubscriber.ReadMessageFromQueueAsync();

            if (bookingModel != null)
            {
                await RegisterUserAsync(bookingModel);
                await BookFlight(bookingModel);
                await BookHotel(bookingModel);
                await queueMessageSubscriber.DeleteMessageAsync();
            }           
        }

        private async Task RegisterUserAsync(WhereToBookingMessage model)
        {
            var user = mapper.Map<WhereToBookingMessage, User>(model);
            createdUser = uow.Users.Create(user);
            await uow.SaveAsync();
        }

        private async Task BookFlight(WhereToBookingMessage model)
        {
            UserFlight userFlight = new UserFlight();
            userFlight.UserId = createdUser.Id;
            userFlight.FlightNumber = new Random().Next(100000, 1000000).ToString();
            userFlight.TourId = model.TourId;

            uow.UserFlights.Create(userFlight);
            await uow.SaveAsync();
        }

        private async Task BookHotel(WhereToBookingMessage model)
        {
            UserHotel userHotel = new UserHotel();
            userHotel.UserId = createdUser.Id;
            userHotel.TourId = model.TourId;

            var response = client.GetAsync($"http://localhost:5269/api/Tours/{model.TourId}").Result;
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                Tour tour = JsonConvert.DeserializeObject<Tour>(content);
                userHotel.HotelDetails = $"Hotel for tour {tour.TourName}";
            }
            else
            {
                userHotel.HotelDetails = String.Empty;
            }
            
            await uow.SaveAsync();
        }
    }
}
