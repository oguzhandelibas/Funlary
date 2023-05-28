using UnityEngine;

namespace Funlary
{
    public class Cubic
    {
        public Vector3[] CubicBezierCurve(int resolution, float height, Vector3 start, Vector3 end, Vector3 control1, Vector3 control2)
        {
            Vector3[] curvePositions = new Vector3[resolution + 1];

            for (int i = 0; i <= resolution; i++)
            {
                float t = i / (float)resolution;
                curvePositions[i] = CalculateCubicBezierPoint(start, control1, control2, end, t);
            }

            return curvePositions;
        }
        
        private Vector3 CalculateCubicBezierPoint(Vector3 start, Vector3 control1, Vector3 control2, Vector3 end, float t)
        {
            float u = 1f - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 point = uuu * start + 3f * uu * t * control1 + 3f * u * tt * control2 + ttt * end;
            return point;
        }
    }
}
