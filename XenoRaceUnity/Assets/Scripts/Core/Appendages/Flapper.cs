using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flapper : Appendage {
    public float ImpulseSpeed = 0f;

    public override void OnActivateStart()
    {
        base.OnActivateStart();
        _coreRB.AddForce(-transform.up * ImpulseSpeed);
    }
}
