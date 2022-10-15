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
    [SerializeField] private float maxTurnAngle = 45f;
    private float currentTurnAngle = 0;
    private void OnEnable() { _inputedirection.Enable(); }
    private void OnDisable() { _inputedirection.Disable(); }
    private float _rotation = 0;
    [SerializeField] private int puissance = 0;
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

        //increase the puissance
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
    }

    //apply the instante force
    public void addForce()
    {
        _rigidebody.AddForce(transform.forward * puissance, ForceMode.Impulse);
        puissance = 0;
    }
}
