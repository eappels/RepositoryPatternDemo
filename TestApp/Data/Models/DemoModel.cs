using SQLite;

namespace TestApp.Data.Models;

[Table("DemoModel")]
public class DemoModel
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [Ignore]
    public List<Item> Items { get; set; } = new List<Item>();

    public DemoModel()
    {
    }

    public DemoModel(string name)
    {
        Name = name;
    }
}