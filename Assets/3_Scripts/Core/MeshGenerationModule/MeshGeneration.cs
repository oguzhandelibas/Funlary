using System;
using UnityEngine;
using Funlary.MeshGenerationModule.Enum;
    
namespace Funlary.MeshGenerationModule
{
    public class MeshGeneration : AbstractSingleton<MeshGeneration>
    {
        [SerializeField] private Material meshMaterial;
        public void CreateMesh(MeshType meshType, MeshRotationType meshRotationType,
            float meshWidth, float meshLength, float meshHeight, 
            Transform startPoint, Transform endPoint, Transform meshParent,
            bool isVisible = false)
        {
            GameObject meshObject = new GameObject("CreatedMesh by MeshGenerator");
            meshObject.transform.parent = meshParent;
            meshObject.transform.localPosition = Vector3.zero;
            
            MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();
            MeshCollider meshCollider = meshObject.AddComponent<MeshCollider>();
            Mesh mesh = BuildPlaneMesh(meshRotationType, meshWidth, meshLength, meshHeight, meshParent);

            meshRenderer.material = meshMaterial;
            meshCollider.sharedMesh = mesh;
            meshCollider.convex = true;
            meshFilter.mesh = mesh;
            meshRenderer.enabled = isVisible;
            meshObject.transform.rotation = CalculateRotation(startPoint, endPoint);
        }

        private Quaternion CalculateRotation(Transform startPoint, Transform endPoint)
        {
            Vector3 direction = endPoint.position - startPoint.position;
            return Quaternion.LookRotation(direction);
        }
        
        private Mesh BuildPlaneMesh(MeshRotationType meshRotationType, float bridgeWidth, float bridgeLength, float bridgeHeight, Transform meshParent, int segmentCount = 5)
        {
            float height = 0f;
            float width = bridgeWidth / 2;
            float length = Mathf.Sqrt(Mathf.Pow(bridgeLength,2) + Mathf.Pow(bridgeHeight,2));
            
            Vector3[] vertices = new Vector3[4];
            switch (meshRotationType)
            {
                case MeshRotationType.UP:
                    vertices = new Vector3[4]
                    {
                        new Vector3(width, 0, 0f),
                        new Vector3(-width, 0, 0f),
                        new Vector3(width, 0, length),
                        new Vector3(-width, 0, length)
                    };
                    break;
                case MeshRotationType.DOWN:
                    vertices = new Vector3[4]
                    {
                        new Vector3(-width, 0, 0f),
                        new Vector3(width, 0, 0f),
                        new Vector3(-width, 0, length),
                        new Vector3(width, 0, length)
                    };
                    break;
                case MeshRotationType.RIGHT:
                    vertices = new Vector3[4]
                    {
                        new Vector3(0, 0, 0f),
                        new Vector3(0, width, 0f),
                        new Vector3(0, 0, length),
                        new Vector3(0, width, length)
                    };
                    break;
                case MeshRotationType.LEFT:
                    vertices = new Vector3[4]
                    {
                        new Vector3(0, 0, 0f),
                        new Vector3(0, width, 0f),
                        new Vector3(0, 0, length),
                        new Vector3(0, width, length)
                    };
                    break;
            }

            int[] triangles = new int[6]
            {
                0, 1, 2,
                2, 1, 3
            };

            Vector2[] uv = new Vector2[4]
            {
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(0f, 1f),
                new Vector2(1f, 1f)
            };
            
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            
            return mesh;
        }
    }
}
