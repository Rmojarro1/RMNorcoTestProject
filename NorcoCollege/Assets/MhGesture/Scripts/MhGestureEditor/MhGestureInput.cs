using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MhGestureInput : MonoBehaviour
{

    public Vector2 lastVector;
    public MhGesturePoint lastGestPoint;
    public GameObject spritePoint;
    public GameObject parrentGesturePoint;
    public MhGesturePoint firstPoint;
    public float maxDistance;
    public bool drawing;
    public int pointCount = 0;

    public Vector2 lastMousemovePosition;//for moving symbol




    // Use this for initialization
    void Start()
    {
        maxDistance = Screen.width * Screen.height / MhGestureManager.distanceConstant;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift)))
        {
            drawing = true;
            Vector2 potentialLastVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (lastVector == Vector2.zero)
            {
                Vector3 pos = new Vector3();
                Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                pos.x = mousePosWorld.x;
                pos.y = mousePosWorld.y;

                lastGestPoint = InstancePoint(pos);

                lastVector = potentialLastVector;
                firstPoint = lastGestPoint;

            }
            else if (Vector2.Distance(lastVector, potentialLastVector) >= maxDistance)
            {

                while (Vector2.Distance(lastVector, potentialLastVector) >= maxDistance)
                {
                    Vector2 direction = potentialLastVector - lastVector;
                    Vector2 newPosition = lastVector + (direction / direction.magnitude) * maxDistance;// normalized direction * wanted direction;
                    Vector3 pos = new Vector3();
                    pos.x = newPosition.x;
                    pos.y = newPosition.y;

                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pos);
                    worldPosition.z = 0;

                    MhGesturePoint nextPoint = InstancePoint(worldPosition);

                    lastGestPoint.SetNextPoint(nextPoint);
                    lastGestPoint = nextPoint;
                    lastVector = newPosition;
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        break;
                    }

                }


            }
        }
        if (Input.GetMouseButton(1))
        {
            if (lastMousemovePosition == Vector2.zero)
            {
                Vector3 curentWord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lastMousemovePosition = new Vector2(curentWord.x, curentWord.y);
            }
            else
            {
                Vector3 curentWord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 newPosition = new Vector2(curentWord.x, curentWord.y);
                Vector2 move = newPosition - lastMousemovePosition;
                lastMousemovePosition = newPosition;
                MhGesturePoint pointChecked = firstPoint;
                while (pointChecked != null)
                {
                    pointChecked.transform.position = new Vector3(pointChecked.transform.position.x + move.x, pointChecked.transform.position.y + move.y, pointChecked.transform.position.z);
                    pointChecked = pointChecked.after;
                }
            }

        }
        else
        {
            lastMousemovePosition = Vector2.zero;
        }


        if (drawing && !Input.GetMouseButton(0))
        {
            drawing = false;

            //drawwing stop
        }

    }

    private MhGesturePoint InstancePoint(Vector3 worldPosition)
    {
        pointCount++;
        MhGesturePoint newPoint = Instantiate(spritePoint, worldPosition, new Quaternion(), parrentGesturePoint.transform).GetComponent<MhGesturePoint>();
        //Little magic for coloured change to help keep track of direction
        newPoint.GetComponent<SpriteRenderer>().color = new Color32((byte)(255 - (byte)Mathf.Abs(255 - (pointCount % 255))), (byte)(255 - Mathf.Abs(255 - (pointCount * 2 % 255))), 255, 255);
        return newPoint;
    }

    public void DrawSymbol(MhGesture gesture)
    {
        foreach (Vector2 v in gesture.points)
        {
            Vector3 v3screen = new Vector3(v.x, v.y);
            Vector3 v3Word = Camera.main.ScreenToWorldPoint(v3screen);

            Vector3 pos = new Vector3();

            pos.x = v3Word.x;
            pos.y = v3Word.y;
            MhGesturePoint newPoint = InstancePoint(pos);
            if (lastVector == Vector2.zero)
            {
                lastVector = v;
                firstPoint = newPoint;
                lastGestPoint = newPoint;
            }
            else
            {
                lastGestPoint.SetNextPoint(newPoint);
                lastVector = v;
                lastGestPoint = newPoint;
            }


        }
    }

    public void ClearSymbol()
    {
        foreach (Transform child in parrentGesturePoint.transform)
        {
            Destroy(child.gameObject);
        }
        firstPoint = null;
        lastVector = new Vector2();
        pointCount = 0;
    }


    public MhGesture GetCurrentgesture()
    {
        MhGesture gesture = new MhGesture();
        MhGesturePoint point = firstPoint;
        while (point != null)
        {
            Vector3 currentScreenPos = Camera.main.WorldToScreenPoint(point.transform.position);
            Vector2 currentScreenPos2D = new Vector2(currentScreenPos.x, currentScreenPos.y);
            point = point.after;
            gesture.Points.Add(currentScreenPos2D);
        }
        return gesture;

    }


    public KeyValuePair<MhGesture, float> CompareGesture()
    {
        MhGesture currentGesture = GetCurrentgesture();
        var results = MhGestureManager.analyzer.GetPointPatternMatchResults(currentGesture.Points);
        var results2 = MhGestureManager.analyzer.GetPointPatternMatchResultsAdv(currentGesture.Points);
        if (results.Length == 0)
            return new KeyValuePair<MhGesture, float>();

        var topResult = results[0];

        Debug.Log("Best Match: " + topResult.Name + " , Probability: " + topResult.Probability);
        Debug.Log("Advance Comparison Best Match: " + results2[0].Name + " , Probability: " + results2[0].Probability);

        return new KeyValuePair<MhGesture, float>(MhGestureManager.FindGesture(topResult.Name), (float)topResult.Probability);
    }

}
