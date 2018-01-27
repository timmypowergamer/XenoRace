using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointSystem Owner;
    private bool Reached = false;
    public bool HasBeenReached
    {
        get { return Reached; }
    }

    public void SetOwner(CheckpointSystem systemObj)
    {
        Owner = systemObj;
    }
	
    void OnTriggerEnter(Collider other)
    {
        //if this checkpoint hasn't been triggered yet and it is the player that hit it
        if (!Reached && other.CompareTag("Player"))
        {
            Reached = true;
            Owner.CheckpointReached(this);
        }
    }
}
