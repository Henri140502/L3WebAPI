using L3WebAPI.Common.Dao;

namespace L3WebAPI.DataAccess.Interfaces {
	public interface IGamesDataAccess {
		Task<IEnumerable<GameDAO>> GetAllGames();
		Task<GameDAO?> GetGameById(Guid id, CancellationToken cancellationToken);
		Task CreateGame(GameDAO game);
		Task<IEnumerable<GameDAO>> SearchByName(string name);
		Task UpdateGame(GameDAO game);
		Task DeleteGame(Guid id);
	}
}
