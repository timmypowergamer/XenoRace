using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the player hits the death floor, we need to restart the game. 
/// </summary>
public class DeathFloor : MonoBehaviour
{
    /// <summary>
    /// If the player has entered our trigger, then we need to do our courtesy calls. This will either restart 
    /// the game immediately, or get the player some kind of "Game Over" card, taking them to the build menu. 
    /// </summary>
    /// <param name="other">The collider that has hit us.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.FellOffTrack(); 
        }
    }
}
