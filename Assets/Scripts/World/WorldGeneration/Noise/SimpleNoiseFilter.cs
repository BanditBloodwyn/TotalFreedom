using System;
using Assets.Scripts.World.WorldGeneration.Settings;
using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration.Noise
{
    internal class SimpleNoiseFilter : INoiseFilter
    {
        private readonly SimplexPerlinNoise noise = new SimplexPerlinNoise();
        private readonly WorldNoiseSettings.SimpleNoiseSettings simpleNoiseSettings;

        public SimpleNoiseFilter(WorldNoiseSettings.SimpleNoiseSettings simpleNoiseSettings)
        {
            this.simpleNoiseSettings = simpleNoiseSettings;
        }

        public float Evaluate(Vector3 point, Vector3 position)
        {
            float noiseValue = 0;
            float frequency = simpleNoiseSettings.BaseRoughness;
            float amplitude = 1;


            for (int i = 0; i < simpleNoiseSettings.NumberOfLayers; i++)
            {
                float v = noise.Evaluate(point * frequency + simpleNoiseSettings.center);
                noiseValue += (v + 1) * 0.5f * amplitude;
                frequency *= simpleNoiseSettings.Roughness;
                amplitude *= simpleNoiseSettings.Persistence;
            }

            noiseValue = Math.Max(0, noiseValue - simpleNoiseSettings.Minimum);
            return noiseValue * simpleNoiseSettings.strength;
        }
    }
}