using Microsoft.AspNetCore.Mvc;
using CinemaApi.Models.Movies.Parameters;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Services;

namespace CinemaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly IService<ListMovieParameter, List<MovieDto>> _listMovieService;
		private readonly IService<CreateMovieParameter, MovieDto> _createMovieService;
		private readonly IService<UpdateMovieParameter, MovieDto> _updateMovieService;
		private readonly IService<GetMovieParameter, MovieDto> _getMovieService;
		private readonly IService<DeleteMovieParameter, MovieDto> _deleteMovieService;

		public MoviesController(
			IService<ListMovieParameter, List<MovieDto>> listMovieService,
			IService<CreateMovieParameter, MovieDto> createMovieService,
			IService<UpdateMovieParameter, MovieDto> updateMovieService,
			IService<GetMovieParameter, MovieDto> getMovieService,
			IService<DeleteMovieParameter, MovieDto> deleteMovieService)
		{
			_listMovieService = listMovieService;
			_createMovieService = createMovieService;
			_updateMovieService = updateMovieService;
			_getMovieService = getMovieService;
			_deleteMovieService = deleteMovieService;
		}

		[HttpGet]
		public async Task<IActionResult> ListAsync(int limit)
		{
			var result = await _listMovieService.CallAsync(
				new ListMovieParameter(limit)
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

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsync(int id)
		{
			var result = await _getMovieService.CallAsync(new GetMovieParameter(id));

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

		[HttpGet("MoviesSearch")]
		public async Task<IActionResult> FilterAsync(
			string? titleInLt,
			string? titleInOriginalLanguage,
			int? releaseYear,
			string? genre)
		{
			var result = await _listMovieService.CallAsync(
				new ListMovieParameter(
					titleInLt,
					titleInOriginalLanguage,
					releaseYear,
					genre
				)
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

		[HttpPost]
		public async Task<IActionResult> CreateAsync(CreateMovieDto movie)
		{
			var result = await _createMovieService.CallAsync(
				new CreateMovieParameter(movie)
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
		public async Task<IActionResult> UpdateAsync(int id, UpdateMovieDto movie)
		{
			if (id != movie.Id)
			{
				return BadRequest(
					"An error occurred. Wrong movie ID was entered. " +
					"Please check the ID and try again."
				);
			}

			var result = await _updateMovieService.CallAsync(
				new UpdateMovieParameter(id, movie)
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
			var result = await _deleteMovieService.CallAsync(
				new DeleteMovieParameter(id)
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