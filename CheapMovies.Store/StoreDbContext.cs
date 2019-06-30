using Microsoft.EntityFrameworkCore;

namespace CheapMovies.Store
{
    public class StoreDbContext : DbContext
    {
        private readonly bool isMigration = false;
        public string connectionString = string.Empty;

        public DbSet<Item> Items { get; set; }

        public StoreDbContext()
        {
            this.isMigration = true;
        }

        public StoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this.isMigration)
            {
                var connectionStringBuilder = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = "cheap-movies.db" };
                var connectionString = connectionStringBuilder.ToString();

                optionsBuilder.UseSqlite(connectionString);
            }
        }
    }
}