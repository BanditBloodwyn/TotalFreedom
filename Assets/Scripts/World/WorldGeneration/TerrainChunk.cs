using System.Collections.Generic;
using Assets.Scripts.Camera;
using Assets.Scripts.Gameplay.Characters.Player;
using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration
{
    public class TerrainChunk
    {
        private const int resolution = 128;

        private TerrainMesh terrainMesh;
        private readonly ShapeGenerator shapeGenerator;
        private MeshFilter meshFilter;
        private GameObject terrainGameObject;
        private readonly Material material;

        private readonly Vector2 chunkPosition2D; // chunk coordinates, not worldspace coordinates
        private Bounds bounds;
        private readonly int size;

        private readonly List<Vector2> gameObjectsThatSeeThisChunk = new List<Vector2>();

        public TerrainChunk(ShapeGenerator shapeGenerator, Vector2 coord, int size, Transform parent, Material material, GameObject Player)
        {
            this.shapeGenerator = shapeGenerator;
            this.size = size;
            this.material = material;

            chunkPosition2D = coord * size;
            bounds = new Bounds(chunkPosition2D, Vector2.one * size);

            CreateGameObject(parent, Player);
            CreateMeshFilter();
            ConstructMesh();
            SetVisible(false, null);
        }

        private void CreateGameObject(Transform parent, GameObject player)
        {
            terrainGameObject = new GameObject("TerrainObject");
            terrainGameObject.transform.parent = parent;
            terrainGameObject.transform.position = new Vector3(chunkPosition2D.x, 0, chunkPosition2D.y);
            terrainGameObject.AddComponent<MeshRenderer>();
            terrainGameObject.layer = 3;
            terrainGameObject.tag = "Walkable";
            terrainGameObject.AddComponent<ClickToSetPlayerTarget>();
            terrainGameObject.GetComponent<ClickToSetPlayerTarget>().walkingScript = player.GetComponent<WalkToClickedPosition>();
        }

        private void CreateMeshFilter()
        {
            meshFilter = terrainGameObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();

            meshFilter.GetComponent<MeshRenderer>().sharedMaterial = material;
        }

        private void ConstructMesh()
        {
            terrainMesh = new TerrainMesh(shapeGenerator, meshFilter.sharedMesh, resolution, Vector3.up, terrainGameObject.transform.position, size);
            terrainMesh.ConstructMesh();
            
            terrainGameObject.AddComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
        }

        public void UpdateChunk(Vector2 worldInstancesChunkCoordinate, float viewDistance, Vector2 gameObject)
        {
            float viewerDistanceFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(worldInstancesChunkCoordinate * size));
            SetVisible(viewerDistanceFromNearestEdge <= viewDistance, gameObject);
        }

        public void SetVisible(bool visible, Vector2? gameObject)
        {
            if(gameObject == null)
            {
                terrainGameObject.SetActive(visible);
                return;
            }
            if (visible)
            {
                terrainGameObject.SetActive(true);
                if (!gameObjectsThatSeeThisChunk.Contains(gameObject.Value))
                    gameObjectsThatSeeThisChunk.Add(gameObject.Value);
            }
            else
            {
                if (gameObjectsThatSeeThisChunk.Contains(gameObject.Value))
                    gameObjectsThatSeeThisChunk.Remove(gameObject.Value);
                if(gameObjectsThatSeeThisChunk.Count == 0)
                    terrainGameObject.SetActive(false);
            }
        }

        public bool IsVisible()
        {
            return terrainGameObject.activeSelf;
        }
    }
}