using L3WebAPI.Buisness.Exceptions;
using L3WebAPI.Buisness.Interfaces;
using L3WebAPI.Common.Dao;
using L3WebAPI.Common.Dto;
using L3WebAPI.Common.Request;
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
            return games.Select(g => g.ToDTO());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all games");
            return [];
        }
    }

    public async Task<GameDTO?> GetGameById(Guid appId)
    {
        try
        {
            var game = await _gameDataAccess.GetGameById(appId);
            return game?.ToDTO();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting game by ID", appId);
            return null;
        }
    }

    public async Task CreateGame(CreateGameRequest game)
    {
        try
        {
            if (string.IsNullOrEmpty(game.Name))
            {
                throw new BuisnessRuleException("Name cannot be null or empty");
            }

            if (game.Name.Length > 1000)
            {
                throw new BuisnessRuleException("Name cannot be longer than 1000 characters");
            }
            if (game.Prices.Count() <= 0)
            {
                throw new BuisnessRuleException("At least one price must be provided");
            }

            _gameDataAccess.CreateGame(new GameDAO
            {
                AppId = Guid.NewGuid(),
                Name = game.Name,
                Prices = game.Prices.Select(p => new PriceDAO {
                    currency = p.currency,
                    valeur = p.valeur,
                })
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating game");
            throw;
        }
    }
}