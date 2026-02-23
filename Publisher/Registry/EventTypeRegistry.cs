using Pushables;

namespace Publisher.Registry
{
    public class EventTypeRegistry
    {
        private readonly Dictionary<string, Type> _nameToType = new();
        private readonly Dictionary<Type, string> _typeToName = new();

        public EventTypeRegistry()
        {
            // Сканируем все типы в текущей сборке и зависимостях
            var pushableTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IPushable).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in pushableTypes)
            {
                // Используем имя типа без "Event" суффикса как ключ (например, UserCreated)
                var typeName = type.Name.Replace("Event", "");
                _nameToType[typeName] = type;
                _typeToName[type] = typeName;
            }
        }

        public Type? GetTypeByName(string typeName) =>
            _nameToType.TryGetValue(typeName, out var type) ? type : null;

        public string GetTypeNameByType(Type type) =>
            _typeToName.TryGetValue(type, out var name) ? name : type.Name;

        public IEnumerable<Type> GetAllTypes() => _nameToType.Values;
    }
}
