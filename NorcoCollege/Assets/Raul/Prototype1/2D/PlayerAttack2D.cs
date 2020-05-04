using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2D : MonoBehaviour
{
    public Rigidbody2D attack1; 
    public Rigidbody2D attack2; 
    public float attackSpeed = 10f; 

    public float knockback = 1f; 
    public GameObject t; 
    public GameObject d; 
    public GameObject l; 
    public GameObject r; 

    bool isGuard; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(string direction)
    {
        if (direction == "Right")
        {
            Rigidbody2D attackCopy = (Rigidbody2D) Instantiate(attack2, r.transform.position, r.transform.rotation); 
            attackCopy.velocity = transform.right * attackSpeed; 
            Debug.Log("Right");  
        }
        if (direction == "Left")
        {
            Rigidbody2D attackCopy = (Rigidbody2D) Instantiate(attack1, l.transform.position, l.transform.rotation); 
            attackCopy.velocity = (-transform.right) * attackSpeed; 
            Debug.Log("Left");
        }
        if (direction == "Down")
        {
            Rigidbody2D attackCopy = (Rigidbody2D) Instantiate(attack1, d.transform.position, d.transform.rotation); 
            attackCopy.velocity = (-transform.up) * attackSpeed; 
            Debug.Log("Down"); 
        }
        if (direction == "Up")
        {
            Rigidbody2D attackCopy = (Rigidbody2D) Instantiate(attack1, t.transform.position, t.transform.rotation); 
            attackCopy.velocity = transform.up * attackSpeed; 
            Debug.Log("Up");  
        }
        if (direction == "RArrow")
        {
            Rigidbody2D attackCopy = (Rigidbody2D) Instantiate(attack1, r.transform.position, r.transform.rotation); 
            attackCopy.velocity = transform.right * attackSpeed; 
            Debug.Log("Arrow"); 
        }
        if (direction == "Circle")
        {
            isGuard = true; 
            //Debug.Log("We are now guarding!"); 
        }

    }

    void OnTriggerEnter2D(Collider2D col )
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (isGuard == true)
            {
                Debug.Log("Blocked!"); 
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, 0); 
                Debug.Log("We're hit!"); 
            }
        }
    }

    /*void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (isGuard)
            {
                Debug.Log("Blocked!"); 
                
            }
            else
            {
                Debug.Log("Player hit!"); 
            }
            
        }
        //Debug.Log("Hit"); 
    }*/
}
