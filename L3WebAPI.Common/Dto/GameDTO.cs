using L3WebAPI.Common.Dao;

namespace L3WebAPI.Common.Dto {
	public class GameDTO {
		public Guid AppId { get; set; }
		public string Name { get; set; } = null!;
		public IEnumerable<PriceDTO> Prices { get; set; } = null!;
		public Uri LogoUri { get; set; } = null!;
	}

	public static class GameDTOExtensions {
		public static GameDTO ToDto(this GameDAO gameDAO) {
			/* var gameDto = new GameDTO();
			 * gameDto.AppId = gameDAO.AppId;
			   gameDto.Name = gameDAO.Name;
			   gameDto.Prices = gameDAO.Prices.Select(price => price.ToDto());
			   return gameDto;
			 */

			return new GameDTO {
				AppId = gameDAO.AppId,
				Name = gameDAO.Name,
				Prices = gameDAO.Prices.Select(price => price.ToDto())
			};
		}
	}

	// GameDTOExtensions.ToDto(myGameDao);
	// myGameDao.ToDto();
}
