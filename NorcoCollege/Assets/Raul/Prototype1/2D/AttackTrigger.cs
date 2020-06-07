using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    bool inRange; 
    void Start()
    {
		inRange = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
            inRange = true; 
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			inRange = false;
		}
	}

	public bool ReturnRange()
	{
        return inRange; 
	}
}
