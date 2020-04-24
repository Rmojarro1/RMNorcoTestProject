using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Provides logic to compare a single gesture against a set of learned PointPatterns.
/// </summary>
public class MhPointPatternAnalyzer
{
    /// <summary>
    /// min probability limit for considering gesture at least "possible" similar
    /// </summary>
    private const double percentageThreshold = 50.0;

    /// <summary>
    /// Instantiates a new PointPatternAnalyzer with an empty PointPatternSet and given precision 
    /// </summary>
    public MhPointPatternAnalyzer(int precision)
           : this(new MhGesture[0], precision)
    {

    }
    /// <summary>
    /// Instantiates a new PointPatternAnalyzer with the specified PointPatternSet and a default precision of 100.
    /// </summary>
    /// <param name="pointPatternSet">IEnumerable of PointPattern objects loaded from an external source.</param>
    public MhPointPatternAnalyzer(IEnumerable<MhGesture> pointPatternSet)
            : this(pointPatternSet, 100)
    {

    }
    /// <summary>
    /// Instantiates a new PointPatternAnalyzer with the specified PointPatternSet and precision.
    /// </summary>
    /// <param name="pointPatternSet">IEnumerable of PointPattern objects loaded from an external source.</param>
    /// <param name="precision">Number of interpolation steps to use when comparing two gestures</param>
    public MhPointPatternAnalyzer(IEnumerable<MhGesture> pointPatternSet, int precision)
    {
        // Instantiate PointPatternAnalyzer class with a PointPatternSet and Precision
        PointPatternSet = pointPatternSet.ToList();
        Precision = precision;
    }

    /// <summary>
    /// Gets or sets the number of interpolation steps to use when comparing two gestures.
    /// </summary>
    public int Precision { get; set; }

    /// <summary>
    /// Library of saved gestures to compare the gesture to be analyzed against.
    /// </summary>
    public List<MhGesture> PointPatternSet { get; set; }

    /// <summary>
    /// Compares the points of a drawn gesture to every learned gesture in PointPatternSet and returns an
    /// array of accuracy probabilities of each gesture ordered by best match first.
    /// </summary>
    /// <param name="points">Points of the current gesture being analyzed.</param>
    /// <returns>Returns an array of accuracy probabilities of each gesture in PointPatternSet ordered by best match first.</returns>
    public MhPointPatternMatchResult[] GetPointPatternMatchResults(List<Vector2> points)
    {
       return GetPointPatternMatchResults(points, PointPatternSet);
    }

    public MhPointPatternMatchResult[] GetPointPatternMatchResults(List<Vector2> points, List<MhGesture> gesturesToCompare)
    {
        // Ensure we have at least 2 points or recognition will fail as we are unable to interpolate between a single point.
        if (points.Count < 2)
            return new MhPointPatternMatchResult[0];

        // Create a list of PointPatternMatchResults to hold final results and group results of point pattern set comparison
        var comparisonResults = new List<MhPointPatternMatchResult>();
        var groupComparisonResults = new List<MhPointPatternMatchResult>();

        // Enumerate each point patterns grouped by name
        foreach (var pointPatternSet in gesturesToCompare.GroupBy(pp => pp.Name))
        {
            // Clear out group comparison results from last group comparison
            groupComparisonResults.Clear();

            // Calculate probability of each point pattern in this group
            foreach (var pointPatternCompareTo in pointPatternSet)
            {
                var result = GetPointPatternMatchResult(pointPatternCompareTo, points);
                groupComparisonResults.Add(result);
            }

            // Add results of group comparison to final comparison results
            var averageProbability = groupComparisonResults.Average(ppmr => ppmr.Probability);
            var totalSetCount = groupComparisonResults.Sum(ppmr => ppmr.PointPatternSetCount);

            comparisonResults.Add(new MhPointPatternMatchResult(pointPatternSet.Key, averageProbability, totalSetCount));
        }

        // Return comparison results ordered by highest probability
        return comparisonResults.OrderByDescending(ppmr => ppmr.Probability).ToArray();
    }


    /// <summary>
    /// Compares a points of a single gesture, to the points in a single saved gesture, and returns a accuracy probability.
    /// </summary>
    /// <param name="compareTo">Learned PointPattern from PointPatternSet to compare gesture points to.</param>
    /// <param name="points">Points of the current gesture being analyzed.</param>
    /// <returns>Returns the accuracy probability of the learned PointPattern to the current gesture.</returns>
    public MhPointPatternMatchResult GetPointPatternMatchResult(MhGesture compareTo, List<Vector2> points)
    {
        return GetPointPatternMatchResult(compareTo, points, Precision);
    }

