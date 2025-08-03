using SQLite;

namespace TestApp.Data.Models;

[Table("Item")]
public class Item
{

    public string Name { get; set; } = string.Empty;
    public int DemoModelId { get; set; }

    public Item()
    {        
    }

    public Item(string name, int id)
    {
        Name = name;
        DemoModelId = id;
    }
}