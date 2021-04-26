using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration.Noise
{
    public interface INoiseFilter
    { public float Evaluate(Vector3 point, Vector3 position);
    }
}