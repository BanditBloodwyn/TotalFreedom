using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration
{
    public class TerrainMesh
    {
        private readonly Mesh mesh;
        private readonly int resolution;

        private readonly Vector3 localUp;
        private readonly Vector3 axisA;
        private readonly Vector3 axisB;
        private readonly ShapeGenerator shapeGenerator;
        private readonly Vector3 position;
        private readonly float size;

        public TerrainMesh(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp, Vector3 position, float size)
        {
            this.shapeGenerator = shapeGenerator;
            this.mesh = mesh;
            this.resolution = resolution;
            this.localUp = localUp;
            this.position = position;
            this.size = size;

            axisA = new Vector3(localUp.y, localUp.z, localUp.x);
            axisB = Vector3.Cross(localUp, axisA);
        }

        public void ConstructMesh()
        {
            Vector3[] vertices = new Vector3[resolution * resolution];
            int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
            int triangleIndex = 0;

            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    int index = x + y * resolution;

                    Vector2 percent = new Vector2(x, y) / (resolution - 1);
                    Vector3 pointOnWorld = position * 2f / size + localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;
                    vertices[index] = shapeGenerator.CalculatePointOnWorld(pointOnWorld, size, position);

                    if (x != resolution - 1 && y != resolution - 1)
                    {
                        triangles[triangleIndex] = index;
                        triangles[triangleIndex + 1] = index + resolution + 1;
                        triangles[triangleIndex + 2] = index + resolution;

                        triangles[triangleIndex + 3] = index;
                        triangles[triangleIndex + 4] = index + 1;
                        triangles[triangleIndex + 5] = index + resolution + 1;

                        triangleIndex += 6;
                    }
                }
            }

            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
    }
}
