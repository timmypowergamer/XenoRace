using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamewraOrbit : MonoBehaviour {

    public Transform _cam;
    public Transform _follow;

    public float Speed = 1f;

    private void LateUpdate()
    {
        transform.position = _follow.position;
        _cam.LookAt(transform);

        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Speed, Space.World);
        transform.Rotate(Vector3.left, Input.GetAxis("Vertical") * Speed, Space.World);
    }
}
