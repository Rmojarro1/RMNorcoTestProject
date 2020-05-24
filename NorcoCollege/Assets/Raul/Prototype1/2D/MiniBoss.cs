using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public Rigidbody projectile1; 

    public Rigidbody shockwave1; 

    public Rigidbody wideSlashDown; 
    public Rigidbody wideSlashUp; 
    private float timer = 5f; 
    //private float attackSpeed = 10f; 

    public Animator animator; 

    public bool isBlue; 
    public bool isRed; 

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
        isRed = true; 
        animator.SetBool("Red", true); 
    }

    // Update is called once per frame
    void Update()
    { 
        //Attack2();
        Attack3(); 
        //Attack4(); 
        //DestroyGameObject();  
        
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
            StartCoroutine(DownSlash());
            timer = 5f;
        }
         
    }

    IEnumerator DownSlash()
    {
        if (isRed == true)
        {
            animator.SetTrigger("OnRedSwipe");
        }
        else
        {
            animator.SetTrigger("OnBlueSwipe");
        }
        yield return new WaitForSeconds(1.4f);
        Rigidbody enemyProjCopy = (Rigidbody) Instantiate(wideSlashDown, D.transform.position, D.transform.rotation);
        enemyProjCopy.tag = "Enemy"; 
        Debug.Log("Creating slash"); 
        yield return new WaitForSeconds(0.9f);
        if (isRed == true)
        {
            animator.SetTrigger("OnRedSwipe");
        }
        else
        {
            animator.SetTrigger("OnBlueSwipe");
        }
        OnColorChange();
        
        
    }

    void Attack4()
    {
        //creates an upward slash
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            Rigidbody enemyProjCopy = (Rigidbody) Instantiate(wideSlashUp, U.transform.position, U.transform.rotation);
            enemyProjCopy.tag = "Enemy"; 
            timer = 2f;
        }
    }

    void OnColorChange()
    {
        if (isRed == true)
        {
            isRed = false; 
            isBlue = true; 
            animator.SetBool("Blue", true);
            animator.SetBool("Red", false);
        }
        else
        {
            isBlue = false; 
            isRed = true; 
            animator.SetBool("Red", true);
            animator.SetBool("Blue", false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Slash")
        {
            Debug.Log("Hit by Slash"); 
            if (isRed)
            {
                DamageBonus(); 
            }
            else
            {
                Damage(); 
            }
        }
        else if(col.gameObject.tag == "Projectile")
        {
            Damage(); 
            Debug.Log("Hit by projectile"); 
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

    void DamageBonus()
    {
        if (player.GetComponent<PlayerAttack2D>().GetMaxStyle() == true)
        {
            health--; 
            DestroyGameObject(); 
        }
        player.GetComponent<PlayerAttack2D>().IncrementStyleBonus();
    }

    public void Revive()
    {
        health = 5; 
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
