using Assets.Scripts.World.WorldGeneration.Noise;
using Assets.Scripts.World.WorldGeneration.Settings;
using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration
{
    public class ShapeGenerator
    {
        private WorldShapeSettings shapeSettings;
        private INoiseFilter[] noiseFilters;

        public void UpdateSettings(WorldShapeSettings settings)
        {
            shapeSettings = settings;

            noiseFilters = new INoiseFilter[shapeSettings.NoiseLayers.Length];

            for (int i = 0; i < noiseFilters.Length; i++)
            {
                noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(shapeSettings.NoiseLayers[i].NoiseSettings);
            }
        }

        public Vector3 CalculatePointOnWorld(Vector3 pointOnWorld, float worldSize, Vector3 position)
        {
            float firstLayerValue = 0;
            float elevation = 0;

            if (noiseFilters.Length > 0)
            {
                firstLayerValue = noiseFilters[0].Evaluate(pointOnWorld, -position / worldSize);
                
                if (shapeSettings.NoiseLayers[0].Enabled)
                    elevation = firstLayerValue;
            }

            for (int i = 1; i < noiseFilters.Length; i++)
            {
                if (shapeSettings.NoiseLayers[i].Enabled)
                {
                    float mask = shapeSettings.NoiseLayers[i].UseFirstLayerAsMask
                        ? firstLayerValue
                        : 1;
                   
                    elevation += noiseFilters[i].Evaluate(pointOnWorld, -position / worldSize) * mask;
                }
            }

            return new Vector3(
                pointOnWorld.x * worldSize/2 - position.x/2,
                pointOnWorld.y * elevation,
                pointOnWorld.z * worldSize/2 - position.z/2);
        }
    }
}