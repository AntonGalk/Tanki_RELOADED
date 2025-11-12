using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Turret : MonoBehaviour
{
    
    [SerializeField] Transform cameraTransform;
    [SerializeField] private float lookRaycastMaxDistance;
    [NonSerialized] public Vector3 cameraRaycastVectorResult;
    [NonSerialized] public Vector3 barrelRaycastVectorResult;
    
    [SerializeField] private GameObject barrelAnchorRef;
    [SerializeField] private GameObject barrelRef;
    [SerializeField] private GameObject turretAnchorRef;
    private float turretRotationSpeed;
    private float barrelPitchSpeed;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TankVariables tankVariables = GetComponentInParent<TankVariables>();
        turretRotationSpeed  = tankVariables.turretRotationSpeed;
        barrelPitchSpeed = tankVariables.barrelPitchSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastToTurretRotationTarget();
        RaycastToBarrelTarget();

        AdjustTurretRotation();
        AdjustBarrelPitch();
    }

    private Vector3 RaycastToTurretRotationTarget()
    {
        Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, lookRaycastMaxDistance);
        if (hitInfo.collider == null)
        {
            hitInfo.point = cameraTransform.position + cameraTransform.forward * lookRaycastMaxDistance;
        }
        cameraRaycastVectorResult = hitInfo.point;
        
        return cameraRaycastVectorResult;
    }

    private Vector3 RaycastToBarrelTarget()
    { 
        Vector3 barrelDirection = cameraRaycastVectorResult - barrelRef.transform.position;
        Physics.Raycast(barrelRef.transform.position, barrelDirection, out RaycastHit hitInfo, lookRaycastMaxDistance);
        if (hitInfo.collider == null)
        {
            hitInfo.point = barrelRef.transform.position + barrelDirection * lookRaycastMaxDistance;
        }
        barrelRaycastVectorResult = hitInfo.point;
        
        return barrelRaycastVectorResult;
    }

    private void AdjustTurretRotation()
    {
        Vector3 rotationDirection = cameraRaycastVectorResult - turretAnchorRef.transform.position;
        Vector3 finalTarget = new Vector3(rotationDirection.x, 0f, rotationDirection.z);
        //Vector3 lerpTarget = Vector3.Lerp(turretAnchorRef.transform.position, finalTarget, turretRotationSpeed * Time.deltaTime);
        
        turretAnchorRef.transform.rotation = Quaternion.LookRotation(finalTarget).normalized;
    }
    
    private void AdjustBarrelPitch()
    {
        Vector3 pitchDirection = barrelRaycastVectorResult - barrelAnchorRef.transform.position;
        Vector3 finalTarget = new Vector3(pitchDirection.x, pitchDirection.y, 0f);
        barrelAnchorRef.transform.rotation = Quaternion.LookRotation(cameraTransform.forward).normalized;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f); 
        Gizmos.DrawLine(cameraTransform.position, cameraRaycastVectorResult);
        Gizmos.DrawSphere(cameraRaycastVectorResult, 0.5f);
        
        Gizmos.color = new Color(1f, 1f, 0f);
        Gizmos.DrawLine(barrelRef.transform.position, barrelRaycastVectorResult);
        Gizmos.DrawSphere(barrelRaycastVectorResult, 0.5f);
    }
    
}
