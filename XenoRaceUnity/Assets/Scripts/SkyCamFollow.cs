using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCamFollow : MonoBehaviour {

    public Transform _transToFollow;

    private Vector3 _lastPos;

    public float ParallaxFactor = 0.005f;

    private void Start()
    {
        _lastPos = _transToFollow.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = _transToFollow.position - _lastPos;

        transform.position += delta * ParallaxFactor;

        _lastPos = _transToFollow.position;
    }
}
