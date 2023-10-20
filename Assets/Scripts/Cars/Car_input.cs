using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car_input : MonoBehaviour
{
    private Rigidbody rbCar;
    [SerializeField] private InputAction _inputedirection;
    [SerializeField] private WheelCollider _FR, _FL;
    [SerializeField] private float maxTurnAngle = 70f, currentTurnAngle = 0, carPower, speedTurnRespawn, speedTurn;
    private void OnEnable() { _inputedirection.Enable(); }
    private void OnDisable() { _inputedirection.Disable(); }
    private float _rotation = 0, carCurrentPower, currentAngle = 0.5f, carForwardAxe, carTurnAxe;
    public float ReturnSpeed { get { return rbCar.velocity.magnitude; } }
    private bool increasePower, isReswpawningSamePos, isPlaying;

    void Start()
    {
        rbCar = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isReswpawningSamePos)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + carTurnAxe * Time.deltaTime * speedTurnRespawn, transform.eulerAngles.z);
            return;
        }
        //get the horizontal axe input values
        _rotation = _inputedirection.ReadValue<Vector2>().x;
        if (increasePower)
            carCurrentPower += 1 * Time.deltaTime * carPower * carForwardAxe;
        if (rbCar.velocity.magnitude < 0.1f)
        {
            rbCar.velocity = Vector3.zero;
            isPlaying = false;
        }
    }

    public void IncreasePower(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            increasePower = true;
            carForwardAxe = context.ReadValue<float>();
        }
        else if (context.canceled)
        {
            increasePower = false;
            isPlaying = true;
            rbCar.velocity = transform.forward * carCurrentPower;
            carCurrentPower = 0;
        }
    }

    public void TurnCar(InputAction.CallbackContext context)
    {
        carTurnAxe = context.ReadValue<float>();
    }

    public void RespawnSamePos(InputAction.CallbackContext context)
    {
        if (isPlaying) return;
        if (context.started)
        {
            transform.eulerAngles = Vector3.up * transform.eulerAngles.y;
            transform.Translate(Vector3.up * 0.5f);
            rbCar.isKinematic = true;
            isReswpawningSamePos = true;
        }
        if (context.canceled)
        {
            isReswpawningSamePos = false;
            rbCar.isKinematic = false;
        }

    }

    void FixedUpdate()
    {
        //turn the wheels
        currentTurnAngle = Mathf.Lerp(maxTurnAngle, -maxTurnAngle, currentAngle);
        if (Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed)
            currentAngle = Mathf.Clamp(currentAngle, 0, 1) + (-_rotation / 10);
        else
        {
            if (currentAngle >= 0.6f)
                currentAngle -= 0.08f * Time.fixedDeltaTime * 5;
            else if (currentAngle <= 0.4f)
                currentAngle += 0.08f * Time.fixedDeltaTime * 5;
            else
                currentAngle = 0.5f;
        }

        _FL.steerAngle = currentTurnAngle;
        _FR.steerAngle = currentTurnAngle;

        if (isPlaying)
            rbCar.velocity = new Vector3(transform.forward.x* rbCar.velocity.magnitude * carForwardAxe, rbCar.velocity.y, transform.forward.z* rbCar.velocity.magnitude * carForwardAxe);
        transform.rotation = Quaternion.Lerp(_FL.transform.rotation, _FR.transform.rotation, Mathf.Pow(rbCar.velocity.magnitude, 1f / 2f) / 10f);
    }
}
