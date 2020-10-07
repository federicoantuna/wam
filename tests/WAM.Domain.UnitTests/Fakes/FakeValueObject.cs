using System;
using System.Collections.Generic;
using WAM.Domain.Bases;

namespace WAM.Domain.UnitTests.Fakes
{
    public class FakeValueObject : ValueObject
    {
        public Int32 Differentiator { get; set; }

        protected override IEnumerable<Object> GetEqualityComponents()
        {
            yield return this.Differentiator;
        }
    }
}