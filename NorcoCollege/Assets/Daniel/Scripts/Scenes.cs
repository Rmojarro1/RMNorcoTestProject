﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenes : MonoBehaviour
{
    //public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        //Button btn = startButton.GetComponent<Button>();
        //btn.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReloadStage()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString()); 
	}
}