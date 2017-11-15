using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float Speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Speed;
        Physics.IgnoreLayerCollision(11, 12);
        Physics.IgnoreLayerCollision(11, 13);
        Physics.IgnoreLayerCollision(12, 13);
    }

    public void Slow()
    {
        rb.velocity = transform.forward * 0;
        // gameObject.GetComponent<Mover>().enabled = false;
    }
}
