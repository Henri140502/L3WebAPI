using L3WebApi.Business.Exceptions;
using L3WebApi.Business.Interfaces;
using L3WebAPI.Common.Dao;
using L3WebAPI.Common.Dto;
using L3WebAPI.Common.Request;
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

		public async Task<GameDTO?> GetGameById(Guid id) {
			try {
				var game = await _gameDataAccess.GetGameById(id);
				/*if (game is null) {
					return null;
				}*/

				return game?.ToDto();
			} catch (Exception ex) {
				_logger.LogError(ex, "Erreur lors de la récupération du jeu {id}", id);
				return null;
			}
		}

		public async Task CreateGame(CreateGameRequest game) {
			try {
				if (string.IsNullOrWhiteSpace(game.Name)) {
					throw new BusinessRuleException("Le nom doit être defini !");
				}

				if (game.Name.Length > 1000) {
					throw new BusinessRuleException("Le nom doit faire moins de 1000 caracteres !");
				}

				if (game.Prices.Count() < 1) {
					throw new BusinessRuleException("Le jeu doit avoir au moins un prix !");
				}

				await _gameDataAccess.CreateGame(new GameDAO {
					AppId = Guid.NewGuid(),
					Name = game.Name,
					Prices = game.Prices.Select(price => new PriceDAO {
						Currency = price.Currency,
						Valeur = price.Valeur,
					})
				});
			} catch (Exception ex) {
				_logger.LogError(ex, "Erreur lors de la creation du jeu");
				throw;
			}
		}
	}
}
