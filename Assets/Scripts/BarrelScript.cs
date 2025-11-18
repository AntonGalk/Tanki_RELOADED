using UnityEngine;
using TMPro;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    
    [SerializeField] private GameObject recoilPoint;
    [SerializeField] private CameraShake cameraShakeScript;
    [SerializeField] private float recoilCameraShakeMagnitude;
    [SerializeField] private float recoilCameraShakeDuration;

    [SerializeField] private Shell[] ammunitionTypes;
    
    [SerializeField] private TextMeshProUGUI reloadTimeRemainingTextBox;
    [SerializeField] private float reloadTime;
    private float reloadTimeRemaining;
    private bool reloading;
    
    private Rigidbody rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        reloadTimeRemainingTextBox.enabled = false;
    }

    // Update is called once per frame
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
        
        ApplyRecoil(shotShell.recoilForce);
        StartCoroutine(cameraShakeScript.Shake(recoilCameraShakeMagnitude,  recoilCameraShakeDuration));

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
}
