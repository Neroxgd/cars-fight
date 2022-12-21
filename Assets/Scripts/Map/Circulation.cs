using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Circulation : MonoBehaviour
{
    [SerializeField] private int timeStart;
    [SerializeField] private Transform[] intersection;
    private List<Vector3> intersectionPoint = new List<Vector3>();
    private bool SmoothIntersection = false;
    private int compterintersection = 0;
    private float InterpolateAmount;
    [SerializeField] private float speed;

    void Start()
    {
        StartCoroutine(TimeStart());
        transform.LookAt(intersection[1].position);
        foreach (var _intersection in intersection)
            intersectionPoint.Add(_intersection.position);
    }

    void _Circulation()
    {
        /*if (Vector3.Distance(transform.position, intersection[compterintersection].position) < 3)
        {
            compterintersection++;
            
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, intersection[compterintersection].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, intersection[intersection.Length - 1].position) < 3)
        {
            compterintersection = 0;
            transform.position = intersection[0].position;
        }*/
        InterpolateAmount = (InterpolateAmount + Time.deltaTime) % 1;
        transform.position = GenerateSpline(intersectionPoint.ToArray(), InterpolateAmount);
    }

    private Vector3 GenerateSpline(Vector3[] intersections, float t)
    {
        List<Vector3> ListCCI = new List<Vector3>();
        Vector3 CalculCurrentIntersection = intersections[0];
        for (int i = 0; i < intersections.Length; i++)
        {
            CalculCurrentIntersection = Vector3.Lerp(CalculCurrentIntersection, intersections[i], t);
            ListCCI.Add(CalculCurrentIntersection);
        }
        if (ListCCI.Count > 1)
            return GenerateSpline(ListCCI.ToArray(), t);
        return ListCCI[0];
    }

    void Update()
    {
        _Circulation();
        /*Vector3 direction = intersection[compterintersection + 1].position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        if (Vector3.Distance(transform.position, intersection[compterintersection].position) < 3 && compterintersection > 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);*/


    }

    IEnumerator TimeStart()
    {
        yield return new WaitForSeconds(timeStart);
        _Circulation();
        SmoothIntersection = true;
    }
}
