using AI.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Trajectory
{
    public class PlayerZone : MonoBehaviour
    {
        public Role PlayerRole;
        public Position PlayerPosition;
        public List<ZoneRange> Ranges;

        private static Dictionary<(Role, Position), PlayerZone> _zones;
        private static Dictionary<(Role, Position), PlayerZone> Zones
        {
            get
            {
                if (_zones == null)
                {
                    _zones = new Dictionary<(Role, Position), PlayerZone>();

                    PlayerZone[] zones = FindObjectsOfType<PlayerZone>();

                    for (int i = 0; i < zones.Length; i++)
                    {
                        _zones.Add((zones[i].PlayerRole, zones[i].PlayerPosition), zones[i]);
                    }
                }
                return _zones;
            }
        }

        public ZoneRange GetRangeForTarget(Transform target)
        {
            for (int i = 0; i < Ranges.Count; i++)
            {
                if (Vector3.Distance(target.position, transform.position + Ranges[i].Spacing) <= Ranges[i].Radius)
                {
                    return Ranges[i];
                }
            }
            return null;
        }

        public static PlayerZone GetZone(Role playerRole, Position playerPosition)
        {
            if (Zones.ContainsKey((playerRole, playerPosition)))
            {
                return Zones[(playerRole, playerPosition)];
            }

            List<PlayerZone> zones = new List<PlayerZone>(FindObjectsOfType<PlayerZone>());

            PlayerZone zone = zones.Find(item => item.PlayerRole == playerRole && item.PlayerPosition == playerPosition);

            if (zone != null)
            {
                Zones.Add((playerRole, playerPosition), zone);
            }

            return zone;
        }

        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < Ranges.Count; i++)
            {
                if (Storage.AIConfigurations.RangeInfos.ContainsKey(Ranges[i].Name))
                {
                    Gizmos.color = Storage.AIConfigurations.RangeInfos[Ranges[i].Name].GizmoColor;
                }

                Gizmos.DrawWireSphere(transform.position + Ranges[i].Spacing, Ranges[i].Radius);
            }
        }

    }
}
