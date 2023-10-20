using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    private List<Vector3> DrawingSpline;
    public Transform[] Intersection { get { return intersection; } }
    [SerializeField] private Transform[] intersection;

    public Vector3 GenerateSpline(List<Vector3> intersections, float t)
    {
        if (intersections.Count == 1)
            return intersections[0];

        List<Vector3> ListCCI = new List<Vector3>();
        Vector3 CalculCurrentIntersection;
        for (int i = 0; i < intersections.Count - 1; i++)
        {
            CalculCurrentIntersection = Vector3.Lerp(intersections[i], intersections[i + 1], t);
            ListCCI.Add(CalculCurrentIntersection);
        }
        return GenerateSpline(ListCCI, t);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (var point in intersection)
            Gizmos.DrawSphere(point.position, 0.2f);

        Gizmos.color = Color.white;
        if (DrawingSpline != null)
            for (int i = 0; i < DrawingSpline.Count - 1; i++)
                Gizmos.DrawLine(DrawingSpline[i], DrawingSpline[i + 1]);
    }

    public void DrawSpine()
    {
        DrawingSpline = new List<Vector3>();
        List<Vector3> intersectionPoint = new List<Vector3>();
        foreach (var _intersection in intersection)
            intersectionPoint.Add(_intersection.position);
        float InterpolateAmountDraw = 0;
        while (InterpolateAmountDraw <= 1f)
        {
            InterpolateAmountDraw += 0.001f;
            Vector3 Spline = GenerateSpline(intersectionPoint, InterpolateAmountDraw);
            DrawingSpline.Add(Spline);
        }
    }
}
