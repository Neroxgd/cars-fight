using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Circulation : MonoBehaviour
{
    [SerializeField] private int timeStart;
    [SerializeField] private Transform[] intersection;
    [SerializeField] private AnimationCurve animationCurve;
    private bool SmoothIntersection = false;
    private int compterintersection = 0;
    [SerializeField] private float speed;

    void Start()
    {
        StartCoroutine(TimeStart());
        transform.LookAt(intersection[1].position);
    }

    void _Circulation()
    {
        if (Vector3.Distance(transform.position, intersection[compterintersection].position) < 3)
        {
            compterintersection++;
            
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, intersection[compterintersection].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, intersection[intersection.Length - 1].position) < 3)
        {
            compterintersection = 0;
            transform.position = intersection[0].position;
        }
    }

    void Update()
    {
        _Circulation();
        Vector3 direction = intersection[compterintersection + 1].position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        if (Vector3.Distance(transform.position, intersection[compterintersection].position) < 3 && compterintersection > 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
    }

    IEnumerator TimeStart()
    {
        yield return new WaitForSeconds(timeStart);
        _Circulation();
        SmoothIntersection = true;
    }

    IEnumerator Turn()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}
