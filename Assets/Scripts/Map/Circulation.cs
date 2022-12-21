using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Circulation : MonoBehaviour
{
    [SerializeField] private int timeStart;
    [SerializeField] private Transform[] intersection;
    private bool SmoothIntersection = false;
    private int compterintersection = 0;
    private float InterpolateAmount;
    [SerializeField] private float speed;

    void Start()
    {
        StartCoroutine(TimeStart());
        transform.LookAt(intersection[1].position);
    }

    void _Circulation()
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
        List<Vector3> ListCCI = new List<Vector3>();
        if (intersections.Count == 1)
            return intersections[0];

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
}
