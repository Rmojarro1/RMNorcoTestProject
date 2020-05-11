using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2D : MonoBehaviour
{
    public Rigidbody attack1; 
    //public Rigidbody attack2; 
    public Rigidbody slashV;
    public Rigidbody slashH;  
    public float attackSpeed = 10f; 

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
    //bool moveMode; 

    //public Text test; 
    

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
        if (direction == "Right")
        {
            //Rigidbody2D attackCopy = (Rigidbody2D) Instantiate(attack2, r.transform.position, r.transform.rotation); 
            Rigidbody attackCopy = (Rigidbody) Instantiate(slashV, r.transform.position, r.transform.rotation);
            //attackCopy.velocity = transform.right * attackSpeed; 
            Debug.Log("Right");  
        }
        else if (direction == "Left")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(slashV, l.transform.position, l.transform.rotation); 
            //attackCopy.velocity = (-transform.right) * attackSpeed; 
            Debug.Log("Left");
        }
        else if (direction == "Down")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, d.transform.position, d.transform.rotation); 
            //attackCopy.velocity = (-transform.up) * attackSpeed; 
            Debug.Log("Down"); 
        }
        else if (direction == "Up")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, t.transform.position, t.transform.rotation); 
            //attackCopy.velocity = transform.up * attackSpeed; 
            Debug.Log("Up");  
        }
        else if (direction == "RArrow")
        {
            Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, r.transform.position, r.transform.rotation); 
            attackCopy.velocity = transform.right * attackSpeed; 
            Debug.Log("Arrow"); 
        }
        else if (direction == "Circle")
        {
            isGuard = true; 
            Debug.Log("We are now guarding!"); 
        }
        AtMaxStyle(); 

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
                health--; 
            }
        }
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

    void DestroyGameObject()
    {
         
        if (health <= 0)
        {
            Debug.Log("Player destroyed"); 
            Destroy(gameObject);
        }
         
    }

}
