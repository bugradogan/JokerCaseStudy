using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;  // For UI

    public Item(string itemName, Sprite icon)
    {
        this.itemName = itemName;
        this.icon = icon;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Item other = (Item)obj;
        return itemName == other.itemName;
    }

    public override int GetHashCode()
    {
        return itemName != null ? itemName.GetHashCode() : 0;
    }
}
