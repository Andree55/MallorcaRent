using MallorcaRent.Application.Dtos;
using MallorcaRent.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MallorcaRent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _reservationService.GetAllReservationsAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservationRequestDto dto)
        {
            try
            {
                var result = await _reservationService.CreateReservationAsync(dto);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                await _reservationService.DeleteAllReservationsAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }

}
