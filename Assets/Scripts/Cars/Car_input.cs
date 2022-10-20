using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car_input : MonoBehaviour
{
    private Rigidbody _rigidebody;
    [SerializeField][Range(1, 100)] private int speedCar = 1000;
    [SerializeField] private InputAction _inputedirection;
    [SerializeField] private WheelCollider _FR;
    [SerializeField] private WheelCollider _FL;
    [SerializeField] private float maxTurnAngle = 70f;
    [SerializeField] private float currentTurnAngle = 0;
    private void OnEnable() { _inputedirection.Enable(); }
    private void OnDisable() { _inputedirection.Disable(); }
    private float _rotation = 0;
    [SerializeField] private float puissance = 0;
    private bool inHold = false;
    private bool playturn = false;
    [SerializeField] private float currentAngle = 0.5f;
    [SerializeField] private float _SlowDownCar = 0.1f;
    void Start()
    {
        _rigidebody = GetComponent<Rigidbody>();
        //debug the collision when you spawn
        _rigidebody.AddForce(transform.forward * 5000, ForceMode.Impulse);
    }

    void Update()
    {
        //get the horizontal axe input values
        _rotation = _inputedirection.ReadValue<Vector2>().x;
    }

    void FixedUpdate()
    {
        //turn the wheels
        currentTurnAngle = Mathf.Lerp(maxTurnAngle, -maxTurnAngle, currentAngle);
        if (Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed)
            currentAngle = Mathf.Clamp(currentAngle, 0, 1) + (-_rotation / 50);
        else
        {
            if (currentAngle >= 0.6f)
                currentAngle -= 0.04f;
            else if (currentAngle <= 0.4f)
                currentAngle += 0.04f;
            else
                currentAngle = 0.5f;
        }

        _FL.steerAngle = currentTurnAngle;
        _FR.steerAngle = currentTurnAngle;

        //increase the power
        if (Keyboard.current.wKey.isPressed && !playturn)
        {
            puissance += speedCar;
            inHold = true;
        }
        else if (Keyboard.current.sKey.isPressed && !playturn)
        {
            puissance -= speedCar;
            inHold = true;
        }
        else if (inHold)
        {
            inHold = false;
            StartCoroutine(timePlayturn());
        }

        //help to turn and add instant force
        if (!inHold && puissance > 0)
        {
            puissance -= _SlowDownCar;
            _rigidebody.velocity = transform.forward * puissance;
        }
            
        SlowDownCar();
    }

    //slow down the car
    public void SlowDownCar()
    {
        if (_rigidebody.velocity.x > 0.1f)
            _rigidebody.velocity -= new Vector3(_SlowDownCar, 0, 0);
        else if (_rigidebody.velocity.x < -0.1f)
            _rigidebody.velocity += new Vector3(_SlowDownCar, 0, 0);
        if (_rigidebody.velocity.z > 0.1f)
            _rigidebody.velocity -= new Vector3(0, 0, _SlowDownCar);
        else if (_rigidebody.velocity.z < -0.1f)
            _rigidebody.velocity += new Vector3(0, 0, _SlowDownCar);
        if (((_rigidebody.velocity.x < 0.1f && _rigidebody.velocity.x > -0.1f) && (_rigidebody.velocity.z < 0.1f && _rigidebody.velocity.z > -0.1f)) && playturn)
        {
            _rigidebody.velocity = Vector3.zero;
        }
    }

    IEnumerator timePlayturn()
    {
        yield return new WaitForSeconds(0.5f);
        playturn = true;
    }

    public void puissance0()
    {
        puissance = 0;
    }
}
