using L3WebAPI.Buisness.Interfaces;
using L3WebAPI.Common.Dto;
using L3WebAPI.DataAcces.Interfaces;
using Microsoft.Extensions.Logging;

namespace L3WebAPI.Buisness.Implementations;

public class GamesService : IGamesService
{
    private readonly IGameDataAccess _gameDataAccess;
    private readonly ILogger<GamesService> _logger;

    public GamesService(IGameDataAccess gameDataAccess, ILogger<GamesService> logger)
    {
        _gameDataAccess = gameDataAccess;
        _logger = logger;
    }
    public async Task<IEnumerable<GameDTO>> GetAllGames()
    {
        try
        {
            var games = await _gameDataAccess.GetAllGames();
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all games");
            throw;
        }
    }
}