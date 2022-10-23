using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _carPosition;
    [SerializeField] private Transform CamPosition;
    [SerializeField] private Rigidbody Cam_rb;
    private Vector3 vit_rot = Vector3.zero;
    [SerializeField] private float hauteurMaxCam = 10;

    //cam follow the car
    void LateUpdate()
    {
        transform.position = _carPosition.position;
        if (Vector3.Distance(CamPosition.position, transform.position) > 3)
            Cam_rb.velocity = Cam_rb.transform.forward * Vector3.Distance(CamPosition.position, transform.position) * Time.deltaTime * 250;
        else 
            Cam_rb.velocity = Vector3.zero;
        CamPosition.position = new Vector3(CamPosition.position.x, Mathf.Clamp(CamPosition.position.y, hauteurMaxCam, int.MaxValue), CamPosition.position.z);
        CamPosition.LookAt(transform.position);
    }
}
