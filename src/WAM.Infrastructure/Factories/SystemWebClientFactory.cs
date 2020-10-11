using WAM.Application.Common.Interfaces;
using WAM.Application.Common.Interfaces.Abstractions;
using WAM.Infrastructure.Abstractions;

namespace WAM.Infrastructure.Factories
{
    /// <inheritdoc/>
    /// <summary>
    /// Factory for <see cref="IWebClient"/> type.
    /// </summary>
    public class SystemWebClientFactory : ISystemWebClientFactory
    {
        public IWebClient Create() => new SystemWebClient();
    }
}