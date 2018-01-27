using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public List<Checkpoint> checkpoints = new List<Checkpoint>();
    public int TotalCheckpoints
    {
        get { return checkpoints.Count; }
    }//total number of checkpoints in the track
    public int CurrentCheckpoint = 0;//the last checkpoint that was passed

    public void CheckpointReached(Checkpoint point)
    {
        //react to the checkpoint that was reached:
        int newPointNumber = checkpoints.IndexOf(point) + 1;
        if (newPointNumber > CurrentCheckpoint)//if this new checkpoint is further down the track than the last one
            CurrentCheckpoint = newPointNumber;
    }

	// Use this for initialization
	void Start ()
    {
		foreach (Checkpoint point in checkpoints)
        {
            point.SetOwner(this);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
