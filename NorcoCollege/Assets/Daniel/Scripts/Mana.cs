using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float maxMana = 10.0f;
    public float currentMana = 5.0f;
    public float manaRegen = 0.1f;
    public float regenTimerTick = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMana();

        // Debug.Log(currentMana);      Just to show current mana until U.I. is implemented.
    }

    void PlayerMana()                   // Increases player's mana by 0.1 point every 1.5 seconds until their current mana pool is the same as the maximum.
    {
        if (currentMana <= maxMana)
        {
            regenTimerTick -= Time.deltaTime;
            if (regenTimerTick < 0)
            {
                currentMana += manaRegen;
                regenTimerTick = 1.5f;
            }
        }
    }
}