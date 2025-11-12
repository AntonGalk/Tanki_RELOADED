using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject recoilPoint;

    [SerializeField] private Shell[] ammunitionTypes;
    
    private Rigidbody rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Shell shotShell = Instantiate(ammunitionTypes[Random.Range(0, ammunitionTypes.Length)],  firePoint.transform.position, firePoint.transform.rotation);
        
        ApplyRecoil(shotShell.recoilForce);
    }

    private void ApplyRecoil(float recoilValue)
    {

        rb.AddForceAtPosition(-recoilPoint.transform.forward * (recoilValue * 5) , recoilPoint.transform.position, ForceMode.Impulse);
    }
}
