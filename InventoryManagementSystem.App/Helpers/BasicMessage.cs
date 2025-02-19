namespace InventoryManagementSystem.App.Helpers;

public class BasicMessage
{
    public string Message { get; set; }
    public string Sender { get; set; }
    public virtual string Type { get; set; }

    public string Action { get; set; }
    public DateTime Date { get; set; }

}
