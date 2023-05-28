using System;
using UnityEngine;
using Funlary.Unit5.ColourBridge.MeshGenerationModule.Enum;
    
namespace Funlary.Unit5.ColourBridge.MeshGenerationModule
{
    public class MeshGeneration : AbstractSingleton<MeshGeneration>
    {
        [SerializeField] private Material meshMaterial;
        public void CreateMesh(MeshType meshType,  Transform startPoint,  Transform endPoint, float bridgeWidth, float bridgeLength, float bridgeHeight, Transform meshParent)
        {
            GameObject meshObject = new GameObject("WalkablePlane");
            meshObject.transform.parent = meshParent;
            meshObject.transform.localPosition = Vector3.zero;
            
            MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();
            MeshCollider meshCollider = meshObject.AddComponent<MeshCollider>();
            Mesh mesh = BuildPlaneMesh(bridgeWidth, bridgeLength, bridgeHeight, meshParent);

            meshRenderer.material = meshMaterial;
            meshCollider.sharedMesh = mesh;

            meshFilter.mesh = mesh;
            meshObject.transform.rotation = CalculateRotation(startPoint, endPoint);
        }

        private Quaternion CalculateRotation(Transform startPoint, Transform endPoint)
        {
            Vector3 direction = endPoint.position - startPoint.position;
            return Quaternion.LookRotation(direction);
        }
        
        private Mesh BuildPlaneMesh(float bridgeWidth, float bridgeLength, float bridgeHeight, Transform meshParent, int segmentCount = 5)
        {
            float height = 0f;
            float width = bridgeWidth / 2;
            float length = Mathf.Sqrt(Mathf.Pow(bridgeLength,2) + Mathf.Pow(bridgeHeight,2));
            
            Vector3[] vertices = new Vector3[4]
            {
                new Vector3(width, 0, 0f),
                new Vector3(-width, 0, 0f),
                new Vector3(width, 0, length),
                new Vector3(-width, 0, length)
            };

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
