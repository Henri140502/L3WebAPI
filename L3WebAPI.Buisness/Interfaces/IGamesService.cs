using L3WebAPI.Common.Dto;
using L3WebAPI.Common.Request;

namespace L3WebAPI.Buisness.Interfaces;

public interface IGamesService
{
    Task<IEnumerable<GameDTO>> GetAllGames();
    Task<GameDTO?> GetGameById(Guid appId);
    Task CreateGame(CreateGameRequest game);
    Task<IEnumerable<GameDTO>?> GetSearchName(string name);
}