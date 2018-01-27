using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointSystem Owner;
    public bool Reached = false;

    public void SetOwner(CheckpointSystem systemObj)
    {
        Owner = systemObj;
    }
	
    void OnTriggerEnter(Collider other)
    {
        //if this checkpoint hasn't been triggered yet and it is the player that hit it
        if (!Reached && other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint!");
            Reached = true;
            Owner.CheckpointReached(this);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}
