using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class Gear : MonoBehaviour
{
    public int currentGear = 0;

    private TankMovement tankMovement;
    
    [SerializeField] private WheelCollider[] wheels;
    
    [SerializeField] TextMeshProUGUI trackGearText;
    [SerializeField] private bool isLeftTrack;
    private int maxGear;
    
    private float moveSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tankMovement = GetComponentInParent<TankMovement>();
        TankVariables tankVariables = GetComponentInParent<TankVariables>();
        maxGear = tankVariables.maxGear;
        moveSpeed = tankVariables.moveSpeed;
    }

    void FixedUpdate()
    {
        foreach (WheelCollider i in wheels)
        {
            i.motorTorque = (currentGear * moveSpeed);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGear(bool speedIncreased)
    {
        if (speedIncreased && currentGear < maxGear)
        {
            currentGear++;
            UpdateGearInGUI();
            UpdateGearInTankMovement();
        }
        if (!speedIncreased && currentGear > -maxGear)
        {
            currentGear--; 
            UpdateGearInGUI();
            UpdateGearInTankMovement();
        }
    }

    private bool IdentifySelf()
    {
        if (isLeftTrack)
        {
            return true;
        }
        return false;
    }

    private void UpdateGearInTankMovement()
    {
        if (IdentifySelf())
        {
            tankMovement.leftTrackGear = currentGear;
        }
        if (!IdentifySelf())
        {
            tankMovement.rightTrackGear = currentGear;
        }
    }

    private void UpdateGearInGUI()
    {
        trackGearText.text = currentGear.ToString();
    }
}
