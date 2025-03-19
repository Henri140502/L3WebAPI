using L3WebAPI.Common.Dao;
using L3WebAPI.DataAcces.Interfaces;

namespace L3WebAPI.DataAcces.Implementaions;

public class GameDataAccess : IGameDataAccess
{
    public Task<IEnumerable<GameDAO>> GetAllGames()
    {
        throw new NotImplementedException();
    }
}