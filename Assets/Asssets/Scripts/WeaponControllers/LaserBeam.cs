using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeamBoundary
{
    public float xMin, xMax, zMin, zMax;
}

public class LaserBeam : MonoBehaviour
{
    public Boundary boundary;
    public LineRenderer LineRenderer;
    public Transform Point1, Point2;
    public ParticleSystem ImpactEffect;

    private Rigidbody rb;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        ImpactEffect.Play();
    }

    void Update()
    {
        Position();
        Move();
        Laser();

    }

    void Position()
    {
        transform.position = new Vector3
             (
                 Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                 0f,
                 Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
             );
    }

    void Move()
    {
        rb.position = Vector3.forward * 500;

        //if (transform.position.z >= boundary.zMax)
        //{
        //    transform.position = Vector3.back * 100 * Time.deltaTime;
        //}

        //else if (transform.position.z <= boundary.zMin)
        //{
        //    transform.position = Vector3.forward * 100 * Time.deltaTime;
        //}
    }

    void Laser()
    {
        LineRenderer.SetPosition(0, Point1.position);
        LineRenderer.SetPosition(1, Point2.position);

    }

    void OnTriggerEnter(Collider other)
    {
        bool Enemy = (other.gameObject.CompareTag("Enemy"));
        if (Enemy)
        {
            Destroy(other.gameObject);
        }
    }
}
