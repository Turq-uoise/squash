using UnityEngine;

public class Speaker : MonoBehaviour
{
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = true;
        rb.AddForce(0, -5, 0);
        Destroy(gameObject, 5);
    }

}
