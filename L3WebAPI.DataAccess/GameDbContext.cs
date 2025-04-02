using L3WebAPI.Common.Dao;
using Microsoft.EntityFrameworkCore;

namespace L3WebAPI.DataAccess {
	public class GameDbContext : DbContext {
		public DbSet<GameDAO> Games { get; set; }
		// public DbSet<UserDAO> Users { get; set; }

		public GameDbContext(DbContextOptions<GameDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder builder) {
			var gameDaoBuilder = builder.Entity<GameDAO>();

			gameDaoBuilder.Property(x => x.AppId)
				.HasColumnName("app_id")
				.HasColumnType("uuid");

			// Primary Key
			gameDaoBuilder.HasKey(x => x.AppId);

			gameDaoBuilder.Property(x => x.Name)
				.HasColumnName("name")
				.HasMaxLength(1000)
				.IsUnicode(true);

			// gameDaoBuilder.HasIndex(x => x.Name).IsUnique();

			var priceDaoBuilder = builder.Entity<PriceDAO>();

			priceDaoBuilder.Property(x => x.Valeur)
				.HasColumnName("valeur")
				.HasPrecision(5, 2);

			priceDaoBuilder.Property(x => x.Currency);

			priceDaoBuilder.HasKey(x => new { x.GameId, x.Currency });

			// Foreign Key
			//gameDaoBuilder.HasMany(x => x.Prices);
			//gameDaoBuilder.HasMany(x => x.Prices).WithOne(x => x.Game);
			//gameDaoBuilder.OwnsMany(x => x.Prices);

			gameDaoBuilder
				.HasMany(x => x.Prices)
				.WithOne(x => x.Game)
				.HasForeignKey(x => x.GameId);


		}
	}
}
