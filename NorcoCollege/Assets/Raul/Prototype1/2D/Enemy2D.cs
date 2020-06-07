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
    //private float attackSpeed = 10f; 

    public Animator animator; 

    public GameObject player; 
    public GameObject L; 
    public GameObject R; 
    public GameObject U; 
    public GameObject D; 
    public GameObject DL; 
    public GameObject DR; 
    public GameObject UR; 
    public GameObject UL;

    public GameObject up;
    public GameObject down; 
    public int health = 1; 
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        Attack(); 
        //Attack2();
        //Attack3(); 
        //Attack4(); 
        //DestroyGameObject();  
        
    }

    void Attack()
    {
        //shoots a projectile in the left direction
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            if (up.GetComponent<AttackTrigger>().ReturnRange() == true)
			{
                StartCoroutine(UpStab());
            }
            else if (down.GetComponent<AttackTrigger>().ReturnRange() == true)
			{
                StartCoroutine(DownStab()); 
			}
            else
			{
                StartCoroutine(LeftStab());
            }
            
            
            //Rigidbody enemyProjCopy = (Rigidbody) Instantiate(projectile1, L.transform.position, L.transform.rotation);
            //enemyProjCopy.tag = "Enemy"; 
            //enemyProjCopy.velocity = (-transform.right) * attackSpeed;
            timer = 5f;
        }
    }

    IEnumerator LeftStab()
    {
        Rigidbody enemyProjCopy = (Rigidbody) Instantiate(projectile1, L.transform.position, L.transform.rotation);
        enemyProjCopy.tag = "Enemy"; 
        animator.SetTrigger("OnSideRightAttack"); 
        yield return new WaitForSeconds(0.8f);
        animator.SetTrigger("OnSideRightAttack");
    }

    IEnumerator DownStab()
    {
        Rigidbody enemyProjCopy = (Rigidbody)Instantiate(projectile1, D.transform.position, D.transform.rotation);
        enemyProjCopy.tag = "Enemy";
        animator.SetTrigger("OnDownAttack");
        yield return new WaitForSeconds(0.8f);
        animator.SetTrigger("OnDownAttack");
    }

    IEnumerator UpStab()
    {
        Rigidbody enemyProjCopy = (Rigidbody)Instantiate(projectile1, U.transform.position, U.transform.rotation);
        enemyProjCopy.tag = "Enemy";
        animator.SetTrigger("OnUpAttack");
        yield return new WaitForSeconds(0.8f);
        animator.SetTrigger("OnUpAttack");
    }

    void Attack2()
    {
        //creates an attack on each spawn around the enemy
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
        //performs a downward slash
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            Rigidbody enemyProjCopy = (Rigidbody) Instantiate(projectile1, L.transform.position, L.transform.rotation);
            enemyProjCopy.tag = "Enemy";
            //enemyProjCopy.MovePosition(player.transform.position); 
            enemyProjCopy.transform.position = Vector3.MoveTowards(enemyProjCopy.transform.position, player.transform.position, 5 ); 
            timer = 3f;
        }
    }

    void Attack4()
    {
        //creates an upward slash
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            Rigidbody enemyProjCopy = (Rigidbody) Instantiate(wideSlashUp, U.transform.position, U.transform.rotation);
            enemyProjCopy.tag = "Enemy"; 
            //enemyProjCopy.velocity = (-transform.right) * attackSpeed;
            timer = 2f;
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
            DestroyGameObject(); 
        }
        player.GetComponent<PlayerAttack2D>().IncrementStyle();
    }

    public void Revive()
    {
        health = 1; 
        //gameObject.SetActive(true); 
    }

    void DestroyGameObject()
    {
         
        if (health <= 0)
        {
            Debug.Log("Enemy destroyed"); 
            //Destroy(gameObject);
            gameObject.SetActive(false); 
        }
         
    }

}
