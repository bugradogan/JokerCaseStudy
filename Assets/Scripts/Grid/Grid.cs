using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] TextMeshPro itemCountText;
    [SerializeField] private ItemData itemData;
    [SerializeField] private GameObject collectParticleEffect;

    private int itemCount;
    private MeshRenderer gridRenderer;
    private Material gridMaterial;

    private void Awake()
    {
        gridRenderer = GetComponent<MeshRenderer>();
        gridMaterial = gridRenderer.material;
    }
    public void SetItemCount(int itemCount)
    {
        this.itemCount = itemCount;
        itemCountText.text = itemCount.ToString();
    }

    public ItemData GetItemData()
    { return itemData; }


    public void CollectItem()
    {
        if (itemData != null)
        {
            Inventory.Instance.AddItem(itemData, itemCount);
            collectParticleEffect.SetActive(true);
        }
    }

    public void SetGridColor(Color color)
    {
        gridMaterial.color = color;
    }
}
