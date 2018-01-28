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
        if(Physics.Raycast(ForceOrigin.position, ForceOrigin.up, ForceHitRadius))
        {
            _coreRB.AddForce(-ForceOrigin.up * ImpulseSpeed);
        }
    }
}
