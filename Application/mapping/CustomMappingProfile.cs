using AutoMapper;
using System.Collections.Generic;
using System.Reflection;

namespace Application;

public class CustomMappingProfile : Profile
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            var allTypes = Assembly.GetEntryAssembly()!.GetExportedTypes();
            var dtoTypes = allTypes.Where(p =>
                p is { IsAbstract: false, IsClass: true, IsPublic: true } && p.GetInterfaces().Contains(typeof(IHaveCustomMapping)));
            foreach (var type in dtoTypes)
            {
                var createMapping = Activator.CreateInstance(type) as IHaveCustomMapping;
                createMapping!.ApplyMapping(this);
            }
        }
    }
}
