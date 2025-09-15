using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Server.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
	public class GameDbContext : DbContext
	{
		//public DbSet<PlayerDb> Players { get; set; }

		static readonly ILoggerFactory _logger = LoggerFactory.Create(builder => { builder.AddConsole(); });

		public GameDbContext()
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			ConfigManager.LoadConfig();

			options
				.UseLoggerFactory(_logger)
				.UseSqlServer(ConfigManager.Config.connectionString);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			// AccountDbId에 인덱스 걸어준다
			builder.Entity<PlayerDb>()
				.HasIndex(t => t.PlayerDbId);

			builder.Entity<PlayerDb>()
				.Property(nameof(PlayerDb.CreateDate))
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<RoomDb>()
				.HasIndex(i => i.RoomDbId);

            builder.Entity<RoomDb>()
				.Property(nameof(RoomDb.CreateDate))
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Room ↔ RoomPlayer 1:N
            builder.Entity<RoomPlayerDb>()
                .HasOne(rp => rp.Room)
                .WithMany(r => r.RoomPlayers)
                .HasForeignKey(rp => rp.RoomDbId)
                .OnDelete(DeleteBehavior.Cascade);

            // Player ↔ RoomPlayer 1:1
            builder.Entity<RoomPlayerDb>()
                .HasOne(rp => rp.Player)
                .WithOne(p => p.RoomPlayer)
                .HasForeignKey<RoomPlayerDb>(rp => rp.PlayerDbId)
                .OnDelete(DeleteBehavior.Cascade);

            // 유니크 인덱스: RoomDbId + PlayerDbId
            builder.Entity<RoomPlayerDb>()
                .HasIndex(rp => new { rp.RoomDbId, rp.PlayerDbId })
                .IsUnique();		
		}
	}
}
