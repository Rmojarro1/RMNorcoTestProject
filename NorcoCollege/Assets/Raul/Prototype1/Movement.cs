using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 up = Vector3.zero; 
    Vector3 right = new Vector3(0, 90,0); 
    Vector3 down = new Vector3(0, 180, 0); 
    Vector3 left = new Vector3(0, 270, 0); 
    Vector3 currentDirection = Vector3.zero; 

    Vector3 nextPosition; 
    Vector3 destination; 
    Vector3 direction; 
    
    private float speed = 10f; 
    private bool canMove; 
    
    // Start is called before the first frame update
    void Start()
    {
        //currentDirection = up; 
        nextPosition = Vector3.up;
        destination = transform.position;  
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime); 
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            nextPosition = Vector3.up; 
            //currentDirection = up; 
            canMove = true; 
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            nextPosition = Vector3.down; 
            //currentDirection = down;
            canMove = true; 
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            nextPosition = Vector3.right; 
            //currentDirection = right; 
            canMove = true; 
        }
            
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            nextPosition = Vector3.left; 
            //currentDirection = left; 
            canMove = true; 
        }

        if (Vector3.Distance(destination, transform.position) <= 0.00001f)
        {
            //transform.localEulerAngles = currentDirection; 
            if (canMove)
            {
                //if(Valid())
                //{
                    destination = transform.position + nextPosition; 
                    //direction = nextPosition; 
                    canMove = false; 
                //}
                
            }
        }
    }

    /*bool Valid()
    {
        Ray myRay = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward); 
        RaycastHit hit; 

        Debug.DrawRay(myRay.origin, myRay.direction, Color.red); 
        if (Physics.Raycast(myRay, out hit))
        {
            if(hit.collider.tag == "Wall")
            {
                return false; 
            }
        }
        return true; 

    }*/
}
