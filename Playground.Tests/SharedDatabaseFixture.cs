using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Playground.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Tests
{
    public class SharedDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _databaseCreated;
        public DbConnection Connection { get; }

        bool isSqlServer = false;

        public SharedDatabaseFixture()
        {
            if (isSqlServer)
            {
                Connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=PlaygroundUnitTestDb;Trusted_Connection=True");
                Seed();
                Connection.Open();
            }
            else
            {
                Connection = new SqliteConnection("Filename=:memory:");
                Connection.Open();
                Seed();
            }
        }

        public PlayDbContext CreateContext(DbTransaction transaction = null)
        {
            PlayDbContext context;

            if (isSqlServer)
            {
                context = new PlayDbContext(new DbContextOptionsBuilder<PlayDbContext>().UseSqlServer(Connection).Options);
            }
            else
            {
                context = new PlayDbContext(new DbContextOptionsBuilder<PlayDbContext>().UseSqlite(Connection).Options);
            }

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseCreated)
                {
                    using (var context = CreateContext())
                    {
                        PlayDbContextSeed.CreateAndSeedDatabaseAsync(context).Wait();
                    }

                    _databaseCreated = true;
                }
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
