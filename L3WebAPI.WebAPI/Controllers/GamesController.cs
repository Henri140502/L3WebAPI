using L3WebApi.Business.Interfaces;
using L3WebAPI.Common.Dto;
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
	}
}
