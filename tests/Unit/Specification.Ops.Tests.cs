using System;
using Xunit;

namespace Byndyusoft.Data.Relational.Specifications.Tests.Unit
{
    public partial class SpecificationTests
    {
        [Fact]
        public void Ops_ILike()
        {
            // act
            var spec = Specification.Ops.ILike("name", "value");

            // assert
            Assert.Equal("name ILIKE @name", spec.Sql);
            var parms = spec.Params as dynamic;
            Assert.Equal(parms!.name, "%value%");
        }

        [Theory]
        [InlineData(null)]
        public void Ops_ILike_NullColumn_ThrowsException(string column)
        {
            // act
            var exception = Assert.Throws<ArgumentNullException>(() => Specification.Ops.ILike(column, "value"));

            // assert
            Assert.Equal("column", exception.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Ops_ILike_InvalidColumn_ThrowsException(string column)
        {
            // act
            var exception = Assert.Throws<ArgumentException>(() => Specification.Ops.ILike(column, "value"));

            // assert
            Assert.Equal("column", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        public void Ops_ILike_NullValue_ThrowsException(string value)
        {
            // act
            var exception = Assert.Throws<ArgumentNullException>(() => Specification.Ops.ILike("column", value));

            // assert
            Assert.Equal("value", exception.ParamName);
        }

        [Fact]
        public void Ops_Eq()
        {
            // act
            var spec = Specification.Ops.Eq("id", "value");

            // assert
            Assert.Equal("id = @id", spec.Sql);
            var parms = spec.Params as dynamic;
            Assert.Equal(parms!.id, "value");
        }

        [Theory]
        [InlineData(null)]
        public void Ops_Eq_NullColumn_ThrowsException(string column)
        {
            // act
            var exception = Assert.Throws<ArgumentNullException>(() => Specification.Ops.Eq(column, "value"));

            // assert
            Assert.Equal("column", exception.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Ops_Eq_InvalidColumn_ThrowsException(string column)
        {
            // act
            var exception = Assert.Throws<ArgumentException>(() => Specification.Ops.Eq(column, "value"));

            // assert
            Assert.Equal("column", exception.ParamName);
        }


        [Fact]
        public void Ops_IsNull()
        {
            // act
            var spec = Specification.Ops.IsNull("id");

            // assert
            Assert.Equal("id IS NULL", spec.Sql);
        }
    }
}