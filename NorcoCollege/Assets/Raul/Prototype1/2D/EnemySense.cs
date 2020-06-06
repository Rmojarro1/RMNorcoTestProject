using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySense : MonoBehaviour
{
    // Start is called before the first frame update

    bool onStart;
    bool isClose; 

    void Start()
    {
        onStart = true; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
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
        if (col.gameObject.tag == "Player")
		{
            isClose = false;
            Debug.Log("Player not in range");
        }
        
    }

    public bool getStart()
	{
        return onStart; 
	}

    public bool getClose()
	{
        return isClose; 
	}
}
