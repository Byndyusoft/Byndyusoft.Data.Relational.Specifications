using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational.Specifications.Example
{
    public static class Program
    {
        public static async Task Main()
        {
            var file = "test.db";

            File.Delete(file);

            await File.Create(file).DisposeAsync();

            var serviceProvider =
                new ServiceCollection()
                    .AddRelationalDb(SqliteFactory.Instance, $"data source={file}")
                    .BuildServiceProvider();

            await InitDbAsync(serviceProvider);

            await SpecificationExampleAsync(serviceProvider);
        }

        private static async Task InitDbAsync(IServiceProvider serviceProvider)
        {
            var sessionFactory = serviceProvider.GetRequiredService<IDbSessionFactory>();
            await using var session = await sessionFactory.CreateCommittableSessionAsync();
            await session.ExecuteAsync("CREATE TABLE test (id PRIMARY KEY ASC, name TEXT, birthday TEXT, city TEXT)");

            await session.ExecuteAsync(
                "INSERT INTO test (id, name, birthday, city) VALUES (1, 'name1', '2021-05-08', 'Chelyabinsk');");
            await session.ExecuteAsync(
                "INSERT INTO test (id, name, birthday, city) VALUES (2, 'name2', '1998-01-02', 'Chelyabinsk');");
            await session.ExecuteAsync(
                "INSERT INTO test (id, name, birthday, city) VALUES (3, 'name3', '2011-09-13', 'Moscow');");

            await session.CommitAsync();
        }

        private static async Task SpecificationExampleAsync(IServiceProvider serviceProvider)
        {
            Console.WriteLine("=== Specification Example ===");

            var sessionFactory = serviceProvider.GetRequiredService<IDbSessionFactory>();
            await using var session = await sessionFactory.CreateSessionAsync();

            var specification = Specification.Empty;

            specification &=
                Specification.Or(
                    Specification.Create("name=@name", new { name = "name1" }),
                    Specification.Ops.Eq("city", "Chelyabinsk"),
                    Specification.Create("name='name2'").Not()
                );
            specification &= Specification.Create("birthday >= @from", new { from = new DateTime(1900, 1, 1) });
            specification =
                specification.And(Specification.Create("birthday <= @to", new { to = new DateTime(2010, 1, 1) }));

            var sql = $"SELECT id, name, birthday, city FROM test WHERE {specification}";

            Console.WriteLine("=== SQL:");
            Console.WriteLine(sql);
            Console.WriteLine("=== PARAMS:");
            Console.WriteLine(JsonConvert.SerializeObject(specification.Params));

            Console.WriteLine("=== ROWS:");

            var result = session.Query(sql, specification.Params);
            await foreach (var row in result) Console.WriteLine(JsonConvert.SerializeObject(row));
        }
    }
}