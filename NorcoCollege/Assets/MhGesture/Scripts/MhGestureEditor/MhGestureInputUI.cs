using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this class should not interact with gameobject in world
/// </summary>
public class MhGestureInputUI : MonoBehaviour
{

    public MhGestureInput gestureInput;
    public InputField inputField;
    public Dropdown dropDownList;
    public GameObject SymbolPointStart;
    public Slider recalculatePointNumber;
    public Text recalculatePointLabel;
    // Use this for initialization
    void Start()
    {
        LoadSymbols();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClearSymbol()
    {
        gestureInput.ClearSymbol();
    }


    public void SaveGesture()
    {
        MhGesture gesture = gestureInput.GetCurrentgesture();
        gesture.Name = inputField.text;
        MhGestureManager.AddGesture(gesture);
        MhGestureManager.SaveGestures();
        LoadSymbols();
    }

    private void LoadSymbols()
    {
        dropDownList.ClearOptions();
        MhGestureManager.LoadGestures();
        List<string> symbolsName = MhGestureManager.gestures.ConvertAll(x => x.name);
        dropDownList.AddOptions(symbolsName);
    }

    public void DrawSymbol()
    {
        ClearSymbol();
        if (dropDownList.options.Count > 0)
        {
            string symbolName = dropDownList.options[dropDownList.value].text;
            MhGesture gest = MhGestureManager.FindGesture(symbolName);
            if (gest == null)
            {
                Debug.LogError("Symbol with name " + symbolName + " was not found");
                return;
            }
            gestureInput.DrawSymbol(gest);
        }

    }

    public void RemoveSelectedSymbol()
    {
        string symbolName = dropDownList.options[dropDownList.value].text;
        MhGestureManager.RemoveGesture(symbolName);
        dropDownList.ClearOptions();
        List<string> symbolsName = MhGestureManager.gestures.ConvertAll(x => x.name);
        dropDownList.AddOptions(symbolsName);
        MhGestureManager.SaveGestures();
    }

    public void CompareSymbol()
    {
        gestureInput.CompareGesture();
    }

    public void UpdatedSelectedSymbol()
    {
        string symbolName = dropDownList.options[dropDownList.value].text;
        MhGestureManager.RemoveGesture(symbolName);
        MhGesture gesture = gestureInput.GetCurrentgesture();
        gesture.Name = symbolName;
        MhGestureManager.AddGesture(gesture);
        MhGestureManager.SaveGestures();
    }


    public void RecalculatePointNumber()
    {
        string symbolName = dropDownList.options[dropDownList.value].text;
        MhGesture gest = MhGestureManager.FindGesture(symbolName);

        var interpolatedPointArray = MhPointPatternMath.GetInterpolatedPointArray(gest.points, (int)recalculatePointNumber.value, gest);
        gest.points = new List<Vector2>(interpolatedPointArray);
        ClearSymbol();
        DrawSymbol();
    }

    public void RecalculatePointValueChanged()
    {
        recalculatePointLabel.text = "Interpolate to points: " + (int)recalculatePointNumber.value;
    }

    public void DeleteLastPoint()
    {
        if(gestureInput.lastGestPoint == gestureInput.firstPoint)
        {
            gestureInput.ClearSymbol();
        }else
        {
            MhGesturePoint lastPoint = gestureInput.lastGestPoint;
            MhGesturePoint secondLastPoint = lastPoint.before;
            Vector3 lastVector=   Camera.main.WorldToScreenPoint(secondLastPoint.transform.position);
            Destroy(lastPoint.gameObject);
            gestureInput.lastGestPoint = secondLastPoint;
            gestureInput.lastVector = new Vector2(lastVector.x, lastVector.y);
            secondLastPoint.after = null;

        }
    }
}
