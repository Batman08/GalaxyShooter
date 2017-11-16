using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float Speed;
    private float _minSpeed = -0.5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Speed;
        Physics.IgnoreLayerCollision(11, 12);
        Physics.IgnoreLayerCollision(11, 13);
        Physics.IgnoreLayerCollision(12, 13);
    }

    private void Update()
    {
        if (Speed < _minSpeed)
        {
            Speed = _minSpeed;
        }
    }

    public void Slow()
    {
       // rb.velocity += Vector3.zero / +0.2f/*transform.forward * 1*/;
        // gameObject.GetComponent<Mover>().enabled = false;
    }
}
