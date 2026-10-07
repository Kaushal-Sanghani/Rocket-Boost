using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 10f;
    [SerializeField] float timeToStop = 7f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.left * speed, ForceMode.VelocityChange);
        Invoke(nameof(StopLeftForce), timeToStop);
    }

    void StopLeftForce()
    {   
        if(Time.time >= timeToStop)
        {
            rb.linearVelocity = Vector3.zero;
        }

    }
}
