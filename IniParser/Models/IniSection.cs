namespace IniParser.Models;

public class IniSection
{
    public string? Name { get; set; }
    public IEnumerable<KeyValuePair<string, string>> Properties { get; set; }

    public IniSection(
        string name,
        IEnumerable<KeyValuePair<string, string>> properties)
    {
        Name = name;
        Properties = properties;
    }

    public override string? ToString()
    {
        string props = String.Join(
            Environment.NewLine,
            Properties.Select(a => $"{a.Key} => {a.Value}"));
        return $"[{Name}]{Environment.NewLine}{props}";
    }
}
