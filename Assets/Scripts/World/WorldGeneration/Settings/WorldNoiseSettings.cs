using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration.Settings
{
    [System.Serializable]
    public class WorldNoiseSettings
    {
        public enum FilterType
        {
            Simple,
            Ridgid
        }
        public FilterType filterType;

        [ConditionalHide("filterType", 0)]
        public SimpleNoiseSettings simpleNoiseSettings;
        [ConditionalHide("filterType", 1)]
        public RidgidNoiseSettings ridgidNoiseSettings;

        [System.Serializable]
        public class SimpleNoiseSettings
        {
            [Range(1, 100)]
            public float strength = 1;

            [Range(1, 10)]
            public int NumberOfLayers = 1;

            [Range(0, 10)]
            public float BaseRoughness = 1;
            [Range(0, 10)]
            public float Roughness = 1;

            [Range(0.1f, 0.5f)]
            public float Persistence = 0.5f;

            public float Minimum;

            public Vector3 center;
        }

        [System.Serializable]
        public class RidgidNoiseSettings : SimpleNoiseSettings
        {
            [Range(0f, 6f)]
            public float WeightMultiplier = 0.8f;
        }
    }
}