using L3WebAPI.Buisness.Interfaces;
using L3WebAPI.Common.Dto;

namespace L3WebAPI.Buisness.Implementations;

public class GamesService : IGamesService {
    public Task<IEnumerable<GameDTO>> GetAllGames()
    {
        throw new NotImplementedException();
    }
}