using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Whenever the checkpoint is hit, it is it's duty to tell the checkpoint manager (should be game manager?) and turn off 
/// the visuals for it (right now just disable). 
/// 
/// NOTE: Not being used at the moment!!! 
/// </summary>
public class Checkpoint : MonoBehaviour
{
    /// <summary>
    /// The parent checkpoint manager. 
    /// </summary>
    private CheckpointSystem Owner;

    /// <summary>
    /// Has this checkpoint been reached? 
    /// </summary>
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
