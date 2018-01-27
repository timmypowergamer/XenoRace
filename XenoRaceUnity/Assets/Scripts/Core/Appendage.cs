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

    public virtual void OnActivateStart()
    {
        //Debug.Log($"{_id} OnActivateStart()");
    }

    public virtual void OnActivateHeld()
    {
        //Debug.Log($"{_id} OnActivateHeld()");
    }

    public virtual void OnActivateEnd()
    {
        //Debug.Log($"{_id} OnActivateEnd()");
    }

    public virtual void OnAttached(Rigidbody coreRB)
    {
        _coreRB = coreRB;
    }
}
