using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private GameObject trackLeft;
    public int leftTrackGear;
    
    [SerializeField] private GameObject trackRight;
    public int rightTrackGear;
    
    
    private Rigidbody rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb =  GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        //Checks if acceleration inputs are made
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdateGear(true, trackLeft);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateGear(true, trackRight);
        }
        
        //Checks if slowdown inputs are made
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdateGear(false, trackLeft);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpdateGear(false, trackRight);
        }
        
        rb.AddForceAtPosition(trackRight.transform.forward * (rightTrackGear), trackRight.transform.position, ForceMode.Impulse);
        rb.AddForceAtPosition(trackLeft.transform.forward * (leftTrackGear), trackLeft.transform.position, ForceMode.Impulse);
    }

    private void UpdateGear(bool speedIncreased, GameObject affectedTrack)
    {
        Gear gearScript = affectedTrack.GetComponent<Gear>(); 
        gearScript.UpdateGear(speedIncreased);
    }
}
