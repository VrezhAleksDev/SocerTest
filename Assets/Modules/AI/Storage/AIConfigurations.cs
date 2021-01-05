using AI.Models;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Storage
{
    [CreateAssetMenu(fileName = "AIConfigurations", menuName = "ScriptableObjects/AIConfigurations", order = 1)]
    public class AIConfigurations : ScriptableObject
    {
        private static AIConfigurations _storage;
        private static AIConfigurations Storage
        {
            get
            {
                if (_storage == null)
                {
                    _storage = Resources.Load<AIConfigurations>("AIConfigurations");
                }

                return _storage;
            }
        }

        public List<ZoneRangeInfo> ZoneRangeInfos = new List<ZoneRangeInfo>();

        public static Dictionary<string, ZoneRangeInfo> _rangeInfos;
        public static Dictionary<string, ZoneRangeInfo> RangeInfos {
            get
            {
                if (_rangeInfos == null)
                {
                    _rangeInfos = new Dictionary<string, ZoneRangeInfo>();

                    for (int i = 0; i < Storage.ZoneRangeInfos.Count; i++)
                    {
                        RangeInfos.Add(Storage.ZoneRangeInfos[i].Name, Storage.ZoneRangeInfos[i]);
                    }
                }

                return _rangeInfos;
            }
        }
    }
}
