using WAM.Application.Common.Interfaces.Abstractions;

namespace WAM.Application.Common.Interfaces
{
    /// <summary>
    /// Defines a contract for the System Web Client Factory.
    /// </summary>
    public interface ISystemWebClientFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IWebClient"/> type.
        /// </summary>
        /// <returns>A new instance of the <see cref="IWebClient"/> type.</returns>
        IWebClient Create();
    }
}