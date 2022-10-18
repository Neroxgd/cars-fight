using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_ragdoll_active : MonoBehaviour
{
    [SerializeField] private Rigidbody _rbParent;
    [SerializeField] private Collider _colliderParent;
    [SerializeField] private Car_input _Car_input;

    void OnTriggerEnter(Collider other)
    {
        //disabled freeze rotation on parent
        _rbParent.constraints = RigidbodyConstraints.None;
        _Car_input.puissance0();
    }
    void Awake()
    {
        //ignore collider collision with parent
        Physics.IgnoreCollision(GetComponent<Collider>(), _colliderParent);
    }
}
