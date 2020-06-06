using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PlayerAttack2D : MonoBehaviour
{
    public TextMeshProUGUI displayHealth;
    public TextMeshProUGUI displayStyle;

    public Rigidbody attack1;
    public Rigidbody slashV;
    public Rigidbody slashH;
    public float attackSpeed = 10f;

    public Animator animator;

    public static int style = 0;
    public static int revertNormal; 
    public int minMaxStyle = 20;
    private string lastAttack = "";
    private bool maxStyle;
    public int health = 10;
    public GameObject t;
    public GameObject d;
    public GameObject l;
    public GameObject r;

    bool isGuard = false;
    bool staleMove = false;

    Vector3 nextPosition; 
    Vector3 destination;

    private bool canMove;
    bool moveMode; 
    bool attackMode; 
    bool magicMode; 
    public SpriteRenderer sprite;
    public Rigidbody rb;
    
    public GameObject leftBlock;
    public GameObject rightBlock;
    public GameObject upBlock;
    public GameObject downBlock; 

    bool isBlocked; 

    
    // Start is called before the first frame update
    void Start()
    {
        moveMode = true; 
        nextPosition = Vector3.up;
        destination = transform.position;
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        DestroyGameObject();
        ModeChange(); 
        //displayHealth.text = "Health: " + health;
        //displayStyle.text = "Style: " + style + "/20";
        //Move(); 
    }

    void ModeChange()
    {
        if (Input.GetKeyDown("w"))
        {
            moveMode = true; 
            attackMode = false; 
            magicMode = false; 
        }
        else if (Input.GetKeyDown("a"))
        {
            moveMode = false; 
            attackMode = true; 
            magicMode = false; 
        }
        else if (Input.GetKeyDown("d"))
        {
            moveMode = false; 
            attackMode = false; 
            magicMode = true; 
        }
    }

    public void Attack(string direction)
    {
        RecentAttack(direction); 
        if (direction == "Right" || direction == "Left" || direction == "Up" || direction == "Down")
        {
            if (moveMode == true)
            {
                MoveG(direction);
            } 
        }
        else if (direction == "HalfCR")
        {
            if (attackMode == true)
            {
                StartCoroutine(RightSlash());
                //Rigidbody2D attackCopy = (Rigidbody2D) Instantiate(attack2, r.transform.position, r.transform.rotation); 
                //Rigidbody attackCopy = (Rigidbody) Instantiate(slashV, r.transform.position, r.transform.rotation);
                //attackCopy.velocity = transform.right * attackSpeed; 
                Debug.Log("RightCR");
            }  
        }
        else if (direction == "HalfCL")
        {
            if (attackMode == true)
            {
                StartCoroutine(LeftSlash());
                //Rigidbody attackCopy = (Rigidbody) Instantiate(slashV, l.transform.position, l.transform.rotation); 
                //attackCopy.velocity = (-transform.right) * attackSpeed; 
                //Debug.Log("HalfCL");
            }
        }
        else if (direction == "HalfCD")
        {
            if (attackMode == true)
            {
                StartCoroutine(DownSlash()); 
                //Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, d.transform.position, d.transform.rotation); 
                //attackCopy.velocity = (-transform.up) * attackSpeed; 
                //Debug.Log("HalfCD"); 
            }
        }
        else if (direction == "HalfCU")
        {
            if (attackMode == true)
            {
                StartCoroutine(UpSlash()); 
                //animator.SetBool("UpSlash", true); 
                //animator.SetTrigger("OnUpSlash"); 
                //Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, t.transform.position, t.transform.rotation); 
                //attackCopy.velocity = transform.up * attackSpeed; 
                //Debug.Log("HalfCU"); 
                //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length+animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                //animator.SetBool("UpSlash", false);
                //animator.ResetTrigger("OnUpSlash");
            }
        }
        else if (direction == "LightR")
        {
            if (magicMode == true)
            {
                StartCoroutine(RightMagic());
                //Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, r.transform.position, r.transform.rotation); 
                //attackCopy.velocity = transform.right * attackSpeed; 
                //Debug.Log("Magic");
            } 
        }
        else if (direction == "LightU")
        {
            if (magicMode == true)
            {
                StartCoroutine(UpMagic()); 
            }
        }
        else if (direction == "LightL")
        {
            if (magicMode == true)
            {
                StartCoroutine(LeftMagic());
                //Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, l.transform.position, l.transform.rotation); 
                //attackCopy.velocity = (-transform.right) * attackSpeed; 
            }
        }
        else if (direction == "LightD")
        {
            if (magicMode == true)
            {
                StartCoroutine(DownMagic());
                //Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, d.transform.position, d.transform.rotation); 
                //attackCopy.velocity = (-transform.up) * attackSpeed; 
            }
        }
        else if (direction == "Guard")
        {
            StartCoroutine(Guard()); 
            //isGuard = true; 
            //Debug.Log("We are now guarding!"); 
        }
        if (maxStyle != true)
        {
            AtMaxStyle();
        } 

    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, attackSpeed * Time.deltaTime); 
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            nextPosition = new Vector3(0, 0.1f, 0); 
            //currentDirection = up; 
            canMove = true; 
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            nextPosition = new Vector3(0, -0.1f, 0); 
            //currentDirection = down;
            canMove = true; 
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            nextPosition = new Vector3(0.1f, 0, 0); 
            //currentDirection = right; 
            canMove = true; 
        }
            
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            nextPosition = new Vector3(-0.1f, 0, 0); 
            //currentDirection = left; 
            canMove = true; 
        }

        if (Vector3.Distance(destination, transform.position) <= 0.00001f)
        {
            //transform.localEulerAngles = currentDirection; 
            if (canMove)
            {
                //if(Valid())
                //{
                    destination = transform.position + nextPosition; 
                    //direction = nextPosition; 
                    canMove = false; 
                //}
                
            }
        }
    }

    void MoveG(string direction)
    {
        string move = direction; 
        //transform.position = Vector3.MoveTowards(transform.position, destination, 1f); 
        if (move == "Up")
        {
            nextPosition = new Vector3(0, 1, 0);
            if (upBlock.GetComponent<MoveTrigger>().IsBlocked() == false)
            {
                canMove = true;
            }
            //canMove = true; 
        }

        else if (move == "Down")
        {
            nextPosition = new Vector3(0, -1, 0);
            if (downBlock.GetComponent<MoveTrigger>().IsBlocked() == false)
            {
                canMove = true;
            }
            //canMove = true; 
        }
        else if (move == "Right")
        {
            nextPosition = new Vector3(1, 0, 0);
            if (rightBlock.GetComponent<MoveTrigger>().IsBlocked() == false)
            {
                canMove = true;
            } 
        }
            
        else if (move == "Left")
        {
            nextPosition = new Vector3(-1, 0, 0); 
            if (leftBlock.GetComponent<MoveTrigger>().IsBlocked() == false)
			{
                canMove = true;
            }
            
        }

        if (Vector3.Distance(destination, transform.position) <= 0.00001f)
        {
            //transform.localEulerAngles = currentDirection; 
            if (canMove)
            {
                //if(Valid())
                //{
                    destination = transform.position + nextPosition; 
                    canMove = false; 
                //}
                
            }
        }
        //transform.position = Vector3.MoveTowards(transform.position, destination, 1f);
        rb.MovePosition(destination); 
        
        
        

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

    IEnumerator LeftSlash()
    {
        sprite.flipX = true; 
        animator.SetTrigger("OnSideSlash"); 
        Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, l.transform.position, l.transform.rotation);  
        Debug.Log("Left"); 
        yield return new WaitForSeconds(0.7f);
        animator.SetTrigger("OnSideSlash");
        sprite.flipX = false; 
    }

    IEnumerator DownSlash()
    {
        animator.SetTrigger("OnDownSlash"); 
        Rigidbody attackCopy = (Rigidbody) Instantiate(slashH, d.transform.position, d.transform.rotation);  
        Debug.Log("Down"); 
        yield return new WaitForSeconds(0.7f);
        animator.SetTrigger("OnDownSlash");
    }

    IEnumerator RightMagic()
    {
        animator.SetTrigger("OnSideMagic");
        Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, r.transform.position, r.transform.rotation); 
        attackCopy.velocity = transform.right * attackSpeed; 
        Debug.Log("Magic");
        yield return new WaitForSeconds(1.0f);
        animator.SetTrigger("OnSideMagic");
    }

    IEnumerator LeftMagic()
    {
        sprite.flipX = true; 
        animator.SetTrigger("OnSideMagic");
        Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, r.transform.position, r.transform.rotation); 
        attackCopy.velocity = (-transform.right) * attackSpeed; 
        Debug.Log("Left magic");
        yield return new WaitForSeconds(1.0f);
        animator.SetTrigger("OnSideMagic");
        sprite.flipX = false; 
    }

    IEnumerator UpMagic()
    {
        animator.SetTrigger("OnUpMagic");
        Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, t.transform.position, t.transform.rotation); 
        attackCopy.velocity = transform.up * attackSpeed; 
        Debug.Log("UpMagic");
        yield return new WaitForSeconds(1.0f);
        animator.SetTrigger("OnUpMagic");
    }

    IEnumerator DownMagic()
    {
        animator.SetTrigger("OnDownMagic");
        Rigidbody attackCopy = (Rigidbody) Instantiate(attack1, d.transform.position, d.transform.rotation); 
        attackCopy.velocity = (-transform.up) * attackSpeed; 
        Debug.Log("DownMagic");
        yield return new WaitForSeconds(1.0f);
        animator.SetTrigger("OnDownMagic");
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
            //health = 2; 
            animator.SetBool("AtMaxStyle", true); 
            animator.SetTrigger("MaxStyle"); 
            Debug.Log("Max style"); 
            MaxStyleEnd(); 
        }
        else 
        {
            maxStyle = false; 
            animator.SetBool("AtMaxStyle", false);
        }
        Debug.Log("Style: " + style); 
    }

    public void MaxStyleEnd()
    {
        if (revertNormal >= 3)
        {
            style = 0; 
            revertNormal = 0; 
            AtMaxStyle(); 
        }
        else
        {
            revertNormal++;
        }
        //revertNormal++; 
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
            style += 2; 
        }
        //test.text = style.ToString(); 
    }

    public void IncrementStyleBonus()
    {
        if (staleMove == true)
        {
            style += 1; 
            //Debug.Log(style); 
        }
        else
        {
            style += 3; 
        }
        //test.text = style.ToString(); 
    }

    IEnumerator Guard()
    {
        isGuard = true; 
        animator.SetTrigger("OnSideBlock");
        Debug.Log("Guarding"); 
        yield return new WaitForSeconds(1.0f);
        animator.SetTrigger("OnSideBlock");
        isGuard = false; 
        Debug.Log("Not guarding");
    }

    void DestroyGameObject()
    {
         
        if (health <= 0)
        {
            Debug.Log("Player destroyed"); 
            //Destroy(gameObject);
            gameObject.SetActive(false); 
        }
    }


}