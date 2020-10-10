﻿using System.Diagnostics.CodeAnalysis;
using WAM.Domain.Bases;
using WAM.Domain.Services;

namespace WAM.Domain.UnitTests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeDomainEvent : DomainEvent
    {
        public FakeDomainEvent(ITimeService timeService)
            : base(timeService)
        {
        }
    }
}