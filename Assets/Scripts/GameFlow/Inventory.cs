using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Transform spawnPoint;
    public TextMeshProUGUI remainingItemText;

    [Serializable]
    public struct Item
    {
        public int itemCount;
        public GameObject itemPrefab;
    }
    private void Start()
    {
        UpdateTextCount();
    }
    private void UpdateTextCount()
    {
        remainingItemText.text = $"{GetItemCount()}";
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

        UpdateTextCount();
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
