using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circulation : MonoBehaviour
{
    [SerializeField] private int timeStart;
    private bool SmoothIntersection = false;
    private float InterpolateAmount;
    [SerializeField] private float speed;
    [SerializeField] private Spline spline;

    void Start()
    {
        StartCoroutine(TimeStart());
        transform.LookAt(spline.Intersection[1].position);
    }

    private void _Circulation()
    {
        List<Vector3> intersectionPoint = new List<Vector3>();
        foreach (var _intersection in spline.Intersection)
            intersectionPoint.Add(_intersection.position);
        InterpolateAmount = (InterpolateAmount + (Time.deltaTime * speed)) % 1;
        Vector3 Spline = spline.GenerateSpline(intersectionPoint, InterpolateAmount);
        transform.LookAt(Spline);
        transform.position = Spline;
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
