namespace EtAlii.Adp.Client;

public class Item
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public float X { get; set; }
    public float Y { get; set; }
    public float W { get; set; }
    public float H { get; set; }
}