using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _PlayerPosition;
    [SerializeField] private Transform CamPosition;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Rigidbody Cam_rb;
    private Vector3 vit_rot = Vector3.zero;
    [SerializeField] private float hauteurMaxCam = 10;
    [SerializeField] private float distanceCam;
    [SerializeField] private float speedCam;
    [SerializeField] private Car_input _car_Input;
    private float smoothCam = 1;

    //cam follow the car
    void LateUpdate()
    {
        transform.position = _PlayerPosition.position;
        if (Vector3.Distance(CamPosition.position, transform.position) > distanceCam)
        {
            smoothCam = 1;
            Cam_rb.velocity = Cam_rb.transform.forward * Mathf.Pow(Vector3.Distance(CamPosition.position, transform.position), 2) * Time.deltaTime * speedCam;
        }
        else
        {
            smoothCam -= 0.1f * Time.deltaTime;
            Cam_rb.velocity = Vector3.Lerp(Vector3.zero, Cam_rb.velocity, smoothCam);
        }
        smoothCam = Mathf.Clamp(smoothCam, 0, 1);
        CamPosition.position = new Vector3(CamPosition.position.x, Mathf.Clamp(CamPosition.position.y, hauteurMaxCam, int.MaxValue), CamPosition.position.z);
        CamPosition.LookAt(transform.position);

        if (_car_Input.returnSpeed() > .1 && Vector3.Distance(CamPosition.position, _PlayerPosition.position) > 6 && boxCollider.enabled)
        {
            boxCollider.enabled = !boxCollider.enabled;
            StartCoroutine(ReplaceCam());
        }


    }
    IEnumerator ReplaceCam()
    {
        yield return new WaitForSeconds(3);
        boxCollider.enabled = !boxCollider.enabled;
    }
}
