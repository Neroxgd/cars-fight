using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _carPosition;

    void LateUpdate()
    {
        transform.position = _carPosition.position;
    }

    void FixedUpdate()
    {
        transform.rotation = new Quaternion(transform.rotation.x, _carPosition.rotation.y, transform.rotation.z, transform.rotation.w);
    }
}
