using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _carPosition;
    [SerializeField] private Transform CamPosition;
    [SerializeField] private Rigidbody Cam_rb;
    private Vector3 vit_rot = Vector3.zero;
    // private bool dist_Cam = false;

    void Start()
    {
        // vit_rot = _carPosition.position;
    }

    //cam follow the car
    void LateUpdate()
    {
        transform.position = _carPosition.position;
        if (Vector3.Distance(CamPosition.position, transform.position) > 3)
            Cam_rb.velocity = Cam_rb.transform.forward * Vector3.Distance(CamPosition.position, transform.position) * Time.deltaTime * 50;
        else 
            Cam_rb.velocity = Vector3.zero;
        CamPosition.position = new Vector3(CamPosition.position.x, Mathf.Clamp(CamPosition.position.y, 71, int.MaxValue), CamPosition.position.z);
        CamPosition.LookAt(transform.position);
    }


    /*Vector2 save_vit = new Vector2(vit_rot.x, vit_rot.z)  - new Vector2(_carPosition.position.x, _carPosition.position.z);
        save_vit.Normalize();
        transform.position = _carPosition.position;
        if (transform.rotation.y < _carPosition.rotation.y)
            transform.Rotate(new Vector3(0, save_vit.x, 0));
        if (transform.rotation.y > _carPosition.rotation.y)
            transform.Rotate(new Vector3(0, -save_vit.x, 0));
        vit_rot = _carPosition.position;
        // transform.rotation == new Quaternion(transform.rotation.x, _carPosition.rotation.y, transform.rotation.z, transform.rotation.w)*/
}
