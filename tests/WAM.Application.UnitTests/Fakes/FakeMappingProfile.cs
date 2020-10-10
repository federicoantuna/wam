using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using WAM.Application.Common.Mappings;

namespace WAM.Application.UnitTests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeMappingProfile : MappingProfile
    {
        protected override void ApplyMappingsFromAssembly(Assembly assembly)
        {
            assembly = Assembly.GetExecutingAssembly();
            base.ApplyMappingsFromAssembly(assembly);
        }
    }
}