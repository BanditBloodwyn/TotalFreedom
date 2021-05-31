using System.Collections.Generic;
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
        private readonly Material terrainMaterial;
        private readonly Material outlineMaterial;
        private readonly Material waterMaterial;

        private readonly Vector2 chunkPosition2D; // chunk coordinates, not worldspace coordinates
        private Bounds bounds;
        private readonly int size;
        private readonly float waterLevel;

        private readonly List<Vector2> gameObjectsThatSeeThisChunk = new List<Vector2>();

        public TerrainChunk(
            ShapeGenerator shapeGenerator, 
            Vector2 coord, 
            int size,
            float waterLevel,
            Transform parent, 
            Material terrainMaterial,
            Material outlineMaterial, 
            Material waterMaterial)
        {
            this.shapeGenerator = shapeGenerator;
            this.size = size;
            this.waterLevel = waterLevel;
            this.terrainMaterial = terrainMaterial;
            this.outlineMaterial = outlineMaterial;
            this.waterMaterial = waterMaterial;

            chunkPosition2D = coord * size;
            bounds = new Bounds(chunkPosition2D, Vector2.one * size);

            CreateGameObject(parent);
            CreateMeshFilter();
            ConstructMesh();
            CreateWater();
            SetVisible(false, null);
        }

        private void CreateGameObject(Transform parent)
        {
            terrainGameObject = new GameObject("TerrainObject");
            terrainGameObject.transform.parent = parent;
            terrainGameObject.transform.position = new Vector3(chunkPosition2D.x, 0, chunkPosition2D.y);
            terrainGameObject.AddComponent<MeshRenderer>();
            terrainGameObject.layer = 3;
            terrainGameObject.tag = "Walkable";
        }

        private void CreateMeshFilter()
        {
            meshFilter = terrainGameObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();

            meshFilter.GetComponent<MeshRenderer>().materials = new[] { terrainMaterial , outlineMaterial};
        }

        private void ConstructMesh()
        {
            terrainMesh = new TerrainMesh(shapeGenerator, meshFilter.sharedMesh, resolution, Vector3.up, terrainGameObject.transform.position, size);
            terrainMesh.ConstructMesh();
            
            terrainGameObject.AddComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
        }

        private void CreateWater()
        {
            if (!terrainMesh.NeedsWater(waterLevel))
                return;

            GameObject waterGameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            waterGameObject.name = "WaterPlane";
            waterGameObject.transform.parent = terrainGameObject.transform;
            waterGameObject.transform.position = new Vector3(chunkPosition2D.x, waterLevel, chunkPosition2D.y);
            waterGameObject.transform.localScale = new Vector3(size/10f, 0.1f, size/10f);
            waterGameObject.GetComponent<MeshRenderer>().material = waterMaterial;
            Object.Destroy(waterGameObject.GetComponent<MeshCollider>());
            waterGameObject.AddComponent<BoxCollider>();
            waterGameObject.GetComponent<BoxCollider>().isTrigger = true;
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