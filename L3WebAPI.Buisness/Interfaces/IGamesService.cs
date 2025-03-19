using L3WebAPI.Common.Dto;
namespace L3WebAPI.Buisness.Interfaces;

public interface IGamesService
{
    Task<IEnumerable<GameDTO>> GetAllGames();
}