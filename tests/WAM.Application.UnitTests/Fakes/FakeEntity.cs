using System;
using System.Diagnostics.CodeAnalysis;
using WAM.Domain.Bases;

namespace WAM.Application.UnitTests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeEntity : Entity
    {
        public String TestText { get; set; }
        public Boolean TestFlag { get; set; }
        public Char TestChar { get; set; }
    }
}