using L3WebAPI.Buisness.Interfaces;
using L3WebAPI.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace L3WebAPI.WebAPI.Controllers;
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase {
        private readonly IGamesService _gameService; 
        public GamesController(IGamesService gameService) {
            _gameService = gameService;
        }
        [HttpGet("")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGames() { 
            return Ok(await _gameService.GetAllGames());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameDTO>> GetGameById(Guid id)
        {
            var game = await _gameService.GetGameById(id);
            if (game == null) {
                return NotFound();
            }
            return Ok(game);
        }
    }