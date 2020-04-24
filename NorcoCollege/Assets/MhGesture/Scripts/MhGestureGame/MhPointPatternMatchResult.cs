using System;
using UnityEngine;

public struct MhPointPatternMatchResult
    {
    private string name;
    private double probability;
    private int pointPatternSetCount;

    public MhPointPatternMatchResult(string name, double probability, int pointPatternSetCount) : this()
        {
            if (probability > 100 || probability < 0)
                throw new OverflowException("Proability must be between zero (0) and one hundred (100)");

            this.name = name;
            this.probability = probability;
            this.pointPatternSetCount = pointPatternSetCount;
        }
        
        public string Name { get { return name; } }

        public double Probability { get { return probability; } }

        public int PointPatternSetCount { get { return pointPatternSetCount; } }
    }
