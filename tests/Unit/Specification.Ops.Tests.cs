using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Byndyusoft.Data.Relational.Specifications.Tests.Unit
{
    public partial class SpecificationTests
    {
        [Fact]
        public void Ops_ILike()
        {
            // act
            var spec = Specification.Ops.ILike("column", "value");

            // assert
            var param = (IDictionary<string, object>)spec.Params;
            var expectedSql = $"column ILIKE {param!.Keys.Single()}";
            Assert.Equal(expectedSql, spec.Sql);
            Assert.Equal("%value%", param.Values.Single());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Ops_ILike_NullColumn_ThrowsException(string column)
        {
            // act
            var exception = Assert.Throws<ArgumentNullException>(() => Specification.Ops.ILike(column, "value"));

            // assert
            Assert.Equal("column", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
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
            var spec = Specification.Ops.Eq("column", "value");

            // assert
            var param = (IDictionary<string, object>)spec.Params;
            var expectedSql = $"column = {param!.Keys.Single()}";
            Assert.Equal(expectedSql, spec.Sql);
            Assert.Equal("value", param.Values.Single());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Ops_Eq_NullColumn_ThrowsException(string column)
        {
            // act
            var exception = Assert.Throws<ArgumentNullException>(() => Specification.Ops.Eq(column, "value"));

            // assert
            Assert.Equal("column", exception.ParamName);
        }
    }
}