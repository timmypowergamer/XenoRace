using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMessages : MonoBehaviour
{
    [Header("Send 'Starting' or 'Stopping' messages.")]
    public AudioSource clip;

	void Starting ()
    {
        if (!clip.isPlaying)
        {
            clip.pitch = Random.Range(0.5f, 2f);
            clip.Play();
        }
	}
	
	void Stopping ()
    {
        clip.Stop();
	}
}
