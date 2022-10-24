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

        if (other.gameObject.CompareTag ("trottoire") && _Car.transform.position.y < 0.29f && _Car_input.returnpuissance() > 1)
        {
            _Car.transform.Translate(new Vector3(0,1f,0));
            // _rbParent.AddForce(_Car.transform.forward * _Car_input.returnpuissance());
        }
            
    }
    void Awake()
    {
        //ignore collider collision with parent
        Physics.IgnoreCollision(GetComponent<Collider>(), _colliderParent);
    }
}
