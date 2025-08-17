using AccesoDatos.Mappings;
using AutoMapper;

namespace Apis.Extensions
{
    public static class MappingExtensions
    {
        public static void RegisterApiMappings(this IMapperConfigurationExpression config)
        {
            config.AddProfile<MappingProfile>();
        }
    }
}
