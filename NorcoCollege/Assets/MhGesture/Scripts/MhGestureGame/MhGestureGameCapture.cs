using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// in game script which is tracking user input and after its end check if given gesture is similar with knew ones
/// </summary>
public class MhGestureGameCapture : MonoBehaviour {

    private bool capturing;
    private float pointCapturingDistance;
    private List<Vector2> capturedPoints = new List<Vector2>();

    //public PlayerAttack attackScript; 
    public PlayerAttack2D attackScript; 
    //placeholder, may be removed 

	// Use this for initialization
	void Start () {
        if (MhGestureManager.gestures.Count ==0)
        {
            MhGestureManager.LoadGestures();
        }
        // consider changing  MhGestureManager.distanceConstant in case of diferent resolution than 1920x1080
        //current ratio is compromise which should do good result in most cases
        pointCapturingDistance = (Screen.width * Screen.height) / MhGestureManager.distanceConstant;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //handle result
        MhPointPatternMatchResult[] captureResult = GestureCapturing();
        if(captureResult != null)
        {
            Debug.Log("Best Match: " + captureResult[0].Name + " , Probability: " + captureResult[0].Probability);
            attackScript.Attack(captureResult[0].Name);
        }
        

    }

    private MhPointPatternMatchResult[] GestureCapturing()
    {
        if (MhGestureManager.gestureCaptureEnable)
        {
            if (GestureIsDrawing())
            {
                if (capturing)
                {
                    Vector2 potentialNextPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Vector2 lastCapturedPoint = capturedPoints[capturedPoints.Count - 1];
                    while (Vector2.Distance(potentialNextPoint, lastCapturedPoint) >= pointCapturingDistance)
                    {
                        Vector2 direction = potentialNextPoint - lastCapturedPoint;
                        lastCapturedPoint = lastCapturedPoint + (direction / direction.magnitude) * pointCapturingDistance;
                        capturedPoints.Add(lastCapturedPoint);

                    }
                }
                else
                {
                    capturing = true;
                    capturedPoints.Add(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                }

            }
            else
            {
                if (capturing)
                {
                    capturing = false;
                    if (capturedPoints.Count >= MhGestureManager.minNumberOfpoints)
                    {
                        MhPointPatternMatchResult[] result = MhGestureManager.analyzer.GetPointPatternMatchResults(capturedPoints);
                        capturedPoints.Clear();
                        return result;
                    }
                }
            }

        }
        return null;
    }

    private static bool GestureIsDrawing()
    {
        //currently only mouse input , could be extended to mobile input too
        return Input.GetMouseButton(0);
    }
}
