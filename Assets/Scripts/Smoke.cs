using UnityEngine;

public class Smoke : Shell
{
    [SerializeField] ParticleSystem smokeEffect;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    
    private void OnTriggerEnter (Collider collider)
    {
        Instantiate(smokeEffect,  transform.position, transform.rotation);
        
        base.OnTriggerEnter(collider); 
    }
}
