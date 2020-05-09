using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2D : MonoBehaviour
{
    public Rigidbody attack1; 
    //public Rigidbody attack2; 
    public Rigidbody slash; 
    public float attackSpeed = 10f; 

    public float knockback = 1f; 
    public static int style; 
    public GameObject t; 
    public GameObject d; 
    public GameObject l; 
    public GameObject r; 

    bool isGuard; 
    bool moveMode; 

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
            //Rigidbody2D attackCopy = (Rigidbody2D) Instantiate(attack2, r.transform.position, r.transform.rotation); 
            Rigidbody attackCopy = (Rigidbody) Instantiate(slash, r.transform.position, r.transform.rotation);
            attackCopy.velocity = transform.right * attackSpeed; 
            Debug.Log("Right");  
        }
        if (direction == "Left")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, l.transform.position, l.transform.rotation); 
            attackCopy.velocity = (-transform.right) * attackSpeed; 
            Debug.Log("Left");
        }
        if (direction == "Down")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, d.transform.position, d.transform.rotation); 
            attackCopy.velocity = (-transform.up) * attackSpeed; 
            Debug.Log("Down"); 
        }
        if (direction == "Up")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, t.transform.position, t.transform.rotation); 
            attackCopy.velocity = transform.up * attackSpeed; 
            Debug.Log("Up");  
        }
        if (direction == "RArrow")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, r.transform.position, r.transform.rotation); 
            attackCopy.velocity = transform.right * attackSpeed; 
            Debug.Log("Arrow"); 
        }
        if (direction == "Circle")
        {
            isGuard = true; 
            Debug.Log("We are now guarding!"); 
        }

    }

    /*void OnTriggerEnter2D(Collider2D col )
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (isGuard == true)
            {
                Debug.Log("Blocked!"); 
            }
            else
            {
                //GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, 0); 
                Debug.Log("We're hit!"); 
            }
        }
    }*/

    void OnTriggerEnter(Collider col )
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (isGuard == true)
            {
                Debug.Log("Blocked!"); 
            }
            else
            {
                //GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, 0); 
                Debug.Log("We're hit!"); 
            }
        }
    }

    public void incrementStyle()
    {
        style++; 
        Debug.Log(style); 
    }

}
