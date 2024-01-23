using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhereToDataAccess.Entities;
using WhereToServices;
using WhereToServices.Interfaces;

namespace WhereTo.Controllers
{
    [Route("api/Tours")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService tourService;

        public ToursController(ITourService tourService)
        {
            this.tourService = tourService;
        }

        [HttpGet("{id}")]
        public ActionResult<Tour> GetTourById(int id)
        { 
            var tour = tourService.GetTourById(id);

            return Ok(tour);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tour>> GetTours()
        {
            var tours = tourService.GetTours();

            return Ok(tours);
        }

        [HttpPost]
        public ActionResult CreateTour([FromBody] Tour tour)
        {
            tourService.CreateTour(tour);

            return Created(nameof(ToursController.GetTours), tour);
        }

        [HttpGet("byCity")]
        public ActionResult<IEnumerable<Tour>> GetToursByCity(int cityId)
        {
            var tours = tourService.GetToursByCity(cityId);

            return Ok(tours);
        }

        [HttpGet("byDate")]
        public ActionResult<IEnumerable<Tour>> GetToursByDateRange(DateTime startDate, DateTime endDate)
        {
            var tours = tourService.GetToursByDateRange(startDate, endDate);

            return Ok(tours);
        }

        [HttpGet("upcoming")]
        public ActionResult<IEnumerable<Tour>> GetUpcomingTours()
        {
            var tours = tourService.GetUpcomingTours();

            return Ok(tours);
        }
    }
}
