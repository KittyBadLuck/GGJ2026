using System;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragController : MonoBehaviour
{
    public bool isDraggable = true;
    private bool isDragging = false;
    
    private Vector3 mousePosition;

    [Range (0.0f, 100.0f)]
    public float damping = 1.0f;

    [Range (0.0f, 100.0f)]
    public float frequency = 5.0f;
    
    [Range (0.0f, 10000.0f)]  
    public float force = 100.0f;
    
    private TargetJoint2D targetJoint2D;
    
    public InputActionReference freeze;

    public static event Action OnItemFroze;

    void OnMouseDown()
    {
        if (isDraggable)
        {
            //Debug.Log("Object Clicked");
            isDragging = true;
            
            var collider = Physics2D.OverlapPoint(mousePosition);
            if (!collider)
            {
                return;
            }

            var body = collider.GetComponentInParent<Rigidbody2D>();

            if (!body)
            {
                return;
            }
            //Debug.Log(collider.gameObject.name);
            //Debug.Log(body);

            targetJoint2D = body.gameObject.AddComponent<TargetJoint2D>();
            targetJoint2D.dampingRatio = damping;
            targetJoint2D.frequency = frequency;
            targetJoint2D.maxForce = force;
            
            targetJoint2D.anchor = targetJoint2D.transform.InverseTransformPoint(mousePosition);
            
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        Destroy(targetJoint2D);
        targetJoint2D = null;
    }

    void Update()
    {
        Mouse mouse = Mouse.current;
        mousePosition = Camera.main.ScreenToWorldPoint(mouse.position.value);
        //Debug.Log(mousePosition);

        if (targetJoint2D != null)
        {
            targetJoint2D.target = mousePosition;
        }
        
    }

    void OnEnable()
    {
        freeze.action.started += Freeze;
    }

    private void OnDisable()
    {
        freeze.action.started -= Freeze;
    }

    private void Freeze(InputAction.CallbackContext context)
    {
        var body = this.GetComponent<Rigidbody2D>();
        body.simulated = false;
        this.gameObject.layer = LayerMask.NameToLayer("Mask");
        OnItemFroze?.Invoke();
    }
}