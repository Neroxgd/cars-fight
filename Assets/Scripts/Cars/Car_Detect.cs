using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Detect : MonoBehaviour
{
    [SerializeField] private Rigidbody _rbParent;
    [SerializeField] private GameObject _Car;
    [SerializeField] private Collider _colliderParent;
    [SerializeField] private Car_input _Car_input;

    void OnTriggerEnter(Collider other)
    {
        //disabled freeze rotation on parent and help for the sidewalks
        _rbParent.constraints = RigidbodyConstraints.None;
        _Car_input.puissance0();

        //pass on the trottoire
        if (other.gameObject.CompareTag ("trottoire") && _Car.transform.position.y < 0.29f && _Car_input.returnpuissance() > 1)
        {
            _rbParent.velocity += Vector3.up*2;
        }
            
    }
    void Awake()
    {
        //ignore collider collision with parent
        Physics.IgnoreCollision(GetComponent<Collider>(), _colliderParent);
    }
}
