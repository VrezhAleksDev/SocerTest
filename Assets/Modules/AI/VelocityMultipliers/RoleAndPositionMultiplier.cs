using AI.Models;
using AI.Trajectory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.VelocityMultipliers
{
    public class RoleAndPositionMultiplier : IMultiplier
    {
        public Vector3 CalculateMultiplier(SocerInfo socerInfo, Transform target)
        {
            PlayerZone zone = PlayerZone.GetZone(socerInfo.Role, socerInfo.Position);

            ZoneRange range = zone.GetRangeForTarget(target);

            if (range == null)
            {
                return Vector3.zero;
            }

            if (Storage.AIConfigurations.RangeInfos.ContainsKey(range.Name))
            {
                return Storage.AIConfigurations.RangeInfos[range.Name].VeloictyMultiplier;
            }

            return Vector3.zero;
        }
    }
}
