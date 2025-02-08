using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapfromtype = typeof(IMapForm<>);
            var mappingMethodName = nameof(IMapForm<object>.Mapping);
            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapfromtype;
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumenttype = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);


                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo?.Invoke(instance, new object[] { this });
                }
                else
                {
                    var intefrace = type.GetInterfaces().Where(HasInterface).ToList();

                    if (intefrace.Count <= 0) continue;

                    foreach (var interfacemethodinfo in intefrace.Select(@intefrace =>
                    @intefrace.GetMethod(mappingMethodName, argumenttype)))
                    {
                        interfacemethodinfo?.Invoke(instance, new object[] { this });
                    }

                }

            }
        }
    }

}