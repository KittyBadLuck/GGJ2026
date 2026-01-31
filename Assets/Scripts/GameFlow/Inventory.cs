using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Transform spawnPoint;

    [Serializable]
    public struct Item
    {
        public int itemCount;
        public GameObject itemPrefab;
    }
    public void SpawnRandomObject()
    {
        if (items.Count == 0) return;
        int randomIndex = UnityEngine.Random.Range(0, items.Count);
        var item = items[randomIndex];

        Instantiate(item.itemPrefab, spawnPoint.position, Quaternion.identity);
        item.itemCount--;
        if (item.itemCount <= 0)
        {
            items.RemoveAt(randomIndex);
        }
        else
        {
            items[randomIndex] = item;
        }
    }

    public int GetItemCount()
    {
        int totalCount = 0;
        foreach (var item in items)
        {
            totalCount += item.itemCount;
        }
        return totalCount;
    }

}
