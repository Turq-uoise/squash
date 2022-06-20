using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contact : MonoBehaviour
{
    public GameObject ball;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public PlayerController control;

    private Vector3 pos;
    

    void Start()
    {
        rb = ball.GetComponent<Rigidbody>();
        control = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }


    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 pos = -contact.normal;
        pos.y = Mathf.Abs(pos.y);
        pos.z = Mathf.Abs(pos.z) + 1f;
        control.hitsound = true;
        rb.velocity = pos * 3 * control.racketPower;
        rb.AddForce(0, control.racketPower, 5, ForceMode.Impulse);
        control.rally += 1;
    }

}
