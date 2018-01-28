using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Slap this on a GO that has an Animator to recieve AnimationEvents for common animation related tasks (Audio, FX, etc.)
/// </summary>
public class UIAnimationEvents : MonoBehaviour {

	[System.Serializable]
	public class ForwardedEvent
	{
		public string EventID;
		public UnityEvent Event;
	}
	public List<ForwardedEvent> ForwardedEvents = new List<ForwardedEvent>();

	public void InvokeEvent(string eventID)
	{
		for(int i = 0; i < ForwardedEvents.Count; i++)
		{
			if(ForwardedEvents[i].EventID == eventID)
			{
				ForwardedEvents[i].Event.Invoke();
			}
		}
	}

    public void DestroySelf(){
        GameObject.Destroy(gameObject);
    }
}
