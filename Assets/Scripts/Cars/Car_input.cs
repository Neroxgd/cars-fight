using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car_input : MonoBehaviour
{
    private Rigidbody _rigidebody;
    [SerializeField][Range(10, 10000)] private int speedCar = 1000;
    [SerializeField][Range(1, 5)] private int speed_rotate = 5;
    [SerializeField] private InputAction _inputedirection;
    [SerializeField] private WheelCollider _FR;
    [SerializeField] private WheelCollider _FL;
    [SerializeField] private WheelCollider _BR;
    [SerializeField] private WheelCollider _BL;
    // [SerializeField] private float acceleration = 500f;
    // [SerializeField] private float breakingForce = 300f;
    [SerializeField] private float maxTurnAngle = 45f;
    // private float currentAcceleration = 0;
    // private float currentBreakForce = 0;
    private float currentTurnAngle = 0;
    // [SerializeField] private InputAction _inputeforce;
    private void OnEnable() { _inputedirection.Enable(); }
    private void OnDisable() { _inputedirection.Disable(); }
    private float _rotation = 0;
    [SerializeField] private int puissance = 0;
    private bool inHold = false;
    void Start()
    {
        _rigidebody = GetComponent<Rigidbody>();
        _rigidebody.AddForce(transform.forward*1000, ForceMode.Impulse);
    }

    void Update()
    {
        _rotation = _inputedirection.ReadValue<Vector2>().x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Keyboard.current.spaceKey.isPressed)
            currentBreakForce = breakingForce;
        else
            currentBreakForce = 0;
        _FR.motorTorque = currentAcceleration;
        _FL.motorTorque = currentAcceleration;

        _FR.brakeTorque = currentBreakForce;
        _FL.brakeTorque = currentBreakForce;
        _BR.brakeTorque = currentBreakForce;
        _BL.brakeTorque = currentBreakForce;*/

        currentTurnAngle = maxTurnAngle * _rotation;
        _FL.steerAngle = currentTurnAngle;
        _FR.steerAngle = currentTurnAngle;
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
            addForce();
        }
        // transform.Rotate(new Vector3(0, _rotation, 0) * speed_rotate, Space.Self);
        // Vector3 _vecocity = _rigidebody.velocity;
        // _rigidebody.velocity = Vector3.zero;
        // _rigidebody.AddForce(_vecocity + (new Vector3(_rotation, 0, 0) * speed_rotate), ForceMode.Impulse);
        Debug.Log(_FR.motorTorque);
    }

    public void addForce()
    {
        _rigidebody.AddForce(transform.forward * puissance, ForceMode.Impulse);
        
        // _FR.brakeTorque = puissance;
        // _FL.brakeTorque = puissance;
        puissance = 0;
    }
}
