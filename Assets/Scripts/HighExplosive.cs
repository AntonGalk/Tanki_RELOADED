using UnityEngine;

public class HighExplosive : Shell
{
    [SerializeField] ParticleSystem explodeEffect;
    [SerializeField] private float explotionRadius;
    private bool exploded = false;
    
    void Start()
    {   
        base.Start();
    }
    
    void Update()
    {
        base.Update();
    }
    
    private void OnTriggerEnter (Collider collider)
    {
        Instantiate(explodeEffect,  transform.position, transform.rotation);
        
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, explotionRadius, transform.forward, 0);
        
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                Debug.Log(hit.transform.name);
            }
        }
        base.OnTriggerEnter(collider);
    }

    private void OnDrawGizmos()
    {
            Gizmos.DrawWireSphere(transform.position, explotionRadius);
    }
}
