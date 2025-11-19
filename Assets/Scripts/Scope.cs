using UnityEngine;

public class Scope : MonoBehaviour
{
    private int cameraZoomChangeValue;
    private float cameraZoomDesired;
    
    private bool scoping = false;

    [SerializeField] private Animator animator;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (scoping)
        {
            if (GetScrollDeltaChanged())
            {
                cameraZoomDesired -= cameraZoomChangeValue * 2;
                
                cameraZoomDesired = Mathf.Clamp(cameraZoomDesired, 25, 55);
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            if (!scoping)
            {
                animator.Play("FirstsPersonCamera");
                scoping = true;
            }

            if (scoping)
            {
                animator.Play("thirdPersonCamera");
                scoping = false;
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
