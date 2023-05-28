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
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private Transform endPole;

        [Header("Variables")] 
        [SerializeField] private float height = -2.0f;
        [SerializeField] private int resolution  = 100;
        BezierCurve bezierCurve = new BezierCurve();

        public void CreateRope(Vector3 endPosition)
        {
            endPole.localPosition = endPosition;
            MeshGeneration.Instance.CreateMesh(
                MeshType.PLANE, MeshRotationType.LEFT,
                5, endPoint.position.z-startPoint.position.z, 1, 
                startPoint, endPoint,this.transform
                );
            
            Vector3[] curvePositions = bezierCurve.DrawQuadraticCurve(resolution, height, startPoint.position, endPoint.position);
            
            lineRenderer.positionCount = resolution + 1;
            lineRenderer.SetPositions(curvePositions);
        }
    }
}