using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Models
{
    public enum Role
    {
        Defender,
        Midfielder,
        Forward,
        Striker
    }
    public enum Position
    {
        Left,
        Middle,
        Right
    }
    [System.Serializable]
    public class SocerSpawnInfo : SocerInfo
    {
        public Transform SpawnPoint;
        public int TrajectoryGroup = - 1;
    }

    [System.Serializable]
    public class SocerInfo
    {
        public string Name;
        public int Team;
        public Role Role;
        public Position Position;
    }

    [System.Serializable]
    public class ZoneRangeInfo
    {
        public string Name;
        public Color GizmoColor;
        public Vector3 VeloictyMultiplier;
    }

    [System.Serializable]
    public class ZoneRange
    {
        public string Name;
        public float Radius;
        public Vector3 Spacing;
    }
}
