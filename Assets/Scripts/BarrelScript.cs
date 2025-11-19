using UnityEngine;
using TMPro;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    
    [SerializeField] private CameraShake cameraShakeScript;
    [SerializeField] private float cameraShakeDuration;
    
    [SerializeField] private GameObject recoilPoint;
    [SerializeField] private float recoilCameraShakeMagnitude;
    [SerializeField] private float recoilCameraShakeDuration;

    [SerializeField] private Shell[] ammunitionTypes;
    
    [SerializeField] private TextMeshProUGUI reloadTimeRemainingTextBox;
    
    private float reloadTime;
    private float reloadTimeRemaining;
    private bool reloading;
    
    private Rigidbody rb;
    
    void Start()
    {
        TankVariables tankVariables = GetComponentInParent<TankVariables>();
        reloadTime = tankVariables.reloadTime;
        
        rb = GetComponentInParent<Rigidbody>();
        reloadTimeRemainingTextBox.enabled = false;
         
    }
    
    void Update()
    {
        if (reloading)
        {
            TurretReloading();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (reloading)
            {
                return;
            }
            Fire();
        }
    }

    private void Fire()
    {
        Shell shotShell = Instantiate(ammunitionTypes[Random.Range(0, ammunitionTypes.Length)],  firePoint.transform.position, firePoint.transform.rotation);

        float shotShellRecoilValue = shotShell.recoilForce;
            
        ApplyRecoil(shotShellRecoilValue);
        
        ApplyCameraShake(shotShellRecoilValue / 10, cameraShakeDuration);
        
        TurretRelaodStarted();
    }

    private void ApplyRecoil(float recoilValue)
    {
        rb.AddForceAtPosition(-recoilPoint.transform.forward * (recoilValue * 5) , recoilPoint.transform.position, ForceMode.Impulse);
    }

    private void TurretRelaodStarted()
    {
        reloadTimeRemaining = reloadTime;

        reloading = true;
        
        reloadTimeRemainingTextBox.enabled = true;
    }
    
    
    private void TurretReloading()
    {
        reloadTimeRemainingTextBox.text = Mathf.Round(reloadTimeRemaining).ToString() ;
        
        reloadTimeRemaining -= Time.deltaTime;
        
        if (reloadTimeRemaining <= 0)
        {
            TurretReadyToFire();
        }
    }

    private void TurretReadyToFire()
    {
        reloading = false;
        reloadTimeRemainingTextBox.enabled = false;
    }

    private void ApplyCameraShake(float shakeIntensity, float shakeDuration)
    {
        cameraShakeScript.ShakeCamera(shakeIntensity, shakeDuration);
    }
}
