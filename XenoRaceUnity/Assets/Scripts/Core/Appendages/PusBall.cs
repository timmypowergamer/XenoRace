using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusBall : Appendage {
    public float force = 1f;
    public float radius = 0.25f;

	public void FinishedBlowingUp()
    {
        _coreRB.AddExplosionForce(force, transform.position, radius, 1f, ForceMode.Impulse);
    }
}
