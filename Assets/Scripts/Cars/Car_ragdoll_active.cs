using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_ragdoll_active : MonoBehaviour
{
    [SerializeField] private Rigidbody _rbParent;
    [SerializeField] private Collider _colliderParent;

    void OnTriggerEnter(Collider other)
    {
        //disabled freeze rotation on parent
        _rbParent.constraints = RigidbodyConstraints.None;
    }
    void Awake()
    {
        //ignore collider collision with parent
        Physics.IgnoreCollision(GetComponent<Collider>(), _colliderParent);
    }
}
