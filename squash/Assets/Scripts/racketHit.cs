using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racketHit : MonoBehaviour
{
    AudioSource audioSource;
    public PlayerController control;
    public AudioClip racketHitsound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        control = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }

    // Update is called once per frame
    void Update()
    {
        if (control.hitsound == true)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(racketHitsound,1);
            control.hitsound = false;
        }
    }
}