    /// <summary>
    /// Compares a points of a single gesture, to the points in a single saved gesture, and returns a accuracy probability.
    /// </summary>
    /// <param name="compareTo">Learned PointPattern from PointPatternSet to compare gesture points to.</param>
    /// <param name="points">Points of the current gesture being analyzed.</param>
    /// <returns>Returns the accuracy probability of the learned PointPattern to the current gesture.</returns>
    public MhPointPatternMatchResult GetPointPatternMatchResult(MhGesture compareTo, List<Vector2> points, int interpolationPoints)
    {
        // Ensure we have at least 2 points or recognition will fail as we are unable to interpolate between a single point.
        if (points.Count < 2 && interpolationPoints <2)
            throw new ArgumentOutOfRangeException("To few points or small inperpolation points");

        // We'll use an array of doubles that matches the number of interpolation points to hold
        // the dot products of each angle comparison.
        var dotProducts = new List<float>(interpolationPoints + 1);

        // We'll need to interpolate the incoming points array and the points of the learned gesture.
        // We do this for each comparison so that we can change the precision at any time and not lose
        // or original learned gesture to multiple interpolations.
        var interpolatedCompareTo = MhPointPatternMath.GetInterpolatedPointArray(compareTo.Points, interpolationPoints, compareTo);
        var interpolatedPointArray = MhPointPatternMath.GetInterpolatedPointArray(points, interpolationPoints, null);

        // Next we'll get an array of angles for each interpolated point in the learned and current gesture.
        // We'll get the same number of angles corresponding to the total number of interpolated points.
        var anglesCompareTo = MhPointPatternMath.GetPointArrayAngles(interpolatedCompareTo);
        var angles = MhPointPatternMath.GetPointArrayAngles(interpolatedPointArray);

        // Now that we have angles for each gesture, we'll get the dot product of every angle equal to 
        // the total number of interpolation points.
        for (var i = 0; i <= anglesCompareTo.Length - 1; i++)
            dotProducts.Add(MhPointPatternMath.GetDotProduct(anglesCompareTo[i], angles[i]));

        //
        // Convert average dot product to probability since we're using the deviation
        // of the average of the dot products of every interpolated point in a gesture.
        var probability = MhPointPatternMath.GetProbabilityFromDotProduct(dotProducts.Average());

        //first 5th percent and last 5th percent is more important
        for (var i = 0; i <= (anglesCompareTo.Length / 20.0) - 1; i++)
        {
            dotProducts.Add(MhPointPatternMath.GetDotProduct(anglesCompareTo[i], angles[i]));
        }
        //last 5th percent
        for (int i = (int)((anglesCompareTo.Length / 100.0) * 95.0 - 1); i <= anglesCompareTo.Length - 1; i++)
        {
            dotProducts.Add(MhPointPatternMath.GetDotProduct(anglesCompareTo[i], angles[i]));
        }

        var probabilityAlt = MhPointPatternMath.GetProbabilityFromDotProduct(dotProducts.Average());

        // Return PointPatternMatchResult object that holds the results of comparison.
        return new MhPointPatternMatchResult(compareTo.Name, probability, 1);
    }


    /// <summary>
    /// advance version of pattern match recognition with predefinet precision to help preeliminated gestures
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public MhPointPatternMatchResult[] GetPointPatternMatchResultsAdv( List<Vector2> points)
    {
        
        List<int> interpoaletNumbers = new List<int>() {  3, 4, 5, 8, 10, 15, 25, 50 };
        List<MhGesture> gesturesToCheck= CheckSimplePart(points);

        double probabilityLimit = percentageThreshold;
        foreach (int precision in interpoaletNumbers)
        {
            List<MhGesture> gesturesToRemove = new List<MhGesture>();

            foreach (MhGesture gesture in gesturesToCheck)
            {
                var gestureResult = GetPointPatternMatchResult(gesture, points,precision);
                if (gestureResult.Probability < probabilityLimit)
                {
                    gesturesToRemove.Add(gesture);
                }
            }
            if(gesturesToRemove.Count < gesturesToCheck.Count)
            {
                gesturesToCheck.RemoveAll(x => gesturesToRemove.Contains(x));
            }
        }       
        return GetPointPatternMatchResults(points, gesturesToCheck);
    }

    /// <summary>
    /// check basic gesture direction  bewteen first and last point
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    private List<MhGesture> CheckSimplePart(List<Vector2> points)
    {
        List<MhGesture> possiblegesture = new List<MhGesture>(PointPatternSet);
        float distance = 0;

        for (int i = 0; i < points.Count - 1; i++)
        {
            distance += Vector2.Distance(points[i], points[i + 1]);
        }
        float startEndDistance = Vector2.Distance(points[0], points[points.Count - 1]);


        List<MhGesture> gesturesToRemove = new List<MhGesture>();
        const double distanceConstant = 10.0;//constant  to check 10% of distance which is limit to considering "direction of gesture"

        if (startEndDistance >= distance / distanceConstant)
        {
            foreach (MhGesture gesture in possiblegesture)
            {
                float gestureDistance = 0;
                for (int i = 0; i < gesture.points.Count - 1; i++)
                {
                    gestureDistance += Vector2.Distance(gesture.points[i], gesture.points[i + 1]);
                }
                float gestureStartEndDistance = Vector2.Distance(gesture.points[0], gesture.points[gesture.points.Count - 1]);

                if (gestureStartEndDistance >= gestureDistance / distanceConstant)
                {
                    var gestureResult = GetPointPatternMatchResult(gesture, points, 2);
                    if (gestureResult.Probability < percentageThreshold)
                    {
                        gesturesToRemove.Add(gesture);
                    }

                }
            }
        }

        possiblegesture.RemoveAll( x => gesturesToRemove.Contains(x));
        return possiblegesture;
    }
}