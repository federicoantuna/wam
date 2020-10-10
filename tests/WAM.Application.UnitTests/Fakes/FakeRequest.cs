using MediatR;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace WAM.Application.UnitTests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeRequest : IRequest<String>
    {
        public Int32 Id { get; set; }
        public String SomeText { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class FakeRequestHandler : IRequestHandler<FakeRequest, String>
    {
        public async Task<String> Handle(FakeRequest request, CancellationToken cancellationToken) => await Task.FromResult("Fake Request Handled!");
    }
}