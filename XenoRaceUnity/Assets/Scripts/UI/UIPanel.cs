using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class UIPanel : MonoBehaviour {

    [SerializeField]
    private UIPanelID _id;
    [SerializeField]
    private bool _startsActive = false;

    [SerializeField]
    private Animator _animator;
	public Animator Animator { get { return _animator; } }

	protected bool _isInitialized;

    /// <summary>
    /// Called when the panel is registered with the CanvasManager
    /// </summary>
    public virtual void OnRegistered()
    {
        if(_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        gameObject.SetActive(_startsActive);
    }

	/// <summary>
	/// Called when the panel is unregistered from the CanvasManager. For additional clean-up.
	/// </summary>
	public virtual void OnUnregistered()
	{

	}

	protected void Start()
    {
        if (_id != UIPanelID.NONE && !CanvasManager.instance.IsRegistered(this))
        {
            CanvasManager.instance.Register(this);
        }

		if (!_isInitialized)
		{
			Init();
		}
    }

    /// <summary>
    /// Initializes the panel. Use for populating children/layouts, etc.
    /// </summary>
    protected virtual void Init()
    {
		_isInitialized = true;
    }

    /// <summary>
    /// Opens the UIPanel (Sets it active). Optionally sets an animator trigger for transtions
    /// </summary>
    public virtual void Open(string transitionTrigger = "")
    {
        gameObject.SetActive(true);
		if (!_isInitialized) Init();
        SetAnimationTrigger(transitionTrigger, onOpenComplete);
    }

	protected virtual void onOpenComplete()
	{

	}


    /// <summary>
    /// Closes the UIPanel (sets it inactive).
    /// </summary>
    /// <param name="transitionTrigger">(optional) Sets an animator trigger for transitions and waits for the animation to finish before closing.</param>
    public virtual void Close(string transitionTrigger = "")
    {
		if (!gameObject.activeSelf) return;
        if (!SetAnimationTrigger(transitionTrigger, Close))
        {
            Close();
        }
    }

    /// <summary>
    /// Instantly close the UIPanel (sets it inactive)
    /// </summary>
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Set a trigger on the animator. Usually used for window transitions.
    /// </summary>
    /// <param name="trigger">Name of the Animator Trigger to set</param>
    /// <param name="callback">(optional) callback to receive when the animaton has finished</param>
    /// <returns></returns>
    public bool SetAnimationTrigger(string trigger, UnityAction callback = null, string completeState = "", Animator animator = null)
    {
		if(animator == null)
		{
			animator = _animator;
		}

        if (animator != null && !string.IsNullOrEmpty(trigger))
        {
			animator.SetTrigger(trigger);
            if(callback != null)
            {
                StartCoroutine(AnimationUtil.WaitForAnim(animator, callback, completeState));
            }
            return true;
        }
        return false;
    }

    public UIPanelID PanelID
    {
        get
        {
            return _id;
        }
    }
}
