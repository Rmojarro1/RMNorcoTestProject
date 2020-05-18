using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject boss; 
    public GameObject enemy; 

    //public GameObject bossSpawn; 

    //public GameObject enemySpawn; 
    public bool isBoss; 
    public bool isEnemy; 
    
    // Start is called before the first frame update
    void Start()
    {
        //GameObject bossCopy = (GameObject) Instantiate(boss, bossSpawn.transform.position, bossSpawn.transform.rotation); 
        boss.SetActive(true); 
        enemy.SetActive(false); 
        isBoss = true; 
        //firstBoss = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.activeSelf == false)
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
        }
        
        /*if (firstBoss == false)
        {
            //GameObject bossCopy = (GameObject) Instantiate(boss, bossSpawn.transform.position, bossSpawn.transform.rotation);
        }
        
        if (isBoss)
        {
            if (bossCopy == null)
            //GameObject bossCopy = (GameObject) Instantiate(boss, bossSpawn.transform.position, bossSpawn.transform.rotation);
        }*/
    }

}
