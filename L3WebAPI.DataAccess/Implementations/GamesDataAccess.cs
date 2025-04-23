using L3WebAPI.Common.Dao;
using L3WebAPI.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L3WebAPI.DataAccess.Implementations {
	public class GamesDataAccess : IGamesDataAccess {
		private readonly GameDbContext _dbContext;

		public GamesDataAccess(GameDbContext dbContext) {
			_dbContext = dbContext;
		}

		public async Task CreateGame(GameDAO game) {
			_dbContext.Games.Add(game);
			await _dbContext.SaveChangesAsync();
		}

		public Task DeleteGame(Guid id) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<GameDAO>> GetAllGames() {
			return _dbContext.Games.Include(x => x.Prices).ToList();
			//return _dbContext.Games.Include(x => x.Prices).Include(x => x.OtherField).ToList();
			//return _dbContext.Games.Include(x => x.Prices).ThenInclude(x => x.PriceField).ToList();
		}

		public Task<GameDAO?> GetGameById(Guid id) {
			return _dbContext.Games.Include(x => x.Prices).FirstOrDefaultAsync(x => x.AppId == id);
		}

		public Task<IEnumerable<GameDAO>> SearchByName(string name) {
			throw new NotImplementedException();
		}

		public Task UpdateGame(GameDAO game) {
			throw new NotImplementedException();
		}
	}
}
