using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WAM.Domain.Bases;

namespace WAM.Domain.UnitTests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeValueObject : ValueObject
    {
        public Int32? TestNullableInt { get; set; }

        public String TestString { get; set; }

        protected override IEnumerable<Object> GetEqualityComponents()
        {
            yield return this.TestNullableInt;
            yield return this.TestString;
        }
    }
}