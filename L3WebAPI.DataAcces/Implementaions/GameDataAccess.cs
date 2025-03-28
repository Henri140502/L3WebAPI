using L3WebAPI.Common.Dao;
using L3WebAPI.Common.Request;
using L3WebAPI.DataAcces.Interfaces;

namespace L3WebAPI.DataAcces.Implementaions;

public class GameDataAccess : IGameDataAccess
{
    private static List<GameDAO> games =
    [
        new GameDAO
        {
            AppId = Guid.NewGuid(),
            Name = "Portal 2",
            Prices =
            [
                new PriceDAO
                {
                    valeur = 19.99M,
                    currency = Currency.USD,
                }
            ]
        },
        new GameDAO
        {
            AppId = Guid.NewGuid(),
            Name = "Half-Life 2",
            Prices =
            [
                new PriceDAO
                {
                    valeur = 14.99M,
                    currency = Currency.EUR,
                },
                new PriceDAO
                {
                    valeur = 15.99M,
                    currency = Currency.USD,
                }
            ]
        }
    ];

    public async Task<IEnumerable<GameDAO>> GetAllGames()
    {
        return games;
    }

    public async Task<GameDAO?> GetGameById(Guid appId)
    {
        return games.FirstOrDefault(g => g.AppId == appId);
    }

    public async Task CreateGame(GameDAO game) {
        games.Add(game);
    }

    public async Task<IEnumerable<GameDAO>?> GetSearchName(string name)
    {
        return games.Where(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}