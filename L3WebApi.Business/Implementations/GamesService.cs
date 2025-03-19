using L3WebApi.Business.Interfaces;
using L3WebAPI.Common.Dto;
using L3WebAPI.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace L3WebApi.Business.Implementations {
	public class GamesService : IGamesService {
		private readonly IGamesDataAccess _gameDataAccess;
		private readonly ILogger<GamesService> _logger;

		public GamesService(IGamesDataAccess gameDataAccess, ILogger<GamesService> logger) {
			_gameDataAccess = gameDataAccess;
			_logger = logger;
		}

		public async Task<IEnumerable<GameDTO>> GetAllGames() {
			try {
				var games = await _gameDataAccess.GetAllGames();
				return games.Select(game => game.ToDto());
			} catch (Exception ex) {
				_logger.LogError(ex, "Erreur lors de la récuparation des jeux");
				return [];
			}
		}
	}
}
