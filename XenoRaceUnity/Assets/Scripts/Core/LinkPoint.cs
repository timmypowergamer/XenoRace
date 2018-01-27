using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkPoint : MonoBehaviour {

    [SerializeField]
    private string _mappedButton;

    private Appendage _attachedItem;

    private bool _isDown;

    private void Update()
    {
        if (!string.IsNullOrEmpty(_mappedButton))
        {
            if (!_isDown)
            {
                if (Input.GetButtonDown(_mappedButton))
                {
                    _isDown = true;
                    if(_attachedItem != null)
                    {
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
        _attachedItem = attachment;
    }
}
