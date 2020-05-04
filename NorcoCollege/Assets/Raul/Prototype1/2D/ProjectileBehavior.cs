﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float timer = 1f; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyGameObject(); 
    }

    void DestroyGameObject()
    {
        timer -= Time.deltaTime; 
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
         
    }
}