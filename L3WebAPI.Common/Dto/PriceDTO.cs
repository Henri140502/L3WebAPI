using L3WebAPI.Common.Dao;

namespace L3WebAPI.Common.Dto {
	public class PriceDTO {
		public decimal Valeur { get; set; }
		public Currency Currency { get; set; }
	}

	public static class PriceDTOExtensions {
		public static PriceDTO ToDto(this PriceDAO priceDAO) {
			return new PriceDTO {
				Valeur = priceDAO.Valeur,
				Currency = priceDAO.Currency,
			};
		}
	}
}
