using System.ComponentModel.DataAnnotations;

namespace BlazorApp2;

public class Relation
{
    //[ID] 
    public Guid Id { get; init; }
    [MaxLength(256)] public string Name { get; set; } = null!;

    public Item Source { get; set; } = null!;
    public Item Target { get; set; } = null!;
}