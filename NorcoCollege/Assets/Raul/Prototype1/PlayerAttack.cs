using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Rigidbody attack1; 
    public float attackSpeed = 10f; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, transform.position, transform.rotation); 
        attackCopy.velocity = transform.forward * attackSpeed; 
        Debug.Log("Attempting to attack now");  
    }
}
