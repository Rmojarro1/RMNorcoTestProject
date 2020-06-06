using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    bool blocked; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider col)
	{
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "EnemyBody")
        {
            blocked = true;
        }
    }

	private void OnTriggerExit(Collider col)
	{
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "EnemyBody")
        {
            blocked = false;
        }
    }

	public bool IsBlocked()
	{
        return blocked; 
	}
}
