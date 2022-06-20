using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [HideInInspector] public PlayerController control;
    [HideInInspector] public int bounce;

    public Transform BallReset;

    private Rigidbody rb;

    public AudioClip ballBounce;
    public AudioClip ballDead;
    AudioSource audioSource;

    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(-5, 5);
        var y = Random.Range(1, 3);
        var z = Random.Range(6, 10);
        return new Vector3(x, y, z);
    }

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        control = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }


    void OnTriggerEnter(Collider other)
    {
       if(other.tag == "BallReset")
        {
            transform.position = RandomVector(0f, 5f);
            rb.Sleep();
            rb.useGravity = false;

            bounce = 0;
            control.rally = 0;

            audioSource.pitch = 0.1f;
            audioSource.PlayOneShot(ballDead);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(ballBounce, 1);
        rb.useGravity = true;

        if (collision.gameObject.tag == "Red") { rb.transform.position = BallReset.transform.position; }

        if (collision.gameObject.tag == "Floor") { bounce += 1; }       

        if (collision.gameObject.tag == "Front Wall") 
        { 
            bounce = 0; 
            if (control.rally >= 5) { rb.AddForce(rb.velocity.x, 0, -control.rally, ForceMode.Impulse); }
        }

        if (bounce >= 2)
        {
            audioSource.pitch = 0.1f;
            audioSource.PlayOneShot(ballDead);

            bounce = 0;
            control.rally = 0;

            transform.position = RandomVector(0f, 5f);
            rb.Sleep();
            rb.useGravity = false;
        }
    }
}
