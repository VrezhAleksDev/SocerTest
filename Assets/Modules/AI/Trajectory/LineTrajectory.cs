using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Trajectory
{
    public class LineTrajectory
    {
        public readonly List<TrajectoryPoint> Points = new List<TrajectoryPoint>();

        public LineTrajectory(params TrajectoryPoint[] points)
        {
            Points = new List<TrajectoryPoint>(points);
        }

        private int _currentPointIndex;
        public TrajectoryPoint CurrentPoint
        {
            get
            {
                if (_currentPointIndex >= Points.Count)
                {
                    return null;
                }

                return Points[_currentPointIndex];
            }
        }

        public void CurrentPointReached()
        {
            _currentPointIndex++;
        }
    }
}

