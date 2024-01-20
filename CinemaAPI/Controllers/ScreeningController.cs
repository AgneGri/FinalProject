using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;
using CinemaApi.Services;
using CinemaApi.Services.Screenings.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScreeningController : ControllerBase
	{
		private readonly IService<ListScreeningParameter, List<ScreeningDto>> _listScreeningService;
		private readonly IService<ListScreeningParameter, List<ScreeningShowingsDto>> _listScreeningWithVariousParametersService;
		private readonly IService<CreateScreeningParameter, ScreeningDto> _createScreeningService;
		private readonly IService<UpdateScreeningParameter, ScreeningDto> _updateScreeningService;
		private readonly IService<GetScreeningParameter, ScreeningDto> _getScreeningService;
		private readonly IService<DeleteScreeningParameter, ScreeningDto> _deleteScreeningService;

		public ScreeningController(
			IService<ListScreeningParameter, List<ScreeningDto>> listScreeningService,
			IService<ListScreeningParameter, List<ScreeningShowingsDto>> listScreeningWithVariousParametersService,
			IService<CreateScreeningParameter, ScreeningDto> createScreeningService,
			IService<UpdateScreeningParameter, ScreeningDto> updateScreeningService, 
			IService<GetScreeningParameter, ScreeningDto> getScreeningService, 
			IService<DeleteScreeningParameter, ScreeningDto> deleteScreeningService)
		{
			_listScreeningService = listScreeningService;
			_listScreeningWithVariousParametersService = listScreeningWithVariousParametersService;
			_createScreeningService = createScreeningService;
			_updateScreeningService = updateScreeningService;
			_getScreeningService = getScreeningService;
			_deleteScreeningService = deleteScreeningService;
		}

		[HttpGet]
		public async Task<IActionResult> ListAsync(int limit)
		{
			var result = await _listScreeningService.CallAsync(
				new ListScreeningParameter(limit));

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
			var result = await _getScreeningService.CallAsync(new GetScreeningParameter(id));

			if (result.Status == 200)
			{
				return Ok(result);
			}

			if (result.Status == 400)
			{
				return NotFound(result);
			}

			if (result.Status == 404)
			{
				return NotFound(result);
			}

			return StatusCode(500, result);
		}

		[HttpGet("Upcoming showings")]
		public async Task<IActionResult> FilterAsync(
			string? city,
			DateTime? showDate,
			string? auditorium,
			string? genre)
		{
			var result = await _listScreeningWithVariousParametersService.CallAsync(
				new ListScreeningParameter(
					city,
					showDate,
					auditorium,
					genre
				)
			);

			if (result.Status == 200)
			{
				return Ok(result);
			}

			if (result.Status == 400)
			{
				return NotFound(result);
			}

			if (result.Status == 404)
			{
				return NotFound(result);
			}

			return StatusCode(500, result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(CreateScreeningDto screening)
		{
			var result = await _createScreeningService.CallAsync(
				new CreateScreeningParameter(screening)
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

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id, UpdateScreeningDto screening)
		{
			if (id != screening.Id)
			{
				return BadRequest(
					"An error occurred. Wrong screening Id was entered. " +
					"Please check the Id and try again."
				);
			}

			var result = await _updateScreeningService.CallAsync(
				new UpdateScreeningParameter(id, screening)
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
			var result = await _deleteScreeningService.CallAsync(
			new DeleteScreeningParameter(id)
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