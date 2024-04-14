namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Subscription 
{
    [Subscribe]
    public Relation RelationAdded([EventMessage] Relation relation) => relation;
    
    [Subscribe]
    public Relation RelationChanged([EventMessage] Relation relation) => relation;
    
    [Subscribe]
    public Relation RelationRemoved([EventMessage] Relation relation) => relation;

}