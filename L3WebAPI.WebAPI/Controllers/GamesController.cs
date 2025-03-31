using L3WebApi.Business.Exceptions;
using L3WebApi.Business.Interfaces;
using L3WebAPI.Common.Dto;
using L3WebAPI.Common.Request;
using Microsoft.AspNetCore.Mvc;

namespace L3WebAPI.WebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class GamesController : ControllerBase {
		private readonly IGamesService _gamesService;

		public GamesController(IGamesService gamesService) {
			_gamesService = gamesService;
		}

		[HttpGet("")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGames() {
			return Ok(await _gamesService.GetAllGames());
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<GameDTO>> GetGameById(Guid id) {
			var game = await _gamesService.GetGameById(id);
			if (game is null) {
				return NotFound();
			}

			return Ok(game);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> CreateGame([FromBody] CreateGameRequest request) {
			try {
				await _gamesService.CreateGame(request);
				return Ok();
			} catch (BusinessRuleException e) {
				return BadRequest(e.Message);
			}
		}

		[HttpGet("search/{term}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<GameDTO>>> SearchByName(string term) {
			return Ok(await _gamesService.SearchByName(term));
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> UpdateGame(Guid id, [FromBody] UpdateGameRequest request) {
			try {
				await _gamesService.UpdateGame(id, request);
				return Ok();
			} catch (BusinessRuleException e) {
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult> DeleteGame(Guid id) {
			await _gamesService.DeleteGame(id);
			return NoContent();
		}

	}
}
