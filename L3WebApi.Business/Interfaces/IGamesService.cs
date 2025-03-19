using L3WebAPI.Common.Dto;

namespace L3WebApi.Business.Interfaces {
	public interface IGamesService {
		Task<IEnumerable<GameDTO>> GetAllGames();
		Task<GameDTO?> GetGameById(Guid id);
	}
}
