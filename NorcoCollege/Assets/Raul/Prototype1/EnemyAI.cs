﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);  
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyGameObject(); 
    }
}