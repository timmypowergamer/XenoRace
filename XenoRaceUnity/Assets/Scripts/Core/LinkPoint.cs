using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkPoint : MonoBehaviour {

    public string ID;

    [SerializeField]
    private string _mappedButton;

    public Appendage AttachedItem
    {
        get
        {
            return _attachedItem;
        }
    }
    private Appendage _attachedItem;

    public Vector3 LookRotation;

    private bool _isDown;
    private float _lastActivated;

    private void Update()
    {

        if (!Core.Instance.PlayerInputEnabled) return;
        if (!string.IsNullOrEmpty(_mappedButton))
        {
            if (!_isDown)
            {
                if (Input.GetButtonDown(_mappedButton))
                {
                    _isDown = true;
                    if(_attachedItem != null)
                    {
                        if (Time.realtimeSinceStartup - _lastActivated < _attachedItem.ReTriggerDelay) return;
                        _attachedItem.OnActivateStart();
                    }
                }
            }
            else
            {
                if(Input.GetButton(_mappedButton))
                {
                    if (_attachedItem != null)
                    {
                        _attachedItem.OnActivateHeld();
                    }
                }
                else if(Input.GetButtonUp(_mappedButton))
                {
                    if(_attachedItem != null)
                    {
                        _attachedItem.OnActivateEnd();
                        _isDown = false;
                    }
                }
            }
        }
    }

    public void SetAttachedItem(Appendage attachment)
    {
        if(_attachedItem != null)
        {
            Destroy(_attachedItem.gameObject);
        }

        _attachedItem = attachment;
    }
}
