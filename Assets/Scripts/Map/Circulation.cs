using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Circulation : MonoBehaviour
{
    [SerializeField] private int timeStart;
    [SerializeField] private Transform[] intersection;

    void Start()
    {
        StartCoroutine(TimeStart());
    }

    void _Circulation()
    {
        var sequence = DOTween.Sequence();
        foreach (var _intersection in intersection)
        {
            sequence.Append(transform.DOLookAt(_intersection.position, 0.5f));
            sequence.Append(transform.DOMove(_intersection.position, 1));
            
            sequence.SetLoops(-1, LoopType.Restart);
        }
        
    }

    IEnumerator TimeStart()
    {
        yield return new WaitForSeconds(timeStart);
        _Circulation();
    }
}
