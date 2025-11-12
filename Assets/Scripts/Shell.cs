using System;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float damage;
    public float velocity;
    public float weight;
    
    [NonSerialized] public Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        ApplyForceOnceFired();
        AdjustWeightToRigidbody();
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 flyingDirection = rb.linearVelocity - transform.forward;
        
        transform.rotation = Quaternion.LookRotation(flyingDirection, transform.up);
    }

    private void AdjustWeightToRigidbody()
    {
        rb.mass = weight;
    }

    private void ApplyForceOnceFired()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * velocity, ForceMode.Impulse);
    }
}
