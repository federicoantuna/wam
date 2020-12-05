using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Curseforge = WAM.Application.Common.Models.Curseforge;

namespace WAM.Application.Common.Interfaces
{
    /// <summary>
    /// Defines a contract for communication with the Curseforge API.
    /// </summary>
    public interface ICurseforgeService
    {
        /// <summary>
        /// Sends a POST request to the Curseforge API with the external IDs as an asynchronous operation.
        /// </summary>
        /// <param name="externalIds">The IDs on Curseforge for the Addons.</param>
        /// <returns>The task object representing the asynchronous operation. The <see cref="Task"/> property on the task object returns a <see cref="Curseforge.AddonDto"/> list abstraction containing the found resources.</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        Task<IEnumerable<Curseforge.AddonDto>> GetAddonsAsync(IEnumerable<Int32> externalIds);
        
        /// <summary>
        /// Sends a GET request to the Curseforge API with a search filter as an asynchronous operation.
        /// </summary>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns>The task object representing the asynchronous operation. The <see cref="Task"/> property on the task object returns a <see cref="Curseforge.AddonDto"/> list abstraction containing the found resources.</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        Task<IEnumerable<Curseforge.AddonDto>> SearchAddonsAsync(String searchFilter);
    }
}