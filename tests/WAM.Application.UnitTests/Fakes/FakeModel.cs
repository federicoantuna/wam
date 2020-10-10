using System;
using System.Diagnostics.CodeAnalysis;
using WAM.Application.Common.Mappings;

namespace WAM.Application.UnitTests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeModel : IMapFrom<FakeEntity>
    {
        public Guid Id { get; set; }
        public String TestText { get; set; }
        public Boolean TestFlag { get; set; }
        public Char TestChar { get; set; }
    }
}