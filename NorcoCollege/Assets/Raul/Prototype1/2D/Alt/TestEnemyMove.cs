using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyMove : MonoBehaviour
{
    //GameObject player; 
    public Transform target; 
    public float speed = 3f; 
    
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        TargetPlayer(); 
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
    }
}
