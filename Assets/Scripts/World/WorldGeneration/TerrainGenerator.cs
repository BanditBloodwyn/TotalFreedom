using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration
{
    public class TerrainGenerator : MonoBehaviour
    {
        [Tooltip("All objects which need an active terrain around them.")]
        public Transform[] WorldInstances;
        public GameObject Player;

        [Range(10, 256)]
        public float ViewDistance = 10;

        [Range(10, 256)]
        public int ChunkSize = 10;

        public Material Material;

        private int chunksVisibleInViewDistance;
        private readonly Dictionary<Vector2, TerrainChunk> allTerrainChunks = new Dictionary<Vector2, TerrainChunk>();
        private readonly List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();
        private readonly List<Vector2> worldInstancesChunkCoordinates = new List<Vector2>();

        // Start is called before the first frame update
        private void Start()
        {
            chunksVisibleInViewDistance = Mathf.RoundToInt(ViewDistance / ChunkSize);
        }

        // Update is called once per frame
        private void Update()
        {
            // "turn off" all chunks that are too far away from all WorldInstances
            foreach (TerrainChunk terrainChunk in terrainChunksVisibleLastUpdate)
            {
                terrainChunk.SetVisible(false, null);
            }
            terrainChunksVisibleLastUpdate.Clear();
            worldInstancesChunkCoordinates.Clear();

            foreach (Transform worldInstance in WorldInstances)
            {
                Vector2 coord = new Vector2(
                    Mathf.RoundToInt(worldInstance.position.x / ChunkSize),
                    Mathf.RoundToInt(worldInstance.position.z / ChunkSize));
                worldInstancesChunkCoordinates.Add(coord);
            }

            foreach (Vector2 worldInstancesChunkCoordinate in worldInstancesChunkCoordinates)
            {
                UpdateChunksForInstance(worldInstancesChunkCoordinate);
            }
        }

        private void UpdateChunksForInstance(Vector2 worldInstancesChunkCoordinate)
        {
            for (int yOffset = -chunksVisibleInViewDistance; yOffset <= chunksVisibleInViewDistance; yOffset++)
            {
                for (int xOffset = -chunksVisibleInViewDistance; xOffset <= chunksVisibleInViewDistance; xOffset++)
                {
                    Vector2 viewedChunkCoord = new Vector2(worldInstancesChunkCoordinate.x + xOffset, worldInstancesChunkCoordinate.y + yOffset);

                    if (allTerrainChunks.ContainsKey(viewedChunkCoord))
                    {
                        allTerrainChunks[viewedChunkCoord].UpdateChunk(new Vector2(worldInstancesChunkCoordinate.x, worldInstancesChunkCoordinate.y), ViewDistance, worldInstancesChunkCoordinate);
                        if (allTerrainChunks[viewedChunkCoord].IsVisible())
                            terrainChunksVisibleLastUpdate.Add(allTerrainChunks[viewedChunkCoord]);
                    }
                    else
                    {
                        allTerrainChunks.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, ChunkSize, transform, Material, Player));
                    }
                }
            }
        }
    }
}
