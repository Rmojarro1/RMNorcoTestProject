using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    public Rigidbody2D projectile; 
    private float timer = 5f; 
    private float attackSpeed = 10f; 

    public float knockback = 1f; 
    public GameObject l; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack(); 
    }

    void Attack()
    {
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            Rigidbody2D enemyProjCopy = (Rigidbody2D) Instantiate(projectile, l.transform.position, l.transform.rotation); 
            enemyProjCopy.tag = "Enemy"; 
            enemyProjCopy.velocity = (-transform.right) * attackSpeed;
            timer = 5f; 
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Slash")
        {
            Debug.Log("Hit by Slash"); 
            GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, 0); 
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("Enemy hit!"); 
        }
        Debug.Log("Hit"); 
    }
}
