using UnityEngine;
using UnityEngine.EventSystems;

public class GFC_Cursor : MonoBehaviour
{
    public float sensitivity = 100f;
    private Camera gameCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        gameCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.tag = "Cursor";
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private float[] GetCamBounds()
    {
        float cameraHeight = gameCamera.orthographicSize;
        float cameraWidth = cameraHeight * gameCamera.aspect;
        
        float minX = gameCamera.gameObject.transform.position.x - cameraWidth;
        float maxX = gameCamera.gameObject.transform.position.x + cameraWidth;
        float minY = gameCamera.gameObject.transform.position.y - cameraHeight;
        float maxY = gameCamera.gameObject.transform.position.y + cameraHeight;
        
        return new float[] { minX, minY, maxX, maxY };
    }
    private void Move()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseDelta *= sensitivity * Time.deltaTime;
        Vector3 cursorPos = this.gameObject.transform.localPosition;
        float[] bounds = GetCamBounds();
        this.gameObject.transform.localPosition = new Vector3(
            (Mathf.Clamp(cursorPos.x + mouseDelta.x, bounds[0], bounds[2])),
            (Mathf.Clamp(cursorPos.y + mouseDelta.y, bounds[1], bounds[3])),
            (0));
    }
}
