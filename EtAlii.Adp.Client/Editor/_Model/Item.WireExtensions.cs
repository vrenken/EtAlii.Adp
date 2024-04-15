namespace EtAlii.Adp.Client;

public static class ItemWireExtensions
{
    public static Item ToLocal(this IAddItem_AddItem_Item item)
    {
        return new Item
        {
            Id = Guid.Parse(item.Id),
            Name = item.Name,
            X = (float)item.X,
            Y = (float)item.Y,
            W = (float)item.W,
            H = (float)item.H,
        };
    }
    public static Item ToLocal(this IItemAdded_ItemAdded item)
    {
        return new Item
        {
            Id = Guid.Parse(item.Id),
            Name = item.Name,
            X = (float)item.X,
            Y = (float)item.Y,
            W = (float)item.W,
            H = (float)item.H,
        };
    }
}