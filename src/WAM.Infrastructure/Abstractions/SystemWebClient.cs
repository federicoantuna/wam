using System.Net;
using WAM.Application.Common.Interfaces.Abstractions;

namespace WAM.Infrastructure.Abstractions
{
    /// <inheritdoc/>
    /// <summary>
    /// An abstraction from <see cref="WebClient"/>.
    /// </summary>
    public class SystemWebClient : WebClient, IWebClient
    {
    }
}