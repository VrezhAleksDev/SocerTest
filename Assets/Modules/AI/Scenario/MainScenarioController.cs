using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Scenario.Models;
using AI.Trajectory;

namespace AI.Scenario
{
    public class MainScenarioController : MonoBehaviour
    {
        public static Dictionary<int, List<BasicController>> Socers = new Dictionary<int, List<BasicController>>();

        public GameObject SocerPrefab;
        public List<SocerSpawnInfo> SpawnInfos;

        private void Start()
        {
            for (int i = 0; i < SpawnInfos.Count; i++)
            {
                SpawnSocer(SpawnInfos[i]);
            }
        }

        private void SpawnSocer(SocerSpawnInfo spawnInfo)
        {
            BasicController basicController = Instantiate(SocerPrefab).GetComponent<BasicController>();
            basicController.transform.position = spawnInfo.SpawnPoint.position;
            basicController.transform.rotation = spawnInfo.SpawnPoint.rotation;

            basicController.Team = spawnInfo.Team;

            if (spawnInfo.TrajectoryGroup >= 0)
            {
                List<TrajectoryPoint> points = new List<TrajectoryPoint>(FindObjectsOfType<TrajectoryPoint>()).FindAll(item => item.Group == spawnInfo.TrajectoryGroup);
                points.Sort((a, b) => a.Index > b.Index ? 1 : -1);

                basicController.CurrentTrajectory = new LineTrajectory(points.ToArray());
            }

            if (!Socers.ContainsKey(spawnInfo.Team))
            {
                Socers.Add(spawnInfo.Team, new List<BasicController>());
            }

            Socers[spawnInfo.Team].Add(basicController);
        }
    }
}
