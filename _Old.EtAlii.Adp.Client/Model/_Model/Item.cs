using System.Numerics;

namespace EtAlii.Adp.Client;

public class Item
{
    public Guid Id { get; init; }
    public string Label { get; set; } = string.Empty;

    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
}