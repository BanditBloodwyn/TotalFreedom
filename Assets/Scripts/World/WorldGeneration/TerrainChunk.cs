using System.Collections.Generic;
using Assets.Scripts.Camera;
using Assets.Scripts.Gameplay.Characters.Player;
using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration
{
    public class TerrainChunk
    {
        private GameObject terrainGameObject;
        private readonly Vector2 chunkPosition2D; // chunk coordinates, not worldspace coordinates
        private Bounds bounds;
        private readonly int size;

        private readonly List<Vector2> gameObjectsThatSeeThisChunk = new List<Vector2>();

        public TerrainChunk(Vector2 coord, int size, Transform parent, Material material, GameObject Player)
        {
            this.size = size;
            chunkPosition2D = coord * size;
            bounds = new Bounds(chunkPosition2D, Vector2.one * size);

            CreateGameObject(parent, material, Player);
            SetVisible(false, null);
        }

        private void CreateGameObject(Transform parent, Material material, GameObject player)
        {
            terrainGameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            terrainGameObject.transform.parent = parent;
            terrainGameObject.transform.position = new Vector3(chunkPosition2D.x, 0, chunkPosition2D.y);
            terrainGameObject.transform.localScale = Vector3.one * size / 10f;
            terrainGameObject.GetComponent<MeshRenderer>().sharedMaterial = material;
            terrainGameObject.layer = 3;
            terrainGameObject.tag = "Walkable";
            terrainGameObject.AddComponent<ClickToSetPlayerTarget>();
            terrainGameObject.GetComponent<ClickToSetPlayerTarget>().walkingScript = player.GetComponent<WalkToClickedPosition>();
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