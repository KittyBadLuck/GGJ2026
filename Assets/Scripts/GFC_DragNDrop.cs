using System;
using Unity.VisualScripting;
using UnityEngine;

public class GFC_DragNDrop : MonoBehaviour
{
    
    private bool cursorInside = false;
    private GameObject cursor;
    private Rigidbody2D rb;
    private float gscale;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        gscale = rb.gravityScale;
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
        if (Input.GetMouseButtonDown(0))
        {
            this.gameObject.transform.parent = cursorInside ? cursor.transform : null;
            rb.gravityScale = cursorInside ? 0f : gscale;
        }
        if (Input.GetMouseButtonUp(0))
        {
            this.gameObject.transform.parent = null;
            rb.gravityScale = gscale;
        }
    }
}
