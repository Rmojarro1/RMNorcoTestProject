using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MhGesturePoint : MonoBehaviour {
   
    public MhGesturePoint before;
    public MhGesturePoint after;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {  
    }

    public void SetNextPoint(MhGesturePoint nextPoint)
    {
        after = nextPoint;
        nextPoint.before = this;
    }
}
