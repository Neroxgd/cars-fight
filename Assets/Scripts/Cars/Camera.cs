using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using DG.Tweening;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _PlayerPosition;
    [SerializeField] private Transform CamPosition;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Rigidbody rbCam;
    [SerializeField] private float hauteurMaxCam = 10;
    [SerializeField] private float distanceCam;
    [SerializeField] private float speedCam;
    [SerializeField] private Car_input _car_Input;
    private bool IsCarStopped { get { return _car_Input.ReturnSpeed < .1; } }

    //cam follow the car
    void LateUpdate()
    {
        if (!DOTween.IsTweening(rbCam))
        {
            transform.position = _PlayerPosition.position;
            if (Vector3.Distance(CamPosition.position, transform.position) > distanceCam)
                rbCam.velocity = rbCam.transform.forward * Mathf.Pow(Vector3.Distance(CamPosition.position, transform.position), 2) * Time.deltaTime * speedCam;

            else
                rbCam.velocity = Vector3.Lerp(rbCam.velocity, Vector3.zero, Time.deltaTime);
            CamPosition.position = new Vector3(CamPosition.position.x, Mathf.Clamp(CamPosition.position.y, hauteurMaxCam, int.MaxValue), CamPosition.position.z);
        }
        CamPosition.LookAt(transform.position);

        if (IsCarStopped && Vector3.Distance(CamPosition.position, _PlayerPosition.position) > 6 && boxCollider.enabled)
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

    public void CameraLookForward(InputAction.CallbackContext context)
    {
        if (!context.started && !IsCarStopped) return;
        rbCam.DOMove(new Vector3(_PlayerPosition.position.x - _PlayerPosition.forward.x * 6f, _PlayerPosition.position.y + hauteurMaxCam, _PlayerPosition.position.z - _PlayerPosition.forward.z * 6f), 0.5f);
    }
}
