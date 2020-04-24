using System;
using System.Collections.Generic;
using UnityEngine;

public static class MhPointPatternMath
{
    public static List<Vector2> GetInterpolatedPointArray(List<Vector2> points, int segments, MhGesture gesture)
    {
        //cached data
        if(gesture!= null && MhGestureManager.preInterpolatedPoints.ContainsKey(gesture) && MhGestureManager.preInterpolatedPoints[gesture].ContainsKey(segments))
        {
            return MhGestureManager.preInterpolatedPoints[gesture][segments] ;
        }
        // Create an empty return collection to store interpolated points
        var interpolatedPoints = new List<Vector2>(segments);

        // Precalculate desired segment length and define helper variables
        var desiredSegmentLength = GetPointArrayLength(points) / segments;
        var currentSegmentLength = 0f; // Initialize to zero

        // Add first point in point pattern to return array and save it for use in the interpolation process
        var lastTestPoint = points[0]; // Initialize to first point in point pattern
        interpolatedPoints.Add(lastTestPoint);

        // Enumerate points starting with second point (if any)
        for (var currentIndex = 1; currentIndex < points.Count; currentIndex++)
        {
            // Store current index point in helper variable
            var currentPoint = points[currentIndex];

            // Calculate distance between last added point and current point in point pattern
            // and use calculated length to calculate test segment length for next point to add
            var incrementToCurrentlength = GetDistance(lastTestPoint, currentPoint);
            var testSegmentLength = currentSegmentLength + incrementToCurrentlength;

            // Does the test segment length meet our desired length requirement
            if (testSegmentLength < desiredSegmentLength)
            {
                // Desired segment length has not been satisfied so we don't need to add an interpolated point
                // save this test point and move on to next test point
                currentSegmentLength = testSegmentLength;
                lastTestPoint = currentPoint;
                continue;
            }

            // Test segment length has met or exceeded our desired segment length
            // so lets calculate how far we overshot our desired segment length and calculate
            // an interpolation position to use to derive our new interpolation point
            var interpolationPosition = (desiredSegmentLength - currentSegmentLength) * (1.0f / incrementToCurrentlength);

            // Use interpolation position to derive our new interpolation point
            var interpolatedPoint = GetInterpolatedPoint(lastTestPoint, currentPoint, interpolationPosition);
            interpolatedPoints.Add(interpolatedPoint);

            // Sometimes rounding errors cause us to attempt to add more points than the user has requested.
            // If we've reached our segment count limit, exit loop
            if (interpolatedPoints.Count == segments)
                break;

            // Store new interpolated point as last test point for use in next segment calculations
            // reset current segment length and jump back to the last index because we aren't done with original line segment
            lastTestPoint = interpolatedPoint;
            currentSegmentLength = 0;
            currentIndex--;
        }

        //add to cache
        if (gesture != null)
        {
            if (MhGestureManager.preInterpolatedPoints.ContainsKey(gesture))
            {
                MhGestureManager.preInterpolatedPoints[gesture].Add(segments, interpolatedPoints);
            }
            else
            {
                Dictionary<int, List<Vector2>> newPointVector = new Dictionary<int, List<Vector2>>();
                newPointVector.Add(segments, interpolatedPoints);
                MhGestureManager.preInterpolatedPoints.Add(gesture, newPointVector);
            }
        }
            
        // Return interpolated point array
        return interpolatedPoints;
    }

    public static Vector2 GetInterpolatedPoint(Vector2 lineStartPoint, Vector2 lineEndPoint, float interpolatePosition)
    {
        // Create return point
        // Calculate x and y of increment point
        var pReturn = new Vector2();
        pReturn.x = (1 - interpolatePosition) * lineStartPoint.x + interpolatePosition * lineEndPoint.x;
        pReturn.y = (1 - interpolatePosition) * lineStartPoint.y + interpolatePosition * lineEndPoint.y;


        // Return new point
        return pReturn;
    }

    public static float[] GetPointArrayAngles(List<Vector2> pointArray)
    {
        // Create an empty collection of angles
        var angularMargins = new List<float>();

        // Enumerate input point array starting with second point and calculate angular margin
        for (var currentIndex = 1; currentIndex < pointArray.Count; currentIndex++)
            angularMargins.Add(GetAngle(pointArray[currentIndex - 1], pointArray[currentIndex]));

        // Return angular margins array
        return angularMargins.ToArray();
    }

    public static float GetAngle(Vector2 lineStartPoint, Vector2 lineEndPoint)
    {
        return Mathf.Atan2((lineEndPoint.y - lineStartPoint.y), (lineEndPoint.x - lineStartPoint.x));
    }

    public static float GetDotProduct(float angle1, float angle2)
    {
        var retValue = Mathf.Abs(angle1 - angle2);

        if (retValue > Mathf.PI)
        {
            retValue = Mathf.PI - (retValue - Mathf.PI);
        }
        return retValue;
    }

    public static double GetProbabilityFromDotProduct(double dotProduct)
    {
        // Constant represents precalculated scale of conversion of angle to probability.
        const double dScale = 31.830988618379067D;
        return Math.Abs(dotProduct * dScale - 100.0f);
    }

    public static float GetDegreeFromRadian(float angle)
    {
        return angle * (180.0f / Mathf.PI);
    }

    public static float GetDistance(Vector2 lineStartPoint, Vector2 lineEndPoint)
    {
        return GetDistance(lineStartPoint.x, lineStartPoint.y, lineEndPoint.x, lineEndPoint.y);
    }

    public static float GetDistance(float x1, float y1, float x2, float y2)
    {
        var xd = x2 - x1;
        var yd = y2 - y1;
        return Mathf.Sqrt(xd * xd + yd * yd);
    }

    public static float GetPointArrayLength(List<Vector2> points)
    {
        // Create return variable to hold final calculated length
        float returnLength = 0f;

        // Enumerate points in point pattern and get a sum of each line segments distances
        for (var currentIndex = 1; currentIndex < points.Count; currentIndex++)
            returnLength += GetDistance(points[currentIndex - 1], points[currentIndex]);

        // Return calculated length
        return returnLength;
    }
}
