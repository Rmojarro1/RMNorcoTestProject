using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject boss; 
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject player; 

    public Scenes sceneScript; 

    
    // Start is called before the first frame update
    void Start()
    { 
        //boss.SetActive(true); 
        //enemy1.SetActive(false); 
        //isBoss = true; 
    }

    // Update is called once per frame
    void Update()
    {
        Win();
        GameOver(); 

    }

    void Win()
	{
        if (enemy1.activeSelf == false && enemy2.activeSelf && enemy3.activeSelf && boss.activeSelf)
		{
            Debug.Log("You won!"); 
		}
	}

    void GameOver()
	{
		if (player.activeSelf == false)
		{
            sceneScript.ReloadStage();
        }
        
	}

    /* OLD STUFF if(boss.activeSelf == false)
        {
            if(enemy.activeSelf == false)
            {
                if (isBoss == true)
                {
                    enemy.SetActive(true); 
                    enemy.GetComponent<Enemy2D>().Revive();
                    isBoss = false; 
                    isEnemy = true; 
                }
                
            }
        }
        if(enemy.activeSelf == false)
        {
            if (boss.activeSelf == false)
            {
                if (isEnemy == true)
                {
                    boss.SetActive(true); 
                    boss.GetComponent<MiniBoss>().Revive();
                    isBoss = true; 
                    isEnemy = false; 
                }
                
            }
        } */

}
