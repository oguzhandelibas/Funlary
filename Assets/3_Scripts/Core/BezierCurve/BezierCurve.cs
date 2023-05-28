using UnityEngine;

namespace Funlary
{
    public class BezierCurve
    {
        public Vector3[] DrawQuadraticCurve(int resolution, float height, Vector3 start, Vector3 end, Vector3 midPoint = default)
        {
            Vector3[] curvePositions = new Vector3[resolution + 1];
            
            QuadraticCurve quadraticCurveCurve = new QuadraticCurve();
            curvePositions = quadraticCurveCurve.QuadraticBezierCurve(resolution, height, start, end, midPoint);

            return curvePositions;
        }
        
        public Vector3[] DrawCubicCurve(int resolution, float height, Vector3 start, Vector3 end, Vector3 control1, Vector3 control2)
        {
            Vector3[] curvePositions = new Vector3[resolution + 1];
            
            Cubic cubicCurveCurve = new Cubic();
            curvePositions = cubicCurveCurve.CubicBezierCurve(resolution, height, start, end, control1, control2);

            return curvePositions;
        }
    }
}
