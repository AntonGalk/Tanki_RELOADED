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
    private float barrelMinPitch;
    private float barrelMaxPitch;
    
    void Start()
    {
        TankVariables tankVariables = GetComponentInParent<TankVariables>();
        turretRotationSpeed  = tankVariables.turretRotationSpeed;
        barrelPitchSpeed = tankVariables.barrelPitchSpeed;
        barrelMinPitch =  tankVariables.barrelMinPitch;
        barrelMaxPitch = tankVariables.barrelMaxPitch;
    }

    void Update()
    {
        RaycastToTurretRotationTarget();
        RaycastToBarrelTarget();

        AdjustTurretRotation();
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
        Vector3 barrelDirection = cameraRaycastVectorResult - barrelAnchorRef.transform.position;
        Physics.Raycast(barrelRef.transform.position, barrelAnchorRef.transform.forward, out RaycastHit hitInfo, lookRaycastMaxDistance);
        if (hitInfo.collider == null)
        {
            hitInfo.point = barrelRef.transform.position + barrelRef.transform.forward * lookRaycastMaxDistance;
        }
        barrelRaycastVectorResult = hitInfo.point;
        
        return barrelRaycastVectorResult;
    }

    private void AdjustTurretRotation()
    {
        Vector3 up = turretAnchorRef.transform.up;
        
        Vector3 directionToTarget = Vector3.ProjectOnPlane(cameraRaycastVectorResult - barrelAnchorRef.transform.position, up);
        Quaternion turretTargetDirection = Quaternion.LookRotation(directionToTarget, up);
        
        Quaternion from = Quaternion.LookRotation(turretAnchorRef.transform.forward, turretAnchorRef.transform.up);
        
        Quaternion turretLerpedRotation = Quaternion.RotateTowards(from, turretTargetDirection, turretRotationSpeed * Time.deltaTime);
        turretAnchorRef.transform.rotation = turretLerpedRotation;
        
        AdjustBarrelPitch();
    }
    
    private void AdjustBarrelPitch()
    {
        Vector3 directionToTarget = cameraRaycastVectorResult - barrelAnchorRef.transform.position;

        Vector3 localDirection = turretAnchorRef.transform.InverseTransformDirection(directionToTarget.normalized);
        
        float desiredPitch = -Mathf.Atan2(localDirection.y, localDirection.z) * Mathf.Rad2Deg;
        
        desiredPitch = Mathf.Clamp(desiredPitch, barrelMinPitch, barrelMaxPitch);
        
        Quaternion targetLocalRot = Quaternion.Euler(desiredPitch, 0f, 0f);
        barrelAnchorRef.transform.localRotation = Quaternion.Slerp(barrelAnchorRef.transform.localRotation, targetLocalRot, barrelPitchSpeed * Time.deltaTime);
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
