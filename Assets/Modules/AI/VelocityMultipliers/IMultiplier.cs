using AI.Models;
using UnityEngine;


namespace AI.VelocityMultipliers
{
    public interface IMultiplier
    {
        Vector3 CalculateMultiplier(SocerInfo socerInfo, Transform target);
    }
}
