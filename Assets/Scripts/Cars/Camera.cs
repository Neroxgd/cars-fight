using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _carPosition;

    void LateUpdate()
    {
        transform.position = _carPosition.position + new Vector3(3, 2, 0);
    }
}
