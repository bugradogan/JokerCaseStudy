using UnityEngine;

public static class ItemFactory
{
    public static ItemData CreateItemData(string itemName)
    {
        // ItemData ScriptableObject'leri Resources klasöründe olmalý
        ItemData itemData = Resources.Load<ItemData>($"Items/{itemName}");

        if (itemData == null)
        {
            Debug.LogError($"Item with name {itemName} not found!");
            return null;
        }

        return itemData;
    }
}
