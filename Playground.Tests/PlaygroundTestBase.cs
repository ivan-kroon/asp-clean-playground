using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Playground.Core.Entities;
using Playground.Core.Interfaces;
using Playground.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Tests
{
    public class PlaygroundTestBase : IDisposable
    {
        DbConnection connection;
        protected PlayDbContext testDbContext;

        protected PlaygroundTestBase()
        {
            connection = CreateInMemoryDatabase();

            var options = new DbContextOptionsBuilder<PlayDbContext>()
                .UseSqlite(connection)
                .Options;

            testDbContext = new PlayDbContext(options);

            PlayDbContextSeed.CreateAndSeedDatabaseAsync(testDbContext).Wait();
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            //in memory database exists while the connection is open
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            return connection;
        }

        public void Dispose()
        {
            //closing connection database is removed from memory
            connection.Dispose();
        }
    }
}
