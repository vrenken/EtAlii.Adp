using System.ComponentModel.DataAnnotations;

namespace BlazorApp2;

public class Item
{
    //[ID] 
    public Guid Id { get; init; }
    [MaxLength(256)] public string Name { get; set; } = null!;
    public float X { get; set; }
    public float Y { get; set; }
    public float W { get; set; }
    public float H { get; set; }
}