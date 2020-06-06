using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LevelSelect : MonoBehaviour
{
    public GameObject levelButton;
    public Scenes script; 
    // Start is called before the first frame update
    void Start()
    {
        Button btn = levelButton.GetComponent<Button>();
        btn.onClick.AddListener(script.StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
