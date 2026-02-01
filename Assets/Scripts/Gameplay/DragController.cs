using System;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragController : MonoBehaviour
{
    public bool isDraggable = true;
    private bool markedForDrag = false;
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
    public InputActionReference release;
    
    LineRenderer lineRenderer;
    public Material lineMaterial;
    public float matScale = 1.0f;

    public static event Action OnItemFroze;
    private bool canFreeze = true;
    void OnMouseDown()
    {
        markedForDrag = true;
    }

    public void Drag()
    {
        var collider = GetComponent<Collider2D>();
        if (isDraggable)
        {
            //Debug.Log("Object Clicked");
            isDragging = true;
            
            
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
            Mouse mouse = Mouse.current;
            mousePosition = Camera.main.ScreenToWorldPoint(mouse.position.value);
            targetJoint2D.anchor = targetJoint2D.transform.InverseTransformPoint(mousePosition);
            
            lineRenderer = body.gameObject.AddComponent<LineRenderer>();
            lineRenderer.textureMode = LineTextureMode.Tile;
            lineRenderer.alignment = LineAlignment.TransformZ;
            lineRenderer.positionCount = 2;
            lineRenderer.material = lineMaterial;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.sortingLayerName = "UI";
            lineRenderer.sortingOrder = 50;
            
        }

        markedForDrag = false;
    }

    public void MarkForDrag()
    {
        markedForDrag = true;
    }
    void Update()
    {

        Mouse mouse = Mouse.current;
        mousePosition = Camera.main.ScreenToWorldPoint(mouse.position.value);
        //Debug.Log(mousePosition);

        if (targetJoint2D != null)
        {
            targetJoint2D.target = mousePosition;

            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, targetJoint2D.transform.TransformPoint(targetJoint2D.anchor));
                lineRenderer.SetPosition(1, mousePosition);
                float distance = Vector3.Distance(targetJoint2D.transform.TransformPoint(targetJoint2D.anchor), mousePosition);
                lineRenderer.textureScale = new Vector2(2, 1);
            }
        }
        
    }

    private void LateUpdate()
    {
        if (markedForDrag)
        {
            Drag();
        }
    }

    void OnEnable()
    {
        freeze.action.started += Freeze;
        release.action.performed += Release;
        Alarm.OnAlarmTriggered += OnAlarmTriggered;
    }

    private void OnDisable()
    {
        Alarm.OnAlarmTriggered -= OnAlarmTriggered;
        release.action.performed -= Release;
        freeze.action.started -= Freeze;
    }
    private void OnAlarmTriggered()
    {
        canFreeze = false;
        Release();
    }
    private void Release()
    {
        if (!isDragging)
            return;

        isDragging = false;
        Destroy(targetJoint2D);
        targetJoint2D = null;

        Destroy(lineRenderer);
        lineRenderer = null;
    }
    private void Release(InputAction.CallbackContext context)
    {

        Release();
    }
    private void Freeze(InputAction.CallbackContext context)
    {
        if (!isDragging)
            return;
        if (!canFreeze)
            return;
        var body = this.GetComponent<Rigidbody2D>();
        body.simulated = false;
        this.gameObject.layer = LayerMask.NameToLayer("Mask");
        OnItemFroze?.Invoke();
    }
}