using Assets.Scripts.World.WorldGeneration.Settings;

namespace Assets.Scripts.World.WorldGeneration.Noise
{
    public static class NoiseFilterFactory
    {
        public static INoiseFilter CreateNoiseFilter(WorldNoiseSettings noiseSettings)
        {
            return noiseSettings.filterType switch
            {
                WorldNoiseSettings.FilterType.Simple => new SimpleNoiseFilter(noiseSettings.simpleNoiseSettings),
                WorldNoiseSettings.FilterType.Ridgid => new RidgidNoiseFilter(noiseSettings.ridgidNoiseSettings),
                _ => null
            };
        }
    }
}