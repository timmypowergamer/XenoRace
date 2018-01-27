using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : Appendage {

    [SerializeField]
    private ParticleSystem _particles;


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
        _coreRB.AddForceAtPosition(transform.forward * -10f, transform.position, ForceMode.Force);
    }


}
