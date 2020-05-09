using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    public Rigidbody projectile; 
    //public Rigidbody proj1; 
    private float timer = 5f; 
    private float attackSpeed = 10f; 

    //public float knockback = 1f; 

    public GameObject player; 
    public GameObject l; 
    public GameObject r; 
    public GameObject t; 
    public GameObject d; 
    public int health = 5; 
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        DestroyGameObject();  
        
    }

    void Attack()
    {
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            /*Rigidbody2D enemyProjCopy = (Rigidbody2D) Instantiate(projectile, l.transform.position, l.transform.rotation); 
            enemyProjCopy.tag = "Enemy"; 
            enemyProjCopy.velocity = (-transform.right) * attackSpeed;
            timer = 5f;*/
            Rigidbody enemyProjCopy = (Rigidbody) Instantiate(projectile, l.transform.position, l.transform.rotation);
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
            //GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, 0); 
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Slash")
        {
            player.GetComponent<PlayerAttack2D>().incrementStyle(); 
            Debug.Log("Hit by Slash"); 
            health--; 
            //GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, 0); 
        }
        else if(col.gameObject.tag == "Projectile")
        {
            //style++; 
            Debug.Log("Hit by projectile"); 
            health--; 
            //GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, 0); 
        }
    }

    void DestroyGameObject()
    {
         
        if (health <= 0)
        {
            Debug.Log("Enemy destroyed"); 
            Destroy(gameObject);
        }
         
    }



    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("Enemy hit!"); 
        }
        Debug.Log("Hit"); 
    }*/
}
