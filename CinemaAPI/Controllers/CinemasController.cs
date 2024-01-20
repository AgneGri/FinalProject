using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Models.Cinemas.Parameters;
using CinemaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CinemasController : ControllerBase
	{
		private readonly IService<ListCinemaParameter, List<CinemaDto>> _listCinemaService;
		private readonly IService<CreateCinemaParameter, CinemaDto> _createCinemaService;
		private readonly IService<UpdateCinemaParameter, CinemaDto> _updateCinemaService;
		private readonly IService<GetCinemaParameter, CinemaDto> _getCinemaService;
		private readonly IService<DeleteCinemaParameter, CinemaDto> _deleteCinemaService;

		public CinemasController(
			IService<ListCinemaParameter, List<CinemaDto>> listCinemaService, 
			IService<CreateCinemaParameter, CinemaDto> createCinemaService, 
			IService<UpdateCinemaParameter, CinemaDto> updateCinemaService, 
			IService<GetCinemaParameter, CinemaDto> getCinemaService, 
			IService<DeleteCinemaParameter, CinemaDto> deleteCinemaService)
		{
			_listCinemaService = listCinemaService;
			_createCinemaService = createCinemaService;
			_updateCinemaService = updateCinemaService;
			_getCinemaService = getCinemaService;
			_deleteCinemaService = deleteCinemaService;
		}

		[HttpGet]
		public async Task<IActionResult> ListAsync(int limit)
		{
			var result = await _listCinemaService.CallAsync(
				new ListCinemaParameter(limit)
			);

			if (result.Status == 200)
			{
				return Ok(result);
			}

			if (result.Status == 400)
			{
				return BadRequest(result);
			}

			return StatusCode(500, result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsync(int id)
		{
			var result = await _getCinemaService.CallAsync(new GetCinemaParameter(id));

			if (result.Status == 200)
			{
				return Ok(result);
			}

			if (result.Status == 400)
			{
				return BadRequest(result);
			}

			if (result.Status == 404)
			{
				return NotFound(result);
			}

			return StatusCode(500, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(CreateCinemaDto movie)
		{
			var result = await _createCinemaService.CallAsync(
				new CreateCinemaParameter(movie)
			);

			if (result.Status == 200)
			{
				return Ok(result);
			}

			if (result.Status == 400)
			{
				return BadRequest(result);
			}

			if (result.Status == 409)
			{
				return Conflict(result);
			}

			return StatusCode(500, result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id, UpdateCinemaDto cinema)
		{
			if (id != cinema.Id)
			{
				return BadRequest(
					"An error occurred. Wrong cinema ID was entered. " +
					"Please check the ID and try again."
				);
			}

			var result = await _updateCinemaService.CallAsync(
				new UpdateCinemaParameter(id, cinema)
			);

			if (result.Status == 200)
			{
				return Ok(result);
			}

			if (result.Status == 400)
			{
				return BadRequest(result);
			}

			if (result.Status == 404)
			{
				return NotFound(result);
			}

			return StatusCode(500, result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var result = await _deleteCinemaService.CallAsync(
				new DeleteCinemaParameter(id)
			);

			if (result.Status == 200)
			{
				return Ok(result);
			}

			if (result.Status == 400)
			{
				return BadRequest(result);
			}

			if (result.Status == 404)
			{
				return NotFound(result);
			}

			return StatusCode(500, result);
		}
	}
}