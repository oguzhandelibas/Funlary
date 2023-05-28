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
            Mesh mesh = BuildPlaneMesh(startPoint, endPoint, bridgeWidth, bridgeLength, bridgeHeight, meshParent);

            meshRenderer.material = meshMaterial;
            meshCollider.sharedMesh = mesh;
            
            /*
            switch (meshType)
            {
                case MeshType.PLANE:
                    mesh = CreatePlaneMesh(startPoint, endPoint);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(meshType), meshType, null);
            }*/

            meshFilter.mesh = mesh;
            meshObject.transform.rotation = CalculateRotation(startPoint, endPoint);
        }

        private Quaternion CalculateRotation(Transform startPoint, Transform endPoint)
        {
            Vector3 direction = endPoint.position - startPoint.position;
            return Quaternion.LookRotation(direction);
        }
        
        private Mesh BuildPlaneMesh(Transform startPoint, Transform endPoint, float bridgeWidth, float bridgeLength, float bridgeHeight, Transform meshParent, int segmentCount = 5)
        {
            // Başlangıç ve bitiş noktalarının düzlemlerini alıyoruz
            Vector3 startPointPosition = startPoint.position;
            Vector3 endPointPosition = endPoint.position;
            
            // Yükseklik değeri olarak 0 kabul ediyoruz, isterseniz farklı bir yükseklik değeri de verebilirsiniz
            float height = 0f;

            // Plane'in genişlik ve uzunluk değerlerini hesaplıyoruz
            float width = bridgeWidth / 2;
            float length = Mathf.Sqrt(Mathf.Pow(bridgeLength,2) + Mathf.Pow(bridgeHeight,2));

            // Plane için vertex, triangle ve uv verilerini oluşturuyoruz
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

            // Mesh nesnesini oluşturup verileri atıyoruz
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            
            return mesh;
        }
    }
}
