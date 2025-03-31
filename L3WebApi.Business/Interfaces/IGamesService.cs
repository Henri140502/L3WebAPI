using L3WebAPI.Common.Dto;
using L3WebAPI.Common.Request;

namespace L3WebApi.Business.Interfaces {
	public interface IGamesService {
		Task<IEnumerable<GameDTO>> GetAllGames();
		Task<GameDTO?> GetGameById(Guid id);
		Task CreateGame(CreateGameRequest game);
		Task<IEnumerable<GameDTO>> SearchByName(string name);
	}
}
