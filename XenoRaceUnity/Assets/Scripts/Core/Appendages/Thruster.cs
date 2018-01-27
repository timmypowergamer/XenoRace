using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : Appendage {

    [SerializeField]
    private ParticleSystem _particles;
    [SerializeField]
    private Transform _thrustPoint;


    public override void OnActivateStart()
    {
        _particles.Play();
    }

    public override void OnActivateEnd()
    {
        _particles.Stop();
    }

    public override void OnActivateHeld()
    {
        _coreRB.AddForceAtPosition(_thrustPoint.transform.forward * -10f, _thrustPoint.transform.position, ForceMode.Force);
    }


}
