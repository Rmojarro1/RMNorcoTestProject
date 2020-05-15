using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb; 
    public float speed = 20f; 

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
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical"); 

        Vector3 tempVect = new Vector3(x, y, 0);

        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);

        /*if (input != Vector3.zero)
        {
            transform.forward = input; 
        }*/
    }

    /*void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime); 
    }*/
}
