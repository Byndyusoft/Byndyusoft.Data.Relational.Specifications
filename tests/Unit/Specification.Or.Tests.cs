using System;
using Xunit;

namespace Byndyusoft.Data.Relational.Specifications.Tests.Unit
{
    public partial class SpecificationTests
    {
        [Fact]
        public void Or()
        {
            // arrange
            var id = 10;
            var name = "name";
            var left = Specification.Create("id = @id", new { id });
            var right = Specification.Create("name = @name", new { name });

            // act
            var or = left.Or(right);

            // assert
            Assert.Equal("(id = @id) OR (name = @name)", or.Sql);
            var param = ((dynamic)or.Params)!;
            Assert.Equal(10, param!.id);
            Assert.Equal(name, param!.name);
        }

        [Fact]
        public void Or_WithNullSpecification_ThrowsException()
        {
            // arrange
            var left = Specification.Create("id = @id", new { id = 10 });

            // act
            var exception = Assert.Throws<ArgumentNullException>(() => left.Or(null!));

            // assert
            Assert.Equal("right", exception.ParamName);
        }

        [Fact]
        public void Or_TrueWithAnyNonFalse_ReturnsTrue()
        {
            // arrange
            var tru = Specification.True;
            var spec = Specification.Create("1=1");

            // act
            var or = tru.Or(spec);

            // assert
            Assert.Equal(tru, or);
        }

        [Fact]
        public void Or_FalseWithAnyOne_ReturnsTheAnyOne()
        {
            // arrange
            var fals = Specification.False;
            var spec = Specification.Create("1=1");

            // act
            var or = fals.Or(spec);

            // assert
            Assert.Equal(spec, or);
        }

        [Fact]
        public void Or_TrueWithFalse_ReturnsTrue()
        {
            // arrange
            var tru = Specification.True;
            var fals = Specification.False;

            // act
            var or = tru.Or(fals);

            // assert
            Assert.Equal(tru, or);
        }

        [Fact]
        public void Or_FalseWithTrue_ReturnsTrue()
        {
            // arrange
            var tru = Specification.True;
            var fals = Specification.False;

            // act
            var or = fals.Or(tru);

            // assert
            Assert.Equal(tru, or);
        }

        [Fact]
        public void Or_EmptyWithAnyOne_ReturnsTheAnyOne()
        {
            // arrange
            var empty = Specification.Empty;
            var spec = Specification.Create("1=1");

            // act
            var or = empty.Or(spec);

            // assert
            Assert.Equal(spec, or);
        }

        [Fact]
        public void BitwiseOr_Test()
        {
            // arrange
            var id = 10;
            var name = "name";
            var left = Specification.Create("id = @id", new { id });
            var right = Specification.Create("name = @name", new { name });

            // act
            var or = left | right;

            // assert
            Assert.Equal("(id = @id) OR (name = @name)", or.Sql);
            var param = ((dynamic)or.Params)!;
            Assert.Equal(10, param!.id);
            Assert.Equal(name, param!.name);
        }

        [Fact]
        public void BitwiseOr_WithNullSpecification_ThrowsException()
        {
            // arrange
            var spec = Specification.Create("id = 10");

            // act
            {
                var exception = Assert.Throws<ArgumentNullException>(() => spec | null);
                Assert.Equal("right", exception.ParamName);
            }

            {
                var exception = Assert.Throws<ArgumentNullException>(() => null | spec);
                Assert.Equal("left", exception.ParamName);
            }
        }

        [Fact]
        public void Or_WithSqlAndParams()
        {
            // arrange
            var id = 10;
            var name = "name";
            var left = Specification.Create("id = @id", new { id });

            // act
            var or = left.Or("name = @name", new { name });

            // assert
            Assert.Equal("(id = @id) OR (name = @name)", or.Sql);
            var param = ((dynamic)or.Params)!;
            Assert.Equal(10, param!.id);
            Assert.Equal(name, param!.name);
        }

        [Theory]
        [InlineData(null)]
        public void Or_WithSqlAndParams_NullSql_ThrowsException(string sql)
        {
            // arrange
            var spec = Specification.Create("id = 10");

            // act
            var exception = Assert.Throws<ArgumentNullException>(() => spec.Or(sql, new {name = "name"}));

            // assert
            Assert.Equal("sql", exception.ParamName);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Or_WithSqlAndParams_InvalidSql_ThrowsException(string sql)
        {
            // arrange
            var spec = Specification.Create("id = 10");

            // act
            var exception = Assert.Throws<ArgumentException>(() => spec.Or(sql, new {name = "name"}));

            // assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public void Or_WithMultipleAndSpecifications_CombinesInnerSpecifications()
        {
            // arrange
            var spec1 = Specification.Create("1");
            var spec2 = Specification.Create("2");
            var spec3 = Specification.Create("3");
            var spec4 = Specification.Create("4");

            // act
            var result = Specification.Or(spec1, spec2).Or(Specification.Or(spec3, spec4));

            // assert
            var expected = Specification.Or(spec1, spec2, spec3, spec4);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Or_WithSingleAndSpecification_AddOtherOnes()
        {
            // arrange
            var spec1 = Specification.Create("1");
            var spec2 = Specification.Create("2");
            var spec3 = Specification.Create("3");

            // act
            var result = Specification.Or(spec1, spec2).Or(spec3);

            // assert
            var expected = Specification.Or(spec1, spec2, spec3);
            Assert.Equal(expected, result);
        }
    }
}