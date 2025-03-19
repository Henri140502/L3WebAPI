using L3WebAPI.Common.Dao;

namespace L3WebAPI.DataAcces.Interfaces;

public interface IGameDataAccess
{
    Task<IEnumerable<GameDAO>> GetAllGames();
    Task<GameDAO?> GetGameById(Guid appId);
    Task CreateGame(GameDAO game);
    Task<IEnumerable<GameDAO>?> GetSearchName(string name);
}