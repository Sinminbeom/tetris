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
		public DbSet<PlayerDb> Players { get; set; }

		static readonly ILoggerFactory _logger = LoggerFactory.Create(builder => { builder.AddConsole(); });

		public GameDbContext()
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			ConfigManager.LoadConfig();

            string connStr = ConfigManager.Config.connectionString;

            string key = Environment.GetEnvironmentVariable("ENCRYPTION_KEY");
            if (string.IsNullOrEmpty(key))
            {
                Console.WriteLine("ENCRYPTION_KEY 환경 변수가 설정되지 않았습니다.");
                return;
            }

            connStr = DecryptCredentials(connStr, key);

            //options
            //	.UseLoggerFactory(_logger)
            //	.UseSqlServer(ConfigManager.Config.connectionString);

            options
				.UseLoggerFactory(_logger)
				.UseMySql(connStr, ServerVersion.AutoDetect(connStr));

        }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			// AccountDbId에 인덱스 걸어준다
			builder.Entity<PlayerDb>()
				.HasIndex(t => t.PlayerDbId);

			builder.Entity<PlayerDb>()
				.Property(nameof(PlayerDb.CreateDate))
				.HasDefaultValueSql("CURRENT_TIMESTAMP");	
		}

        static string DecryptCredentials(string connStr, string key)
        {
            connStr = DecryptPart(connStr, "Uid=", key);
            connStr = DecryptPart(connStr, "Pwd=", key);
            return connStr;
        }

        static string DecryptPart(string connStr, string token, string key)
        {
            int start = connStr.IndexOf(token);
            if (start == -1)
                return connStr;

            start += token.Length;
            int end = connStr.IndexOf(';', start);
            if (end == -1)
                end = connStr.Length;

            string encryptedValue = connStr.Substring(start, end - start);
            string decryptedValue = AesEncryption.Decrypt(encryptedValue, key);

            return connStr.Replace(encryptedValue, decryptedValue);
        }
    }
}
