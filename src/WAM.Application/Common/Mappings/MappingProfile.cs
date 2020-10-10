using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace WAM.Application.Common.Mappings
{
    /// <inheritdoc/>
    /// <summary>
    /// Represents the mapping profile.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes the mapping profile.
        /// </summary>
        public MappingProfile()
        {
            this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected virtual void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly
                .GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)));

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(nameof(IMapFrom<Object>.Mapping))
                    ?? type.GetInterface(typeof(IMapFrom<>).Name).GetMethod(nameof(IMapFrom<Object>.Mapping));

                _ = (methodInfo?.Invoke(instance, new Object[] { this }));
            }
        }
    }
}