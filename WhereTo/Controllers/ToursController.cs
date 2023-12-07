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
            try
            {
                var tour = tourService.GetTourById(id);

                if (tour == null)
                {
                    return NotFound();
                }

                return Ok(tour);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tour>> GetTours()
        {
            try
            {
                var tours = tourService.GetTours();

                if (tours == null)
                {
                    return NotFound();
                }

                return Ok(tours);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult CreateTour([FromBody] Tour tour)
        {
            try
            {
                tourService.CreateTour(tour);

                return Created(nameof(ToursController.GetTours), tour);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        [HttpGet("byCity")]
        public ActionResult<IEnumerable<Tour>> GetToursByCity(int cityId)
        {
            try
            {
                var tours = tourService.GetToursByCity(cityId);

                if (tours == null)
                {
                    return NotFound();
                }

                return Ok(tours);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        [HttpGet("byDate/{startDate}/{endDate}")]
        public ActionResult<IEnumerable<Tour>> GetToursByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var tours = tourService.GetToursByDateRange(startDate, endDate);

                if (tours == null)
                {
                    return NotFound();
                }

                return Ok(tours);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        [HttpGet("upcoming")]
        public ActionResult<IEnumerable<Tour>> GetUpcomingTours()
        {
            try
            {
                var tours = tourService.GetUpcomingTours();

                if (tours == null)
                {
                    return NotFound();
                }

                return Ok(tours);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }
    }
}
