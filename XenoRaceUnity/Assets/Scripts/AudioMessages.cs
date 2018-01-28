using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMessages : MonoBehaviour
{
    [Header("Send 'Starting' or 'Stopping' messages.")]
    public AudioSource clip;

	void Starting () {
        if (!clip.isPlaying)
            clip.Play();
	}
	
	void Stopping () {
        clip.Stop();
	}
}
