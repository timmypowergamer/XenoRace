using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appendage : MonoBehaviour
{

    public enum AttachmentState
    {
        STORED,
        FLOATING,
        ATTACHED
    }

    public AttachmentState AttachState { get; private set; }

    protected Rigidbody _coreRB;

    [SerializeField]
    private string _id;
    public string ID { get { return _id; } }

    public Sprite Icon;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private string _activeBool;

    [SerializeField]
    private string _startTrigger;
    [SerializeField]
    private string _endTrigger;

    private void Awake()
    {
        if(_animator == null) _animator = GetComponentInChildren<Animator>();
    }

    public virtual void OnActivateStart()
    {
        if (!string.IsNullOrEmpty(_activeBool)) _animator.SetBool(_activeBool, true);
        if (!string.IsNullOrEmpty(_startTrigger)) _animator.SetTrigger(_startTrigger);
    }

    public virtual void OnActivateHeld()
    {
        //Debug.Log($"{_id} OnActivateHeld()");
    }

    public virtual void OnActivateEnd()
    {
        if (!string.IsNullOrEmpty(_activeBool)) _animator.SetBool(_activeBool, false);
        if (!string.IsNullOrEmpty(_startTrigger)) _animator.SetTrigger(_endTrigger);
    }

    public virtual void OnAttached(Rigidbody coreRB)
    {
        _coreRB = coreRB;
    }
}
