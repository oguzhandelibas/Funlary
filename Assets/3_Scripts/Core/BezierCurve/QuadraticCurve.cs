using UnityEngine;

namespace Funlary
{
    public class QuadraticCurve
    {
        public Vector3[] QuadraticBezierCurve(int resolution, float height, Vector3 start, Vector3 end, Vector3 midPoint = default)
        {
            Vector3[] curvePositions = new Vector3[resolution + 1];
            if(midPoint==default)midPoint = ((start + end) / 2) + (Vector3.up * height);
            
            for (int i = 0; i <= resolution; i++)
            {
                float t = i / (float)resolution;
                curvePositions[i] = CalculateQuadraticBezierPoint(start, midPoint, end, t);
            }

            return curvePositions;
            
        }
        
        private Vector3 CalculateQuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t)
        {
            float u = 1f - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 point = uu * start + 2f * u * t * control + tt * end;
            return point;
        }
    }
}
