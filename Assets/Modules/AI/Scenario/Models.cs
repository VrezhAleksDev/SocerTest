using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Scenario.Models
{
    [System.Serializable]
    public class SocerSpawnInfo
    {
        public string Name;
        public Transform SpawnPoint;
        public int TrajectoryGroup = - 1;
        public int Team;
    }
}
