using System.Threading.Tasks;
using WAM.Domain.Bases;

namespace WAM.Application.Common.Interfaces
{
    /// <summary>
    /// Defines the contract for the Domain Event Service.
    /// </summary>
    public interface IDomainEventService
    {
        /// <summary>
        /// Publishes a Domain Event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task Publish(DomainEvent domainEvent);
    }
}