using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject racket;
    public GameObject ball;
    public GameObject hitSpherePrefab;
    public GameObject jumpSphere;
    public GameObject hitpowerSphere;
    public Transform hitPoint;

    [HideInInspector] public Contact contact;
    [HideInInspector] public Contact sphereScript;
    [HideInInspector] public BallScript ballScript;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Rigidbody rbb;

    [HideInInspector] public float jumpingPower;
    [HideInInspector] public float racketPower;
    [HideInInspector] public int rally = 0;
    [HideInInspector] public bool hit = false;
    [HideInInspector] public bool hitAgain = true;
    [HideInInspector] public bool showTutorial;
    [HideInInspector] public bool hitsound;

    private bool isJumping = false;
    private bool jumped = false;
    private float speed;
    private float speedDefault = 7;
    private float gravityMultiplier = 5;
    private Quaternion rotGoal;
    private Quaternion rottGoal;
    private Quaternion racketDefault = Quaternion.Euler(0f, 0f, -60f);

    public AudioClip racketSwing;
    AudioSource audioSource;

    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(-3, 3);
        var y = Random.Range(5, 10);
        var z = Random.Range(10, 30);
        return new Vector3(x, y, z);
    }
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbb = ball.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        ballScript = GameObject.FindObjectOfType(typeof(BallScript)) as BallScript;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L)) { showTutorial = !showTutorial; }

        if (Input.GetKey(KeyCode.LeftShift)) { speed = speedDefault * 2f; }

        else { speed = speedDefault; }

        if (Input.GetKey(KeyCode.D) && transform.position.x < 10.25)
        {
            transform.position += Time.deltaTime * speed * Vector3.right;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -15), 0.05f);
        }

        if (Input.GetKey(KeyCode.A) && transform.position.x > -10.25)
        {
            transform.position += Time.deltaTime * speed * Vector3.left;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 15), 0.05f);

        }

        if (Input.GetKey(KeyCode.W) && transform.position.z < 30)
        {
            transform.position += Time.deltaTime * speed * Vector3.forward;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(15, 0, 0), 0.05f);

        }

        if (Input.GetKey(KeyCode.S) && transform.position.z > 5)
        {
            transform.position += Time.deltaTime * speed * Vector3.back;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-15, 0, 0), 0.05f);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.01f);


        if(Input.GetKey(KeyCode.Space) && isJumping == false)
        {
            jumpingPower = Mathf.Clamp(jumpingPower, 3, 6);
            jumpingPower += Time.deltaTime * 10f;
            jumpSphere.transform.localScale = new Vector3(jumpingPower / 12, jumpingPower / 12, jumpingPower / 12);
            transform.localScale = new Vector3(0.8304f, 1f, 0.8304f);
        }
        
        if (Input.GetKeyUp(KeyCode.Space) && isJumping == false)
        {
            isJumping = true;
            jumped = true;

            jumpSphere.transform.localScale = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(0.8304f, 1.4f, 0.8304f);

            Invoke("JumpReset", 0.72f);
        }

        if (Input.GetKey(KeyCode.H) && hitAgain)
        {
            racketPower = Mathf.Clamp(racketPower, 2, 6);
            racketPower += Time.deltaTime * 10f;
            hitpowerSphere.transform.localScale = new Vector3(racketPower / 12, racketPower / 12, racketPower / 12);

            rottGoal = Quaternion.Euler(0f, 60f, -60f);
            racket.transform.rotation = Quaternion.Slerp(racket.transform.rotation, rottGoal, 0.02f);
        }

        if (Input.GetKeyUp(KeyCode.H) && hitAgain)
        {
            hit = true;
            hitAgain = false;
            hitpowerSphere.transform.localScale = new Vector3(0, 0, 0);

            GameObject hitSphere = Instantiate(hitSpherePrefab, hitPoint);
            sphereScript = hitSphere.GetComponent<Contact>();
            sphereScript.ball = ball;
            Destroy(hitSphere, 0.02f);

            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(racketSwing, 1);
            Invoke("RacketReset", 0.4f);
        }

        // Ball Testing
        if (Input.GetKeyDown(KeyCode.F))
        {
            rbb.velocity = RandomVector(0f, 5f);
            rally = 0;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ball.transform.position = new Vector3(transform.position.x + 1, transform.position.y + 2, transform.position.z + 2);
            rbb.Sleep();
            rally = 0;
            ballScript.bounce = 0;
        }
    }


    private void JumpReset()
    {
        isJumping = false;
        transform.localScale = new Vector3(0.8304f, 1.2456f, 0.8304f);
    }


    private void RacketReset()
    {
        hitAgain = true;
        racket.transform.rotation = racketDefault;
        racketPower = 0;
    }


    void FixedUpdate()
    {
        if (jumped == true)
        { 
            rb.AddForce(Vector3.up * 5 * jumpingPower, ForceMode.Impulse);
            jumped = false;
            jumpingPower = 0;
        }

        if (hit == true)
        {
            rotGoal = Quaternion.Euler(0f, -90f, -60f);
            racket.transform.rotation = Quaternion.Slerp(racket.transform.rotation, rotGoal, 1.0f);
            hit = false;
        }

        rb.AddForce(Physics.gravity * gravityMultiplier);
    }


}
