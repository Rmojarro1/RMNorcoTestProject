using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 

    public float camSpeed = .5f; 
    public Vector3 offset; 
    private Vector3 velocity = Vector3.zero; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset; 
        Vector3 cameraTransition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, camSpeed); 
        transform.position = cameraTransition; 

        transform.LookAt(target); 
    }
}
