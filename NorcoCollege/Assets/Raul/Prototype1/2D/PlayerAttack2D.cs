﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro; 

public class PlayerAttack2D : MonoBehaviour
{
    public Rigidbody attack1; 
    //public Rigidbody attack2; 
    public Rigidbody slashV;
    public Rigidbody slashH;  
    public float attackSpeed = 10f; 

    public Animator animator; 

    //public float knockback = 1f; 
    public static int style; 
    public int minMaxStyle = 20; 
    private string lastAttack = "";
    private bool maxStyle;  
    public int health = 10; 
    public GameObject t; 
    public GameObject d; 
    public GameObject l; 
    public GameObject r; 

    bool isGuard; 
    bool staleMove; 

    //public PlayerMovement pm; 
    //bool moveMode; 

    //public TextMeshProGUI score; 
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyGameObject(); 
    }

    public void Attack(string direction)
    {
        RecentAttack(direction); 
        
        if (direction == "HalfCR")
        {
            StartCoroutine(RightSlash());
            //Rigidbody2D attackCopy = (Rigidbody2D) Instantiate(attack2, r.transform.position, r.transform.rotation); 
            //Rigidbody attackCopy = (Rigidbody) Instantiate(slashV, r.transform.position, r.transform.rotation);
            //attackCopy.velocity = transform.right * attackSpeed; 
            Debug.Log("RightCR");  
        }
        else if (direction == "HalfCL")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(slashV, l.transform.position, l.transform.rotation); 
            //attackCopy.velocity = (-transform.right) * attackSpeed; 
            Debug.Log("HalfCL");
        }
        else if (direction == "HalfCD")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, d.transform.position, d.transform.rotation); 
            //attackCopy.velocity = (-transform.up) * attackSpeed; 
            Debug.Log("HalfCD"); 
        }
        else if (direction == "HalfCU")
        {
            
            StartCoroutine(UpSlash()); 
            //animator.SetBool("UpSlash", true); 
            animator.SetTrigger("OnUpSlash"); 
            Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, t.transform.position, t.transform.rotation); 
            //attackCopy.velocity = transform.up * attackSpeed; 
            Debug.Log("HalfCU"); 
            //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length+animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            //animator.SetBool("UpSlash", false);
            //animator.ResetTrigger("OnUpSlash");
        }
        else if (direction == "RArrow")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, r.transform.position, r.transform.rotation); 
            attackCopy.velocity = transform.right * attackSpeed; 
            Debug.Log("Arrow"); 
        }
        else if (direction == "Circle")
        {
            StartCoroutine(Guard()); 
            //isGuard = true; 
            //Debug.Log("We are now guarding!"); 
        }
        AtMaxStyle(); 

    }


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
                health--; 
            }
        }
    }

    IEnumerator UpSlash()
    {
        animator.SetTrigger("OnUpSlash"); 
        Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, t.transform.position, t.transform.rotation); 
            //attackCopy.velocity = transform.up * attackSpeed; 
        Debug.Log("HalfCU"); 
        yield return new WaitForSeconds(0.8f);
            //animator.SetBool("UpSlash", false);
        //animator.ResetTrigger("OnUpSlash");
        animator.SetTrigger("OnUpSlash");
        
        /*animator.SetBool("UpSlash", true); 
        Debug.Log("UpSlash: " + animator.GetBool("UpSlash")); 
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length+animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, t.transform.position, t.transform.rotation); 
        //attackCopy.velocity = transform.up * attackSpeed; 
        Debug.Log("HalfCU"); 
        Debug.Log("After Yield UpSlash: " + animator.GetBool("UpSlash"));
        animator.SetBool("UpSlash", false);
        Debug.Log("End UpSlash: " + animator.GetBool("UpSlash"));*/
    }

    IEnumerator RightSlash()
    {
        animator.SetTrigger("OnSideSlash"); 
        Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, r.transform.position, r.transform.rotation);  
        Debug.Log("Right"); 
        yield return new WaitForSeconds(0.7f);
        animator.SetTrigger("OnSideSlash");
    }

    private void RecentAttack(string direction)
    {
        if (lastAttack == direction)
        {
            staleMove = true; 
        }
        else
        {
            lastAttack = direction;
            staleMove = false; 
        }
    }

    private void AtMaxStyle()
    {
        if (style >= minMaxStyle)
        {
            maxStyle = true; 
            Debug.Log("Max style"); 
        }
        else 
        {
            maxStyle = false; 
        }
    }

    public bool GetMaxStyle()
    {
        return maxStyle; 
    }

    public void IncrementStyle()
    {
        if (staleMove == true)
        {
            style++; 
            //Debug.Log(style); 
        }
        else
        {
            style += 5; 
        }
        //test.text = style.ToString(); 
    }

    IEnumerator Guard()
    {
        isGuard = true; 
        Debug.Log("Guarding"); 
        yield return new WaitForSeconds(1.0f);
        isGuard = false; 
        Debug.Log("Not guarding");
    }

    void DestroyGameObject()
    {
         
        if (health <= 0)
        {
            Debug.Log("Player destroyed"); 
            Destroy(gameObject);
        }
         
    }

}
