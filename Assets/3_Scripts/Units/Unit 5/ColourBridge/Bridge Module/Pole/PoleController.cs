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
            endPole.localPosition = endPosition + Vector3.up;
            CreateMeshAndRope(MeshType.BRIDGE_ROPE, endPoint.position.z - startPoint.position.z);
        }

        public void CreateMeshAndRope(MeshType meshType, float meshLength)
        {
            MeshGeneration.Instance.CreateMesh(
                meshType, MeshRotationType.LEFT,
                5, meshLength, 5, new Vector3(startPoint.position.x, startPoint.position.y - 1, startPoint.position.z),
                startPoint, endPoint, this.transform
            );

            Vector3 startPos = startPoint.position;
            Vector3 endPos = endPoint.position;

            if (meshType == MeshType.HORIZONTAL_WALL || meshType == MeshType.VERTICAL_WALL)
            {
                startPos += Vector3.up *1;
                endPos += Vector3.up*1;
            }

            Vector3[] curvePositions = bezierCurve.DrawQuadraticCurve(resolution, height, startPos, endPos);

            lineRenderer.positionCount = resolution + 1;
            lineRenderer.SetPositions(curvePositions);
        }
    }
}
