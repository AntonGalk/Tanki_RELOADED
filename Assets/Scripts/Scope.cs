using UnityEngine;

public class Scope : MonoBehaviour
{
    [SerializeField] private Camera scopeCamera;
    [SerializeField] private Camera thirdPersonCamera;

    private int cameraZoomChangeValue;
    private float cameraZoomDesired;
    
    private bool scoping = false;

    void Start()
    {
        cameraZoomDesired = scopeCamera.fieldOfView;
    }
    
    void Update()
    {
        if (scoping)
        {
            if (GetScrollDeltaChanged())
            {
                cameraZoomDesired -= cameraZoomChangeValue * 2;
                
                cameraZoomDesired = Mathf.Clamp(cameraZoomDesired, 25, 55);

                scopeCamera.fieldOfView = cameraZoomDesired;
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            if (!scoping)
            {
                scopeCamera.enabled = true;
                thirdPersonCamera.enabled = false;
                
                scoping =  true;
                
                return;
            }

            if (scoping)
            {
                thirdPersonCamera.enabled = true;
                scopeCamera.enabled = false;
                
                scoping = false;

                return;
            }
        }
    }

    private bool GetScrollDeltaChanged()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            cameraZoomChangeValue = Mathf.RoundToInt(Input.mouseScrollDelta.y);
            
            return true;
        }
        return false;
    }
}
