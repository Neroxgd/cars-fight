using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car_input : MonoBehaviour
{
    private Rigidbody _rigidebody;
    [SerializeField][Range(1, 100)] private int speedCar = 5;
    [SerializeField][Range(1, 5)] private int speed_rotate = 5;
    [SerializeField] private InputAction _inputedirection;
    // [SerializeField] private InputAction _inputeforce;
    private void OnEnable() { _inputedirection.Enable(); }
    private void OnDisable() { _inputedirection.Disable(); }
    private float _rotation = 0;
    [SerializeField] private int puissance = 0;
    private bool inHold = false;
    void Start()
    {
        _rigidebody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        _rotation = _inputedirection.ReadValue<Vector2>().x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (Keyboard.current.wKey.isPressed)
        {
            puissance++;
            inHold = true;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            puissance--;
            inHold = true;
        }
        else
        {
            inHold = false;
            addForce();
        }
        transform.Rotate(new Vector3(0, _rotation, 0) * speed_rotate, Space.Self);
        Vector3 _vecocity = _rigidebody.velocity;
        _rigidebody.velocity = Vector3.zero;
        _rigidebody.AddForce(_vecocity + (new Vector3(_rotation, 0, 0) * speed_rotate), ForceMode.Impulse);
        Debug.Log(_rigidebody.velocity);*/
    }

    /*public void addForce()
    {
        _rigidebody.AddForce(transform.right * puissance, ForceMode.Impulse);
        puissance = 0;
    }*/
}
