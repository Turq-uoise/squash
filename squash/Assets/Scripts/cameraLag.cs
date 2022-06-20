using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLag : MonoBehaviour
{
    [HideInInspector] public GameObject cameraFollow;

    public Camera cam;

    Vector3 oldTransform;
    Vector3 newTransform;
    
    void LateUpdate()
    {
        oldTransform = transform.position;
        newTransform = new Vector3(cameraFollow.transform.position.x, 7f, -4f);
        transform.position = Vector3.Lerp(oldTransform, newTransform, 0.005f);
    }
}
