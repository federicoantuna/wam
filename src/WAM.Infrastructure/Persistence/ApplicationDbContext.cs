using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WAM.Application.Common.Interfaces;
using WAM.Domain.Bases;
using WAM.Domain.Entities;
using WAM.Domain.Services;

namespace WAM.Infrastructure.Persistence
{
    /// <inheritdoc/>
    /// <summary>
    /// Represents a session with the database and can be used to query and save instances of the entities.
    /// </summary>
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDomainEventService _domainEventService;
        private readonly ITimeService _timeService;

        public ApplicationDbContext(DbContextOptions dbContextOptions,
            IDomainEventService domainEventService,
            ITimeService timeService)
            : base(dbContextOptions)
        {
            this._domainEventService = domainEventService;
            this._timeService = timeService;
        }

        public DbSet<Addon> Addons { get; set; }

        public DbSet<Package> Packages { get; set; }

        public override async Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in this.ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.SetLastModifiedDate(this._timeService.UtcNow);
                        break;
                    case EntityState.Added:
                        entry.Entity.SetCreatedAtDate(this._timeService.UtcNow);
                        break;
                    default:
                        break;
                }
            }

            await this.DispatchEvents();

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        private async Task DispatchEvents()
        {
            var domainEvents = this.ChangeTracker.Entries<Entity>()
                .Select(ee => ee.Entity.DomainEvents)
                .SelectMany(de => de)
                .ToArray();

            foreach (var domainEvent in domainEvents)
            {
                await this._domainEventService.Publish(domainEvent);
            }
        }
    }
}