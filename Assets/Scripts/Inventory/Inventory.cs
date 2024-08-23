using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory Instance => _instance ??= new GameObject("Inventory").AddComponent<Inventory>();

    private Dictionary<Item, int> items = new Dictionary<Item, int>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }  


    private void OnEnable()
    {
        LoadInventory();
    }  

    public void AddItem(ItemData itemData, int amount)
    {
        Item item = itemData.CreateItem();

        if (items.ContainsKey(item))
        {
            items[item] += amount;
        }
        else
        {
            items[item] = amount;
        }

        SaveInventory();
        NotifyObservers();
    }

    public int GetItemCount(ItemData itemData)
    {
        Item item = itemData.CreateItem();       
        return items.ContainsKey(item) ? items[item] : 0;
    }

    private void SaveInventory()
    {
        foreach (var item in items)
        {
            PlayerPrefs.SetInt(item.Key.itemName, items[item.Key]);
        }
        PlayerPrefs.Save();
    }

    private void LoadInventory()
    {
        var allItems = Resources.LoadAll("Items");
        foreach (var _item in allItems)
        {
            int itemCount = PlayerPrefs.GetInt(_item.name, 0);
            if (itemCount > 0)
            {
                ItemData itemData = ItemFactory.CreateItemData(_item.name);
                if (itemData != null)
                {
                    Item item = itemData.CreateItem();
                    items[item] = itemCount;                 
                }
            }
        }
        NotifyObservers();
    }


    private void OnApplicationQuit()
    {
        SaveInventory();
    }

    
    private void NotifyObservers()
    {
        EventManager.Broadcast(GameEvent.OnInventoryChanged);
    }
}
