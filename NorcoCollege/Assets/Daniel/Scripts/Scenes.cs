using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public GameObject startButton;
    public static int sceneCountInBuildSettings;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(sceneCountInBuildSettings);
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseDown(startButton);
    }

    public void OnMouseDown(GameObject st)
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("New Prototype Level");
        }
    }
}