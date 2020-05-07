using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject line; 
    public GameObject currentLine; 
    public GameObject cloneLine; 

    public LineRenderer lineRenderer; 
    public List<Vector2> mousePositions; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0))
        {
            
            startLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, mousePositions[mousePositions.Count - 1]) > .1f)
            {
                updateLine(tempFingerPos);
            }
            
        } 
        if (Input.GetMouseButtonUp(0))
        {
            if (cloneLine != null)
            {
                DestroyGameObject();
            }
        }
    }

    void startLine()
    {
        //currentLine = Instantiate(line, Vector3.zero, Quaternion.identity);
        cloneLine = Instantiate(line, Vector3.zero, Quaternion.identity) as GameObject; 
        currentLine = cloneLine; 
        lineRenderer = currentLine.GetComponent<LineRenderer>(); 
        mousePositions.Clear(); 
        mousePositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition)); 
        mousePositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, mousePositions[0]); 
        lineRenderer.SetPosition(1, mousePositions[1]); 
    }

    void updateLine(Vector2 newMousePos)
    {
        mousePositions.Add(newMousePos); 
        lineRenderer.positionCount++; 
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newMousePos);
    }

    void DestroyGameObject()
    {
        GameObject temp = cloneLine; 
        cloneLine = null; 
        Destroy(temp); 
    }
}
