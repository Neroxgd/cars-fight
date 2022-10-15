using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car_input : MonoBehaviour
{
    private Rigidbody _rigidebody;
    [SerializeField][Range(10, 10000)] private int speedCar = 1000;
    [SerializeField] private InputAction _inputedirection;
    [SerializeField] private WheelCollider _FR;
    [SerializeField] private WheelCollider _FL;
    [SerializeField] private float maxTurnAngle = 45f;
    private float currentTurnAngle = 0;
    private void OnEnable() { _inputedirection.Enable(); }
    private void OnDisable() { _inputedirection.Disable(); }
    private float _rotation = 0;
    private int puissance = 0;
    private bool inHold = false;
    void Start()
    {
        _rigidebody = GetComponent<Rigidbody>();
        //debug the collision when you spawn
        _rigidebody.AddForce(transform.forward*5000, ForceMode.Impulse);
    }

    void Update()
    {
        //get the horizontal axe input values
        _rotation = _inputedirection.ReadValue<Vector2>().x;
    }

    void FixedUpdate()
    {
        //turn the car
        currentTurnAngle = maxTurnAngle * _rotation;
        _FL.steerAngle = currentTurnAngle;
        _FR.steerAngle = currentTurnAngle;

        //increase the power
        if (Keyboard.current.wKey.isPressed)
        {
            puissance += speedCar;
            inHold = true;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            puissance-= speedCar;
            inHold = true;
        }
        else
        {
            inHold = false;
            addInstantForce();
        }
        SlowDownCar();
        Debug.Log(_rigidebody.velocity);
    }

    //apply the instante force
    public void addInstantForce()
    {
        _rigidebody.AddForce(transform.forward * puissance, ForceMode.Impulse);
        puissance = 0;
    }

    //slow down the car
    public void SlowDownCar()
    {
        if (_rigidebody.velocity.x > 0)
            _rigidebody.velocity -= new Vector3(0.05f, 0, 0);
        else if (_rigidebody.velocity.x < 0)
            _rigidebody.velocity += new Vector3(0.05f, 0, 0);
        if (_rigidebody.velocity.z > 0)
            _rigidebody.velocity -= new Vector3(0, 0, 0.05f);
        else if (_rigidebody.velocity.z < 0)
            _rigidebody.velocity += new Vector3(0, 0, 0.05f);
    }
}
