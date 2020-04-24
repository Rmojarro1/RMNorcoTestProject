using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
        private Rigidbody rb; 
    public float speed = 5f; 

    private Vector3 input = Vector3.zero; 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Vector3.zero; 
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical"); 

        if (input != Vector3.zero)
        {
            transform.forward = input; 
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime); 
    }

}
