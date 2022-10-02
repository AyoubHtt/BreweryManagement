namespace Domain.SeedWork;

public class SmartEnum<T> where T : SmartEnum<T>
{
    public int Id { get; }
    public string Name { get; }
    public string Value { get; }

    public SmartEnum(int id, string name) : this(id, name, name) { }

    public SmartEnum(int id, string name, string value)
    {
        Id = id;
        Name = name;
        Value = value;
    }

    public static IEnumerable<T> GetAll()
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public static T FromId(int id) => Parse(item => item.Id == id);

    public static T FromName(string name) => Parse(item => item.Name == name);

    public static T FromValue(string value) => Parse(item => item.Value == value);

    public static bool TryParseFromName(string name) => GetAll().Any(s => s.Name == name);

    private static T Parse(Func<T, bool> predicate)
    {
        var matchingItem = GetAll().FirstOrDefault(predicate);

        if (matchingItem == null) return default!;

        return matchingItem;
    }
}
