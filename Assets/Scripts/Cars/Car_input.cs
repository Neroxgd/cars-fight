using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_input : MonoBehaviour
{
    private Rigidbody _rigidebody;
    [SerializeField] [Range(1, 1000)] private int speedCar = 5;
    void Start()
    {
        _rigidebody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidebody.AddForce(Vector3.forward*speedCar*Time.deltaTime);
    }
}
