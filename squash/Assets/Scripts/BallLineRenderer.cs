using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLineRenderer : MonoBehaviour
{
    private bool rendered;
    public Renderer rend;


    void Start() { rend = GetComponent<Renderer>(); }

    
    void Update()
    {
        rend.enabled = rendered;
        if (Input.GetKeyDown(KeyCode.T)) { rendered = !rendered; }
    }
}
