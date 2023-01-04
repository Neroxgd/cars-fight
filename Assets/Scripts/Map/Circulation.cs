using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Circulation : MonoBehaviour
{
    [SerializeField] private int timeStart;
    [SerializeField] private Transform[] intersection;
    private bool SmoothIntersection = false;
    private float InterpolateAmount;
    [SerializeField] private float speed;
    private List<Vector3> DrawingSpline;

    void Start()
    {
        StartCoroutine(TimeStart());
        transform.LookAt(intersection[1].position);
    }

    private void _Circulation()
    {
        List<Vector3> intersectionPoint = new List<Vector3>();
        foreach (var _intersection in intersection)
            intersectionPoint.Add(_intersection.position);
        InterpolateAmount = (InterpolateAmount + (Time.deltaTime * speed)) % 1;
        Vector3 Spline = GenerateSpline(intersectionPoint, InterpolateAmount);
        transform.LookAt(Spline);
        transform.position = Spline;
    }

    private Vector3 GenerateSpline(List<Vector3> intersections, float t)
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

    void Update()
    {
        if (SmoothIntersection)
            _Circulation();
    }

    IEnumerator TimeStart()
    {
        yield return new WaitForSeconds(timeStart);
        SmoothIntersection = true;
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
