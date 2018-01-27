using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class AnimationUtil
{
	public static IEnumerator WaitForAnim(Animator animator, UnityAction finishedCallback, string state = "")
	{
		if (string.IsNullOrEmpty(state))
		{
			//If no state is defined, we assume the next state is the end state. Wait for the current state to exit.
			//(Can't use GetNextAnimatorStateInfo() if the transition duration is 0, which it often is for UI transitions)
			int currentState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
			yield return null;
			while (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == currentState)
			{
				yield return null;
			}
			yield return new WaitUntilAnimatorStateCompletes(animator, animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
		}
		else
		{
			//just wait for the specified state
			yield return new WaitUntilAnimatorStateCompletes(animator, state);
		}

		if (finishedCallback != null)
		{
			finishedCallback();
		}
	}

	public static float UnboundedLerp(float a, float b, float t)
	{
		return t * b + (1 - t) * a;
	}

	public static IEnumerator DelayedAction(float delay, UnityAction action)
	{
		yield return new WaitForSeconds(delay);
		action.Invoke();
	}

	public static IEnumerator DelayedAction<T>(float delay, UnityAction<T> action, object value)
	{
		yield return new WaitForSeconds(delay);
		action.Invoke((T)value);
	}
}
