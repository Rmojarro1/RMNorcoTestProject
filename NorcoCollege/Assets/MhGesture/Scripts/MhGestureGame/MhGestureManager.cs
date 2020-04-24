using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// main backend logic for handling gestures, saving , loading , removing and add new ones
/// </summary>
public class MhGestureManager
{
    /// <summary>
    /// existing gestures loaded from file or added runtime// 
    /// </summary>
    public static List<MhGesture> gestures = new List<MhGesture>();
    /// <summary>
    /// class which comparaing gestures has default precision 500 points per gestures
    /// </summary>
    public static MhPointPatternAnalyzer analyzer = new MhPointPatternAnalyzer(500);
    /// <summary>
    /// cached gestures which are interpolated to different number of  points for better performance
    /// </summary>
    public static Dictionary<MhGesture, Dictionary<int, List<Vector2>>> preInterpolatedPoints = new Dictionary<MhGesture, Dictionary<int, List<Vector2>>>();
    /// <summary>
    /// enable or disabe capturing gestures in game or not
    /// </summary>
    public static bool gestureCaptureEnable = true;
    /// <summary>
    /// distane betweem points when gesture is capturing, current precision set for 1920x1080
    /// </summary>
    public static float distanceConstant = 92000.0f;// 
    /// <summary>
    /// minimum number of point which need to be capture to recognize as gesture
    /// </summary>
    public static int minNumberOfpoints = 10;


    public static void SaveGestures(string filePath = "Assets/MhGesture/Data/Gestures.json")
    {
        MhGestureList list = new MhGestureList();
        list.gestures = gestures;
        string jsonString = JsonUtility.ToJson(list, true);

        File.WriteAllText(filePath, jsonString);
    }


    public static void LoadGestures(string filePath = "Assets/MhGesture/Data/Gestures.json")
    {
        if (!File.Exists(filePath))
        {
            Debug.Log(string.Format("File in path: {0} was not found", filePath));
            return;

        }
        string jsonString = File.ReadAllText(filePath);

        MhGestureList list = JsonUtility.FromJson<MhGestureList>(jsonString);
        gestures = list.gestures;
        analyzer.PointPatternSet.AddRange(list.gestures);

    }


    public static void AddGesture(MhGesture gesture)
    {
        gestures.Add(gesture);
        analyzer.PointPatternSet.Add(gesture);
    }

    public static MhGesture FindGesture(string name)
    {
        return gestures.Find(x => x.name.Equals(name));
    }

    public static bool RemoveGesture(string name)
    {
        MhGesture gestureToRemove = FindGesture(name);
        if(gestureToRemove != null)
        {
            preInterpolatedPoints.Remove(gestureToRemove);
            return gestures.Remove(gestureToRemove);
        }
        Debug.LogWarning(string.Format("Removing gesture problem, gesture with name: {0} was not found {1}", name));
        return false;
        
    }

}
