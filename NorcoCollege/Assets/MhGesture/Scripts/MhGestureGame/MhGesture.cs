using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MhGesture
{

    public string name;
    public List<Vector2> points;

    public MhGesture()
    {
        points = new List<Vector2>();
    }

    public MhGesture(string name, List<Vector2> points)
    {
        this.name = name;
        this.points = points;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public List<Vector2> Points { get { return points; } }


    public void CentrePoints()
    {
        float xChange = 0 - points[0].x;
        float yChange = 0 - points[0].y;
        for (int i = 0; i < points.Count; i++)
        {
            points[i] = new Vector2(points[i].x + xChange, points[i].y + yChange);

        }
    }

    public override int GetHashCode()
    {
        return name.GetHashCode();
    }
    public override bool Equals(object obj)
    {
        MhGesture gesture = obj as MhGesture;

        return (gesture != null) && String.Equals(name, gesture.name);
    }
    

}
