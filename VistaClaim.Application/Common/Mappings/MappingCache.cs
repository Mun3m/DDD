using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VistaClaim.Application.Common.Mappings
{
    public class MappingCache
    {
        private readonly HashSet<string> _keys = new HashSet<string>();

        public MappingCache()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public bool ContainsKey(string key) => _keys.Contains(key);

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICacheMap<>)))
                .ToList();

            foreach (var type in types)
                _keys.Add(type.Name);
        }
    }
}
