using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

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
        Mouse mouse = Mouse.current;
        var mousePosition = Camera.main.ScreenToWorldPoint(mouse.position.value);
        mousePosition.z = 0;
        var go = Instantiate(item.itemPrefab, mousePosition, Quaternion.identity);
        item.itemCount--;
        if (item.itemCount <= 0)
        {
            items.RemoveAt(randomIndex);
        }
        else
        {
            items[randomIndex] = item;
        }
        
        var dragController = go.GetComponent<DragController>();
        var rb = go.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = UnityEngine.Random.Range(-300f, 300f);
        if (dragController != null)
        {
            dragController.MarkForDrag();
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
