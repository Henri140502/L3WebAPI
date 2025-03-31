using L3WebApi.Business.Exceptions;
using L3WebApi.Business.Interfaces;
using L3WebAPI.Common.Dao;
using L3WebAPI.Common.Dto;
using L3WebAPI.Common.Request;
using L3WebAPI.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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
				CheckNameBusinessRules(game.Name);

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

		private static void CheckNameBusinessRules(string name) {
			if (string.IsNullOrWhiteSpace(name)) {
				throw new BusinessRuleException("Le nom doit être defini !");
			}

			if (name.Length > 1000) {
				throw new BusinessRuleException("Le nom doit faire moins de 1000 caracteres !");
			}
		}

		public async Task<IEnumerable<GameDTO>> SearchByName(string name) {
			try {
				return (await _gameDataAccess.SearchByName(name))
					.Select(game => game.ToDto());
			} catch (Exception ex) {
				_logger.LogError(ex, "Erreur lors de la recherche");
				return [];
			}
		}

		public async Task UpdateGame(Guid id, UpdateGameRequest game) {
			try {
				CheckNameBusinessRules(game.Name);

				if (game.Prices.Count() < 1) {
					throw new BusinessRuleException("Le jeu doit avoir au moins un prix !");
				}

				await _gameDataAccess.UpdateGame(new GameDAO {
					AppId = id,
					Name = game.Name,
					Prices = game.Prices.Select(price => new PriceDAO {
						Currency = price.Currency,
						Valeur = price.Valeur,
					})
				});

				/*var dbGame = await _gameDataAccess.GetGameById(id);
				dbGame.Name = game.Name;
				dbGame.Prices = game.Prices.Select(price => new PriceDAO {
					Currency = price.Currency,
					Valeur = price.Valeur,
				});

				await _gameDataAccess.UpdateGame(dbGame);*/
			} catch (Exception ex) {
				_logger.LogError(ex, "Erreur lors de la creation du jeu");
				throw;
			}
		}

		public async Task DeleteGame(Guid id) {
			try {
				await _gameDataAccess.DeleteGame(id);
			} catch (Exception ex) {
				_logger.LogError(ex, "Erreur lors de la suppression du jeu {id}", id);
			}
		}
	}
}
