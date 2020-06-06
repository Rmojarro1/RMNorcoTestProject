using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyMove : MonoBehaviour
{
    //GameObject player; 
    public Transform target; 
    public float speed = 3f; 

    bool isClose;
    bool onStart;

    public GameObject es; 
    //public EnemySense es; 

    //public Rigidbody rb; 
    
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player"); 
        //onStart = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (es.GetComponent<EnemySense>().getStart() == false)
		{
            
            if (es.GetComponent<EnemySense>().getClose() != true)
            {
                TargetPlayer();
                //Debug.Log("We should be moving"); 
            }
        }

    }

    void TargetPlayer()
    {
        //transform.LookAt (target.position);
        //transform.LookAt (target.position);
        //transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
        
        //transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
        //transform.Translate (target.position);

        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step/2);
        //rb.MovePosition(target.position * step); 
    }

    /*void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (onStart == true)
			{
                onStart = false;
                //Debug.Log("We should be active now"); 
			}
            isClose = true; 
            Debug.Log("Player in range"); 
        }
    }

    void OnTriggerExit(Collider col)
    {
        isClose = false; 
        Debug.Log("Player not in range");
    }*/
}
