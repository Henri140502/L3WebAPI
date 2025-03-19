using L3WebAPI.Common;
using L3WebAPI.Common.Dao;
using L3WebAPI.DataAccess.Interfaces;

namespace L3WebAPI.DataAccess.Implementations {
	public class GamesDataAccess : IGamesDataAccess {
		private static List<GameDAO> games = [
			new GameDAO {
				AppId = Guid.NewGuid(),
				Name = "Portal 2",
				Prices = [
					new PriceDAO {
						Valeur = 19.99M,
						Currency = Currency.USD,
					}
				]
			},
			new GameDAO {
				AppId = Guid.NewGuid(),
				Name = "Half-Life 2",
				Prices = [
					new PriceDAO {
						Valeur = 14.99M,
						Currency = Currency.EUR,
					},
					new PriceDAO {
						Valeur = 15.99M,
						Currency = Currency.USD,
					}
				]
			}
		];

		public async Task<IEnumerable<GameDAO>> GetAllGames() {
			return games;
		}
	}
}
