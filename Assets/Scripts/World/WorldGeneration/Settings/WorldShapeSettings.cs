using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration.Settings
{
    [CreateAssetMenu]
    public class WorldShapeSettings : ScriptableObject
    {
        [Range(1, 256)]
        public float worldSize;

        public NoiseLayer[] NoiseLayers;

        [System.Serializable]
        public class NoiseLayer
        {
            public bool Enabled = true;
            public bool UseFirstLayerAsMask;
            public WorldNoiseSettings NoiseSettings;
        }
    }
}