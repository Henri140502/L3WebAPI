namespace L3WebAPI.Common.Dao {
	public class GameDAO {
		public Guid AppId { get; set; }
		public string Name { get; set; } = null!;
		public ICollection<PriceDAO> Prices { get; set; } = null!;
	}
}
