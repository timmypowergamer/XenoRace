using UnityEngine;

//An easy way to wait for the to Animator complete a transition inside a coroutine

public class WaitUntilAnimatorStateCompletes : CustomYieldInstruction
{
	private enum State {
		WaitingForChange,
		WaitingForAnimComplete,
		Complete
	}
	private State state;
	private int testState;
	private int layer;
	private Animator animator;

	public override bool keepWaiting {
		get {
			AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(layer);
			switch(state)
			{
				case State.WaitingForChange:
					if( animatorState.fullPathHash == testState ) {
						state = State.WaitingForAnimComplete;
					}
					break;

				case State.WaitingForAnimComplete:
					if( animatorState.loop || animatorState.fullPathHash != testState || animatorState.normalizedTime >= 1f ) {
						state = State.Complete;
					}
					break;

				case State.Complete:
					return false;
			}
			return true;
		}
	}
	
	public WaitUntilAnimatorStateCompletes( Animator animator, string testState )
	{
		this.animator = animator;
		this.testState = Animator.StringToHash(testState);
		this.layer = animator.GetLayerIndex(testState.Split('.')[0]);
		state = State.WaitingForChange;
	}

	public WaitUntilAnimatorStateCompletes( Animator animator, int testState, int layer = 0 )
	{
		this.layer = layer;
		this.animator = animator;
		this.testState = testState;
		state = State.WaitingForChange;
	}

}