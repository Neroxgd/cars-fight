using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car_input : MonoBehaviour
{
    private Rigidbody _rigidebody;
    [SerializeField][Range(1, 100)] private int speedCar = 5;
    [SerializeField][Range(1, 5)] private int speed_rotate = 5;
    [SerializeField] private InputAction _inputeAction;
    private void OnEnable() { _inputeAction.Enable(); }
    private void OnDisable() { _inputeAction.Disable(); }
    private float _rotation = 0;
    void Start()
    {
        _rigidebody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        _rotation = _inputeAction.ReadValue<Vector2>().x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, _rotation, 0) * speed_rotate, Space.Self);
        _rigidebody.velocity = transform.right * speedCar;
        Debug.Log(_rotation);
    }
}
