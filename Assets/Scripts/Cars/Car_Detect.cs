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
    }
    void Awake()
    {
        //ignore collider collision with parent
        Physics.IgnoreCollision(GetComponent<Collider>(), _colliderParent);
    }

    void Update()
    {
        //pass on the trottoire
        if (_Car_input.ReturnSpeed < 10) return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f * (_Car_input.ReturnSpeed / 10f)) && hit.transform.CompareTag("trottoire") && _Car.transform.position.y - hit.transform.position.y < 0.2f)
            _Car.transform.position += Vector3.up * 0.2f;

    }
}
