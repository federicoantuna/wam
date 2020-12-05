using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using WAM.Application.Common.Interfaces;
using WAM.Domain.Entities;
using WAM.Domain.Enums;
using WAM.Domain.Services;
using WAM.Infrastructure.Persistence;
using WAM.Infrastructure.UnitTests.Fakes;
using Xunit;

namespace WAM.Infrastructure.UnitTests.Persistence
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContextTests
    {
        private readonly Mock<IDomainEventService> _domainEventServiceMock;
        private readonly Mock<ITimeService> _timeServiceMock;

        public ApplicationDbContextTests()
        {
            this._domainEventServiceMock = new Mock<IDomainEventService>();
            this._timeServiceMock = new Mock<ITimeService>();
        }

        [Fact]
        public async Task SaveChangesAsync_SetsCreatedAtDatesAndSavesNewEntries()
        {
            // Arrange
            var packageAName = "Test Package A";
            var packageBName = "Test Package B";
            var packageAExternalId = 1;
            var packageBExternalId = 2;
            var packageA = new Package(packageAExternalId, packageAName);
            var packageB = new Package(packageBExternalId, packageBName);

            var now = DateTime.UtcNow;
            _ = this._timeServiceMock.SetupGet(tsm => tsm.UtcNow).Returns(now);

            var sut = this.CreateSut();

            _ = await sut.Packages.AddAsync(packageA);
            _ = await sut.Packages.AddAsync(packageB);

            var expectedResult = 2;

            // Act
            var result = await sut.SaveChangesAsync();

            // Arrange
            Assert.Equal(expectedResult, result);
            Assert.Collection(sut.Packages,
                item => Assert.Equal(now, item.CreatedAt),
                item => Assert.Equal(now, item.CreatedAt));
        }

        [Fact]
        public async Task SaveChangesAsync_SetsLastModifiedDatesAndSavesUpdatedEntries()
        {
            // Arrange
            var updatedVersion = "1.2.3";
            var packageName = "Test Package Name";
            var externalId = 1;
            var name = "Test Addon Name";
            var version = "0.1.2";
            var package = new Package(externalId, packageName);
            var gameVersionFlavor = GameVersionFlavor.Retail;
            var releaseType = ReleaseType.Stable;
            var addon = new Addon(name, version, package, gameVersionFlavor, releaseType);

            var before = DateTime.UtcNow;
            _ = this._timeServiceMock.SetupGet(tsm => tsm.UtcNow).Returns(before);

            var sut = this.CreateSut();

            _ = await sut.Addons.AddAsync(addon);
            _ = await sut.SaveChangesAsync();
            var savedAddon = await sut.Addons.FirstOrDefaultAsync(a => a.Id == addon.Id);
            savedAddon.UpdateVersion(updatedVersion);
            _ = sut.Addons.Update(savedAddon);
            var now = DateTime.UtcNow.AddHours(new Random().Next(8));
            _ = this._timeServiceMock.SetupGet(tsm => tsm.UtcNow).Returns(now);

            var expectedResult = 1;

            // Act
            var result = await sut.SaveChangesAsync();

            // Arrange
            _ = Assert.Single(sut.Addons);
            var updatedAddon = await sut.Addons.SingleAsync();
            Assert.Equal(expectedResult, result);
            Assert.Equal(now, updatedAddon.LastModified);
            Assert.Equal(before, updatedAddon.CreatedAt);
            Assert.Equal(updatedVersion, updatedAddon.Version);
        }

        [Fact]
        public async Task SaveChangesAsync_DispatchesEvents()
        {
            // Arrange
            var packageName = "Test Package";
            var packageExternalId = 1;
            var fakeDomainEventA = new FakeDomainEvent(this._timeServiceMock.Object);
            var fakeDomainEventB = new FakeDomainEvent(this._timeServiceMock.Object);
            var package = new Package(packageExternalId, packageName);
            package.AddDomainEvent(fakeDomainEventA);
            package.AddDomainEvent(fakeDomainEventB);

            var now = DateTime.UtcNow;
            _ = this._timeServiceMock.SetupGet(tsm => tsm.UtcNow).Returns(now);

            var sut = this.CreateSut();

            _ = await sut.Packages.AddAsync(package);

            // Act
            var result = await sut.SaveChangesAsync();

            // Arrange
            this._domainEventServiceMock.Verify(desm => desm.Publish(fakeDomainEventA), Times.Once);
            this._domainEventServiceMock.Verify(desm => desm.Publish(fakeDomainEventB), Times.Once);
        }

        private ApplicationDbContext CreateSut()
        {
            var databaseName = Guid.NewGuid().ToString();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _ = dbContextOptionsBuilder.UseInMemoryDatabase(databaseName);

            var applicationDbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options, this._domainEventServiceMock.Object, this._timeServiceMock.Object);

            return applicationDbContext;
        }
    }
}