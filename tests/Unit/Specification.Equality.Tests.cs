using Moq;
using Xunit;

namespace Byndyusoft.Data.Relational.Specifications.Tests.Unit
{
    public partial class SpecificationTests
    {
        [Fact]
        public void Equality_Test()
        {
            // arrange
            var spec = Specification.Create("id = @id", new { id = 1 });
            var same = Specification.Create("id = @id", new { id = 1 });
            var other = Specification.Create("id = @id", new { id = 2 });

            // assert
            Assert.True(spec.Equals(same));
            Assert.False(spec.Equals(other));
        }

        [Fact]
        public void Empty_Equality_Test()
        {
            // arrange
            var spec = Specification.Empty;
            var same = Specification.Empty;
            var other = Specification.True;

            // assert
            Assert.True(spec.Equals(same));
            Assert.False(spec.Equals(other));
        }

        [Fact]
        public void True_Equality_Test()
        {
            // arrange
            var spec = Specification.True;
            var same = Specification.True;
            var other = Specification.False;

            // assert
            Assert.True(spec.Equals(same));
            Assert.False(spec.Equals(other));
        }

        [Fact]
        public void False_Equality_Test()
        {
            // arrange
            var spec = Specification.False;
            var same = Specification.False;
            var other = Specification.True;

            // assert
            Assert.True(spec.Equals(same));
            Assert.False(spec.Equals(other));
        }

        [Fact]
        public void Not_Equality_Test()
        {
            // arrange
            var inner1 = Mock.Of<Specification>();
            var inner2 = Mock.Of<Specification>();
            var inner3 = Mock.Of<Specification>();
            Mock.Get(inner1).Setup(x => x.Equals(inner2)).Returns(true);

            var spec = Specification.Not(inner1);
            var same = Specification.Not(inner2);
            var other = Specification.Not(inner3);

            // assert
            Assert.True(spec.Equals(same));
            Assert.False(spec.Equals(other));
        }

        [Fact]
        public void And_Equality_Test()
        {
            // arrange
            var inner1 = Mock.Of<Specification>();
            var inner2 = Mock.Of<Specification>();
            var inner3 = Mock.Of<Specification>();
            Mock.Get(inner1).Setup(x => x.Equals(inner2)).Returns(true);

            var spec = Specification.And(inner1, inner3);
            var same = Specification.And(inner2, inner3);
            var other = Specification.And(inner1, inner2);

            // assert
            Assert.True(spec.Equals(same));
            Assert.False(spec.Equals(other));
        }

        [Fact]
        public void Or_Equality_Test()
        {
            // arrange
            var inner1 = Mock.Of<Specification>();
            var inner2 = Mock.Of<Specification>();
            var inner3 = Mock.Of<Specification>();
            Mock.Get(inner1).Setup(x => x.Equals(inner2)).Returns(true);

            var spec = Specification.Or(inner1, inner3);
            var same = Specification.Or(inner2, inner3);
            var other = Specification.Or(inner1, inner2);

            // assert
            Assert.True(spec.Equals(same));
            Assert.False(spec.Equals(other));
        }
    }
}