﻿using System.ComponentModel.DataAnnotations;

namespace EtAlii.Adp.Service;

public class Graph
{
    [ID] public Guid Id { get; init; }
    [MaxLength(256)] public string Name { get; set; } = null!;

    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public ICollection<Item> Items { get; init; } = new List<Item>();
    public ICollection<Relation> Relations { get; init; } = new List<Relation>();
}