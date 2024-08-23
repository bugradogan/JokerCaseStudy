using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using static GridController;
using static UnityEditor.PlayerSettings;
public class GridController : MonoBehaviour
{
    [Range(10, GameManager.MAX_GRID_COUNT)]
    [SerializeField] int gridCount;

    [SerializeField] Grid[] gridPrefabs;


    private void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < gridCount; i++)
        {
            pos.z = i * 5;
            int prefabIndex = i == 0 ? 0 : Random.Range(0, gridPrefabs.Length);
            Grid grid = Instantiate(gridPrefabs[prefabIndex], pos, Quaternion.identity);
            if (grid.GetItemData() != null)
                grid.SetItemCount(Random.Range(2, 15));
        }
    }
}
