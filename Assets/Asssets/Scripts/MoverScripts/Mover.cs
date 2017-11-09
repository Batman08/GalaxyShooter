using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Physics.IgnoreLayerCollision(11, 12);
        Physics.IgnoreLayerCollision(11, 13);
        Physics.IgnoreLayerCollision(12, 13);
    }
}
