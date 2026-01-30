using System;
using Unity.VisualScripting;
using UnityEngine;

public class GFC_DragNDrop : MonoBehaviour
{
    
    private bool cursorInside = false;
    private GameObject cursor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DragObject();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cursor")
        {
            cursorInside = true;
            cursor = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cursor")
        {
            cursorInside = false;
        }
    }

    private void DragObject()
    {
        if (cursorInside && Input.GetMouseButtonDown(0))
        {
            this.gameObject.transform.parent = cursor.transform;
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            this.gameObject.transform.parent = null;
        }

        if (!cursorInside)
        {
            this.gameObject.transform.parent = null;
        }
    }
}
