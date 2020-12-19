using Xunit;

namespace Byndyusoft.Data.Relational.Specifications.Tests.Unit
{
    public partial class SpecificationTests
    {
        [Fact]
        public void Not()
        {
            // arrange
            var id = 10;
            var left = Specification.Create("id = @id", new {id});

            // act
            var not = left.Not();

            // assert
            Assert.Equal("NOT (id = @id)", not.Sql);
            var param = ((dynamic) not.Params)!;
            Assert.Equal(10, param!.id);
        }

        [Fact]
        public void Not_ForFalseSpec_ReturnsTrueOne()
        {
            // arrange
            var fals = Specification.False;

            // act
            var not = fals.Not();

            // assert
            Assert.Equal(Specification.True, not);
        }

        [Fact]
        public void Not_ForTrueSpec_ReturnsFalseOne()
        {
            // arrange
            var tru = Specification.True;

            // act
            var not = tru.Not();

            // assert
            Assert.Equal(Specification.False, not);
        }

        [Fact]
        public void Not_ForEmptySpec_ReturnsItself()
        {
            // arrange
            var empty = Specification.Empty;

            // act
            var not = empty.Not();

            // assert
            Assert.Equal(empty, not);
        }

        [Fact]
        public void Not_OfNot()
        {
            // arrange
            var spec = Specification.Create("id = 10");

            // act
            var notOfNot = spec.Not().Not();

            // assert
            Assert.Equal(spec, notOfNot);
        }

        [Fact]
        public void BitwiseNot()
        {
            // arrange
            var id = 10;
            var left = Specification.Create("id = @id", new { id });

            // act
            var not = !left;

            // assert
            Assert.Equal("NOT (id = @id)", not.Sql);
            var param = ((dynamic)not.Params)!;
            Assert.Equal(10, param!.id);
        }
    }
}