namespace InventoryManagementSystem.App.Entities;

public class Warehouse : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int Capacity { get; set; }
}
