using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public List<Checkpoint> Checkpoints = new List<Checkpoint>();
    public int TotalCheckpoints
    {
        get { return Checkpoints.Count; }
    }//total number of checkpoints in the track
    public int CurrentCheckpoint = 0;//the last checkpoint that was passed

    public void CheckpointReached(Checkpoint point)
    {
        //react to the checkpoint that was reached:
        int newPointNumber = Checkpoints.IndexOf(point) + 1;
        if (newPointNumber > CurrentCheckpoint)//if this new checkpoint is further down the track than the last one
            CurrentCheckpoint = newPointNumber;
    }

	// Use this for initialization
	void Start ()
    {
        if (Checkpoints.Count == 0)
            Debug.LogError("NO CHECKPOINTS!?!?!?!");
		foreach (Checkpoint point in Checkpoints)
        {
            point.SetOwner(this);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
