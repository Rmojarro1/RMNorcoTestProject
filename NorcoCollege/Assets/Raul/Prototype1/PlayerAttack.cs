using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Rigidbody attack1; 
    public float attackSpeed = 10f; 
    public GameObject t; 
    public GameObject d; 
    public GameObject l; 
    public GameObject r; 
    
    
    
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
            Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, r.transform.position, r.transform.rotation); 
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
            attackCopy.velocity = (-transform.forward) * attackSpeed; 
            Debug.Log("Down"); 
        }
        if (direction == "Up")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, t.transform.position, t.transform.rotation); 
            attackCopy.velocity = transform.forward * attackSpeed; 
            Debug.Log("Up");  
        }

    }
}
