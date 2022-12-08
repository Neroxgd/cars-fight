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
    private float savepuissance;
    public float returnpuissance() { return savepuissance; }
    private bool inHold = false;
    private bool playturn = false;
    private bool endturn = true;
    private float currentAngle = 0.5f;
    [SerializeField] private float _SlowDownCar = 0.1f;
    private float lerpSlowDownCar = 1;
    void Start()
    {
        _rigidebody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //get the horizontal axe input values
        _rotation = _inputedirection.ReadValue<Vector2>().x;
    }

    void FixedUpdate()
    {
        savepuissance = puissance;
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

        //increase the power
        if (Keyboard.current.wKey.isPressed && !playturn)
        {
            puissance += speedCar * Time.fixedDeltaTime;
            inHold = true;
        }
        else if (Keyboard.current.sKey.isPressed && !playturn)
        {
            puissance -= speedCar * Time.fixedDeltaTime;
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
            puissance -= _SlowDownCar * Time.fixedDeltaTime;
            _rigidebody.velocity = transform.forward * puissance;
        }

        SlowDownCar();
    }

    //slow down the car
    public void SlowDownCar()
    {
        _rigidebody.velocity = Vector3.Lerp(Vector3.zero, _rigidebody.velocity, lerpSlowDownCar);
        lerpSlowDownCar -= _SlowDownCar * _rigidebody.velocity.magnitude * Time.fixedDeltaTime;
        lerpSlowDownCar = Mathf.Clamp(lerpSlowDownCar, 0, 1);

        if (((_rigidebody.velocity.x < 0.1f && _rigidebody.velocity.x > -0.1f) && (_rigidebody.velocity.z < 0.1f && _rigidebody.velocity.z > -0.1f)) && playturn && endturn)
        {
            _rigidebody.velocity = Vector3.zero;
            Debug.Log("geeeee");
            endturn = false;
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
