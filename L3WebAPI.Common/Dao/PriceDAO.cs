namespace L3WebAPI.Common.Dao {
	public class PriceDAO {
		public decimal Valeur { get; set; }
		public Currency Currency { get; set; }
		public GameDAO Game { get; set; }
		public Guid GameId { get; set; }
	}
}
