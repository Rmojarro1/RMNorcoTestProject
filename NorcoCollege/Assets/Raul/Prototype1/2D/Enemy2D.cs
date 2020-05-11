using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    public Rigidbody projectile1; 

    public Rigidbody shockwave1; 

    public Rigidbody wideSlashDown; 
    public Rigidbody wideSlashUp; 
    //public Rigidbody proj1; 
    private float timer = 5f; 
    private float attackSpeed = 10f; 

    //public float knockback = 1f; 

    public GameObject player; 
    public GameObject L; 
    public GameObject R; 
    public GameObject U; 
    public GameObject D; 
    public GameObject DL; 
    public GameObject DR; 
    public GameObject UR; 
    public GameObject UL; 
    public int health = 5; 
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        //Attack(); 
        //Attack2();
        Attack3(); 
        Attack4(); 
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
            Rigidbody enemyProjCopy = (Rigidbody) Instantiate(projectile1, L.transform.position, L.transform.rotation);
            enemyProjCopy.tag = "Enemy"; 
            enemyProjCopy.velocity = (-transform.right) * attackSpeed;
            timer = 5f;
        }
    }

    void Attack2()
    {
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            Rigidbody enemyProjCopy1 = (Rigidbody) Instantiate(shockwave1, L.transform.position, L.transform.rotation);
            enemyProjCopy1.tag = "Enemy"; 
            Rigidbody enemyProjCopy2 = (Rigidbody) Instantiate(shockwave1, DL.transform.position, DL.transform.rotation);
            enemyProjCopy2.tag = "Enemy"; 
            Rigidbody enemyProjCopy3 = (Rigidbody) Instantiate(shockwave1, D.transform.position, D.transform.rotation);
            enemyProjCopy3.tag = "Enemy"; 
            Rigidbody enemyProjCopy4 = (Rigidbody) Instantiate(shockwave1, DR.transform.position, DR.transform.rotation);
            enemyProjCopy4.tag = "Enemy"; 
            Rigidbody enemyProjCopy5 = (Rigidbody) Instantiate(shockwave1, R.transform.position, R.transform.rotation);
            enemyProjCopy5.tag = "Enemy"; 
            Rigidbody enemyProjCopy6 = (Rigidbody) Instantiate(shockwave1, UR.transform.position, UR.transform.rotation);
            enemyProjCopy6.tag = "Enemy"; 
            Rigidbody enemyProjCopy7 = (Rigidbody) Instantiate(shockwave1, U.transform.position, U.transform.rotation);
            enemyProjCopy7.tag = "Enemy"; 
            Rigidbody enemyProjCopy8 = (Rigidbody) Instantiate(shockwave1, UL.transform.position, UL.transform.rotation);
            enemyProjCopy8.tag = "Enemy"; 
            timer = 5f;
        }
    }

    void Attack3()
    {
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            Rigidbody enemyProjCopy = (Rigidbody) Instantiate(wideSlashDown, D.transform.position, D.transform.rotation);
            enemyProjCopy.tag = "Enemy"; 
            //enemyProjCopy.velocity = (-transform.right) * attackSpeed;
            timer = 2f;
        }
    }

    void Attack4()
    {
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            Rigidbody enemyProjCopy = (Rigidbody) Instantiate(wideSlashUp, U.transform.position, U.transform.rotation);
            enemyProjCopy.tag = "Enemy"; 
            //enemyProjCopy.velocity = (-transform.right) * attackSpeed;
            timer = 2f;
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
            //player.GetComponent<PlayerAttack2D>().IncrementStyle(); 
            Debug.Log("Hit by Slash"); 
            //health--;
            Damage();  
            //GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, 0); 
        }
        else if(col.gameObject.tag == "Projectile")
        {
            Damage(); 
            Debug.Log("Hit by projectile"); 
            //health--; 
            //GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, 0); 
        }
    }

    void Damage()
    {
        if (player.GetComponent<PlayerAttack2D>().GetMaxStyle() == true)
        {
            health--; 
        }
        player.GetComponent<PlayerAttack2D>().IncrementStyle();
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
