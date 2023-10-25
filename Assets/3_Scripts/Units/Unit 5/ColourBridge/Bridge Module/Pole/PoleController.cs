using System;
using Funlary.MeshGenerationModule;
using Funlary.MeshGenerationModule.Enum;
using UnityEngine;

namespace Funlary
{
    public class PoleController : MonoBehaviour
    {
        [Header("Line Renderer")]
        [SerializeField] private LineRenderer lineRenderer;
        
        [Header("Indicator Transform")]
        public Transform startPoint;
        public Transform endPoint;
        [SerializeField] private Transform endPole;

        [Header("Variables")] 
        [SerializeField] private float height = -2.0f;
        [SerializeField] private int resolution  = 100;
        BezierCurve bezierCurve = new BezierCurve();

        public void SetEndPolePosition(Vector3 endPosition)
        {
            endPole.localPosition = endPosition;
            CreateMeshAndRope(MeshType.BRIDGE, endPoint.position.z - startPoint.position.z);
        }

        public void CreateMeshAndRope(MeshType meshType, float meshLength)
        {
            MeshGeneration.Instance.CreateMesh(
                meshType, MeshRotationType.RIGHT,
                5, meshLength, 5, new Vector3(startPoint.position.x, startPoint.position.y - 1, startPoint.position.z),
                startPoint, endPoint, this.transform
            );

            Vector3[] curvePositions = bezierCurve.DrawQuadraticCurve(resolution, height, startPoint.position + Vector3.up * 1, endPoint.position + Vector3.up * 1);

            lineRenderer.positionCount = resolution + 1;
            lineRenderer.SetPositions(curvePositions);
        }
    }
}
