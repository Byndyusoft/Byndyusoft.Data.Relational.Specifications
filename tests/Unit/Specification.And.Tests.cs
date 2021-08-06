using System;
using Xunit;

namespace Byndyusoft.Data.Relational.Specifications.Tests.Unit
{
    public partial class SpecificationTests
    {
        [Fact]
        public void And()
        {
            // arrange
            var id = 10;
            var name = "name";
            var left = Specification.Create("id = @id", new {id});
            var right = Specification.Create("name = @name", new {name});

            // act
            var and = left.And(right);

            // assert
            Assert.Equal("(id = @id) AND (name = @name)", and.Sql);
            var param = ((dynamic) and.Params)!;
            Assert.Equal(10, param!.id);
            Assert.Equal(name, param!.name);
        }

        [Fact]
        public void And_WithNullSpecification_ThrowsException()
        {
            // arrange
            var left = Specification.Create("id = @id", new {id = 10});

            // act
            var exception = Assert.Throws<ArgumentNullException>(() => left.And(null!));

            // assert
            Assert.Equal("right", exception.ParamName);
        }

        [Fact]
        public void And_TrueWithAnyNonFalse_ReturnsTheAnyOne()
        {
            // arrange
            var tru = Specification.True;
            var spec = Specification.Create("1=1");

            // act
            var and = tru.And(spec);

            // assert
            Assert.Equal(spec, and);
        }

        [Fact]
        public void And_FalseWithAnyOne_ReturnsTheAnyOne()
        {
            // arrange
            var fals = Specification.False;
            var spec = Specification.Create("1=1");

            // act
            var and = fals.And(spec);

            // assert
            Assert.Equal(fals, and);
        }

        [Fact]
        public void And_TrueWithFalse_ReturnsFalse()
        {
            // arrange
            var tru = Specification.True;
            var fals = Specification.False;

            // act
            var and = tru.And(fals);

            // assert
            Assert.Equal(fals, and);
        }

        [Fact]
        public void And_TrueWithEmpty_ReturnsTrue()
        {
            // arrange
            var tru = Specification.True;
            var empty = Specification.Empty;

            // act
            var and = tru.And(empty);

            // assert
            Assert.Equal(tru, and);
        }

        [Fact]
        public void And_FalseWithTrue_ReturnsFalse()
        {
            // arrange
            var tru = Specification.True;
            var fals = Specification.False;

            // act
            var and = fals.And(tru);

            // assert
            Assert.Equal(fals, and);
        }

        [Fact]
        public void And_FalseWithEmpty_ReturnsTrue()
        {
            // arrange
            var fals = Specification.False;
            var empty = Specification.Empty;

            // act
            var and = fals.And(empty);

            // assert
            Assert.Equal(fals, and);
        }

        [Fact]
        public void And_EmptyWithAnyOne_ReturnsTheAnyOne()
        {
            // arrange
            var empty = Specification.Empty;
            var spec = Specification.Create("1=1");

            // act
            var and = empty.And(spec);

            // assert
            Assert.Equal(spec, and);
        }

        [Fact]
        public void BitwiseAnd()
        {
            // arrange
            var id = 10;
            var name = "name";
            var left = Specification.Create("id = @id", new {id});
            var right = Specification.Create("name = @name", new {name});

            // act
            var and = left & right;

            // assert
            Assert.Equal("(id = @id) AND (name = @name)", and.Sql);
            var param = ((dynamic) and.Params)!;
            Assert.Equal(10, param!.id);
            Assert.Equal(name, param!.name);
        }

        [Fact]
        public void BitwiseAnd_WithNullSpecification_ThrowsException()
        {
            // arrange
            var spec = Specification.Create("id = 10");

            // act
            {
                var exception = Assert.Throws<ArgumentNullException>(() => spec & null);
                Assert.Equal("right", exception.ParamName);
            }

            {
                var exception = Assert.Throws<ArgumentNullException>(() => null & spec);
                Assert.Equal("left", exception.ParamName);
            }
        }

        [Fact]
        public void And_WithSqlAndParams()
        {
            // arrange
            var id = 10;
            var name = "name";
            var left = Specification.Create("id = @id", new {id});

            // act
            var and = left.And("name = @name", new {name});

            // assert
            Assert.Equal("(id = @id) AND (name = @name)", and.Sql);
            var param = ((dynamic) and.Params)!;
            Assert.Equal(10, param!.id);
            Assert.Equal(name, param!.name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void And_WithSqlAndParams_NullSql_ThrowsException(string sql)
        {
            // arrange
            var spec = Specification.Create("id = 10");

            // act
            var exception = Assert.Throws<ArgumentNullException>(() => spec.And(sql, new {name = "name"}));

            // assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public void And_WithMultipleAndSpecifications_CombinesInnerSpecifications()
        {
            // arrange
            var spec1 = Specification.Create("1");
            var spec2 = Specification.Create("2");
            var spec3 = Specification.Create("3");
            var spec4 = Specification.Create("4");

            // act
            var result = Specification.And(spec1, spec2).And(Specification.And(spec3, spec4));

            // assert
            var expected = Specification.And(spec1, spec2, spec3, spec4);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void And_WithSingleAndSpecification_AddOtherOnes()
        {
            // arrange
            var spec1 = Specification.Create("1");
            var spec2 = Specification.Create("2");
            var spec3 = Specification.Create("3");

            // act
            var result = Specification.And(spec1, spec2).And(spec3);

            // assert
            var expected = Specification.And(spec1, spec2, spec3);
            Assert.Equal(expected, result);
        }
    }
}