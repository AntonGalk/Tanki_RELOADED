using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private GameObject trackLeft;
    [NonSerialized] public int leftTrackGear;
    
    [SerializeField] private GameObject trackRight;
    [NonSerialized] public int rightTrackGear;

    private float moveSpeed;
    
    private Rigidbody rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = GetComponent<TankVariables>().moveSpeed;
        rb =  GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        moveSpeed = GetComponent<TankVariables>().moveSpeed;
        
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
        
        rb.AddForceAtPosition(trackRight.transform.forward * (rightTrackGear * moveSpeed * Time.deltaTime), trackRight.transform.position, ForceMode.Acceleration);
        rb.AddForceAtPosition(trackLeft.transform.forward * (leftTrackGear * moveSpeed * Time.deltaTime), trackLeft.transform.position, ForceMode.Acceleration);
    }

    void FixedUpdate()
    {
        
    }

    private void UpdateGear(bool speedIncreased, GameObject affectedTrack)
    {
        Gear gearScript = affectedTrack.GetComponent<Gear>(); 
        gearScript.UpdateGear(speedIncreased);
    }
}
