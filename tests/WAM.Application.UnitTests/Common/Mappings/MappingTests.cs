using AutoMapper;
using WAM.Application.UnitTests.Fakes;
using Xunit;

namespace WAM.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            this._configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<FakeMappingProfile>());

            this._mapper = this._configurationProvider.CreateMapper();
        }

        [Fact]
        public void MappingConfigurationIsValid() =>
            // Act
            // Arrange
            // Assert
            this._configurationProvider.AssertConfigurationIsValid();

        [Fact]
        public void MappingProfile_AppliesMappingFromAssembly()
        {
            // Arrange
            var entity = new FakeEntity();
            var model = new FakeModel();
            
            // Act
            _ = this._mapper.Map(entity, model);

            // Assert
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.TestChar, model.TestChar);
            Assert.Equal(entity.TestFlag, model.TestFlag);
            Assert.Equal(entity.TestText, model.TestText);
        }
    }
}