using System.IO;
using Windows.Storage;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace WeatherAppUwp.Models
{
    public class ForecastContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbFile = Path.Combine(ApplicationData.Current.LocalFolder.Path, "forecast.sqlite");
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = dbFile };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

        public DbSet<ForecastDbitem> Items { get; set; }
    }
}