using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flapper : Appendage {
    public float ImpulseSpeed = 0f;

    public Transform ForceOrigin;
    public float ForceHitRadius = 0.2f;

    private void Start()
    {
        if(ForceOrigin == null)
        {
            ForceOrigin = transform;
        }
    }

    public override void OnActivateStart()
    {
        base.OnActivateStart();
        RaycastHit hitInfo;
        if(Physics.SphereCast(ForceOrigin.position, ForceHitRadius, Vector3.zero, out hitInfo, 0, LayerMask.NameToLayer("Default")))
        {
            _coreRB.AddForce(-ForceOrigin.up * ImpulseSpeed);
        }
    }
}
