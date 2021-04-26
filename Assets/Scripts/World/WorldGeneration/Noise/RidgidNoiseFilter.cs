using System;
using Assets.Scripts.World.WorldGeneration.Settings;
using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration.Noise
{
    internal class RidgidNoiseFilter : INoiseFilter
    {
        private readonly SimplexPerlinNoise noise = new SimplexPerlinNoise();
        private readonly WorldNoiseSettings.RidgidNoiseSettings noiseSettings;

        public RidgidNoiseFilter(WorldNoiseSettings.RidgidNoiseSettings noiseSettings)
        {
            this.noiseSettings = noiseSettings;
        }

        public float Evaluate(Vector3 point, Vector3 position)
        {
            float noiseValue = 0;
            float frequency = noiseSettings.BaseRoughness;
            float amplitude = 1;
            float weight = 1;

            for (int i = 0; i < noiseSettings.NumberOfLayers; i++)
            {
                float v = 1 - Mathf.Abs(noise.Evaluate((point + noiseSettings.center - position) * frequency));
                v *= v;
                v *= weight;
                weight = Mathf.Clamp01(v * noiseSettings.WeightMultiplier);
                noiseValue += v * amplitude;
                frequency *= noiseSettings.Roughness;
                amplitude *= noiseSettings.Persistence;
            }

            noiseValue = Math.Max(0, noiseValue - noiseSettings.Minimum);
            return noiseValue * noiseSettings.strength;
        }
    }
}