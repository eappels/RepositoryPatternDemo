using SQLite;

namespace TestApp.Models;

[Table("DemoModel")]
public class DemoModel
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public DemoModel()
    {
    }

    public DemoModel(string name)
    {
        Name = name;
    }
}