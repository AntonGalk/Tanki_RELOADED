using System;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float velocity;
    [SerializeField] protected float weight;
    public float recoilForce;
    
    [NonSerialized] protected Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        ApplyForceOnceFired();
        AdjustWeightToRigidbody();
    }

    // Update is called once per frame
    protected void Update()
    {
        Vector3 flyingDirection = rb.linearVelocity - transform.forward;
        if (flyingDirection.magnitude > 1.5f)
        {
            transform.rotation = Quaternion.LookRotation(flyingDirection, transform.up);
        }
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
