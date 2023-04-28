using Dapper;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Byndyusoft.Data.Relational.Specifications.Tests.Functional
{
    public class DiagnosticListenerFunctionalTests : IAsyncLifetime
    {
        private readonly string _connectionString = "Data Source=queries.db";

        public async Task InitializeAsync()
        {
            File.Delete("queries.db");

            await using var connection = new SQLiteConnection(_connectionString);

            await connection.ExecuteAsync("CREATE TABLE test (id INT, name TEXT)");
            await connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'name1')");
            await connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (2, 'name2')");
            await connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (3, 'name3')");

            await connection.CloseAsync();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task Test()
        {
            // arrange
            var spec = Specification.Ops.Eq("id", 1) & Specification.Create("name like @name", new { name = "%name%" });

            var x = (IDictionary<string, object>)spec.Params;

            // act
            await using var connection = new SQLiteConnection(_connectionString);
            var result = (await connection.QueryAsync($"SELECT id, name FROM test WHERE {spec.Sql}", spec.Params))
                .ToArray();

            // assert
            var item = Assert.Single(result);
            Assert.Equal(1, item!.id);
            Assert.Equal("name1", item!.name);
        }
    }
}