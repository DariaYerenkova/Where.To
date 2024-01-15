using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.WhereTo_BookingEntities;
using WhereToDataAccess.WhereTo_BookingInterfaces;
using WhereToServices;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

namespace ServicesTests
{
    public class BookingServiceTests
    {
        private readonly BookingService bookingService;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly Mock<IUserFlightRepository> mockUserFlightRepository;
        private readonly Mock<IUserHotelRepository> mockUserHotelRepository;
        private readonly Mock<IQueueMessageSubscriber<WhereToBookingMessage>> mockQueueMessageSubscriber;
        private readonly Mock<IMapper> mockAutomapper;
        private readonly Mock<IHttpClientWrapper> mockHttpClient;
        private readonly Mock<IEventPublisherService<BookingFinishedEvent>> mockEventPublisher;
        private readonly User fakeUser;
        private readonly BookingFinishedEvent fakeEventData;


        public BookingServiceTests()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockUserFlightRepository = new Mock<IUserFlightRepository>();
            mockUserHotelRepository = new Mock<IUserHotelRepository>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockQueueMessageSubscriber = new Mock<IQueueMessageSubscriber<WhereToBookingMessage>>();
            mockEventPublisher = new Mock<IEventPublisherService<BookingFinishedEvent>>();
            mockAutomapper = new Mock<IMapper>();
            mockHttpClient = new Mock<IHttpClientWrapper>();

            var bookingModel = new WhereToBookingMessage("firstname", "lastname", "0000", 1);
            fakeUser = new User { Id = 1, FirstName = "firstname", LastName = "lastname", PassportNumber = "0000" };
            fakeEventData = new BookingFinishedEvent(bookingModel.TourId.ToString(), bookingModel.PassportNumber);

            mockUnitOfWork.Setup(m => m.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(m => m.UserFlights).Returns(mockUserFlightRepository.Object);
            mockUnitOfWork.Setup(m => m.UserHotels).Returns(mockUserHotelRepository.Object);
            bookingService = new BookingService(mockQueueMessageSubscriber.Object, mockUnitOfWork.Object, mockAutomapper.Object, mockHttpClient.Object, mockEventPublisher.Object);
            mockQueueMessageSubscriber.Setup(q => q.ReadMessageFromQueueAsync()).ReturnsAsync(bookingModel);
            mockAutomapper.Setup(x => x.Map<WhereToBookingMessage, User>(It.IsAny<WhereToBookingMessage>())).Returns(fakeUser);
            mockUserRepository.Setup(m => m.Create(It.IsAny<User>())).Returns(fakeUser);
            mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("{ \"TourName\": \"Test Tour\" }") });
        }

        [Fact]
        public async Task InitBookingProcess_Executed()
        {
            // Act
            await bookingService.InitBookingProcess();

            // Assert
            mockQueueMessageSubscriber.Verify(q => q.ReadMessageFromQueueAsync(), Times.Once);
            mockUnitOfWork.Verify(uow => uow.Users.Create(fakeUser), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Exactly(3));
            mockEventPublisher.Verify(p => p.PublishEventAsync(fakeEventData), Times.Once);
            mockQueueMessageSubscriber.Verify(q => q.DeleteMessageAsync(), Times.Once);
        }


    }
}
