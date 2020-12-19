using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Byndyusoft.Data.Relational.Specifications.Tests.Unit
{
    public partial class SpecificationTests
    {
        [Fact]
        public void Create_WithSql()
        {
            // arrange
            var sql = "SELECT * FROM table";

            // act
            var specification = Specification.Create(sql);

            // assert
            Assert.Equal(sql, specification.Sql);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_NullSql_ThrowsException(string sql)
        {
            // act
            var exception = Assert.Throws<ArgumentNullException>(() => Specification.Create(sql));

            // assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public void Create_WithSqlAndParameters()
        {
            // arrange
            var sql = "id = @id";
            var param = new {id = 10};

            // act
            var specification = Specification.Create(sql, param);

            // assert
            Assert.Equal(sql, specification.Sql);
            Assert.Equal(10, ((dynamic) specification.Params)!.id);
        }

        [Fact]
        public void CreateFormat()
        {
            // arrange
            var id = 10;

            // act
            var specification = Specification.CreateFormat($"id = {id}");

            // assert
            var param = (IDictionary<string, object>) specification.Params;
            var expectedSql = $"id = {param!.Keys.Single()}";
            Assert.Equal(expectedSql, specification.Sql);
            Assert.Equal(id, param.Values.Single());
        }

        [Fact]
        public void CreateFormat_NoParams()
        {
            // act
            var specification = Specification.CreateFormat($"id = 10");

            // assert
            Assert.Equal("id = 10", specification.Sql);
            Assert.Null(specification.Params);
        }

        [Fact]
        public void CreateFormat_NullString()
        {
            // act
            var exception = Assert.Throws<ArgumentNullException>(() => Specification.CreateFormat(null!));

            // assert
            Assert.Equal("formattableString", exception.ParamName);
        }

        [Fact]
        public void Empty()
        {
            // act
            var specification = Specification.Empty;

            // assert
            Assert.Equal("", specification.Sql);
            Assert.Null(specification.Params);
        }

        [Fact]
        public void True()
        {
            // act
            var specification = Specification.True;

            // assert
            Assert.Equal("", specification.Sql);
            Assert.Null(specification.Params);
        }

        [Fact]
        public void False()
        {
            // act
            var specification = Specification.False;

            // assert
            Assert.Equal("1<>1", specification.Sql);
            Assert.Null(specification.Params);
        }

        [Fact]
        public void And_Static()
        {
            // arrange
            var id = 10;
            var name = "name";
            var left = Specification.Create("id = @id", new { id });
            var right = Specification.Create("name = @name", new { name });

            // act
            var and = Specification.And(left, right);

            // assert
            Assert.Equal("(id = @id) AND (name = @name)", and.Sql);
            var param = ((dynamic)and.Params)!;
            Assert.Equal(10, param!.id);
            Assert.Equal(name, param!.name);
        }

        [Fact]
        public void And_Static_Nullarray_ThrowsException()
        {
            // act
            var exception = Assert.Throws<ArgumentNullException>(() => Specification.And(null!));
            
            // assert
            Assert.Equal("specifications", exception.ParamName);
        }

        [Fact]
        public void Or_Static()
        {
            // arrange
            var id = 10;
            var name = "name";
            var left = Specification.Create("id = @id", new { id });
            var right = Specification.Create("name = @name", new { name });

            // act
            var or = Specification.Or(left, right);

            // assert
            Assert.Equal("(id = @id) OR (name = @name)", or.Sql);
            var param = ((dynamic)or.Params)!;
            Assert.Equal(10, param!.id);
            Assert.Equal(name, param!.name);
        }

        [Fact]
        public void Or_Static_Nullarray_ThrowsException()
        {
            // act
            var exception = Assert.Throws<ArgumentNullException>(() => Specification.Or(null!));

            // assert
            Assert.Equal("specifications", exception.ParamName);
        }
    }
}