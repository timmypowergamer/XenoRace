using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {

    [SerializeField]
    private LinkPoint[] _attachmentPoints;
    [SerializeField]
    private Rigidbody _rigidbody;

    private Dictionary<LinkPoint, Appendage> _attachedObjects = new Dictionary<LinkPoint, Appendage>();

    private void Awake()
    {
        for(int i = 0; i < _attachmentPoints.Length; i++)
        {
            _attachedObjects.Add(_attachmentPoints[i], null);
            Appendage attached = _attachmentPoints[i].GetComponentInChildren<Appendage>();
            if(attached != null)
            {
                AttachAppendage(attached, _attachmentPoints[i]);
            }
        }
    }

    public void AttachAppendage(Appendage attachment, LinkPoint linkPoint)
    {
        if (!_attachedObjects.ContainsKey(linkPoint))
        {
            Debug.LogError($"'{linkPoint.name}' is not a valid attachment point!");
            return;
        }
        if (_attachedObjects[linkPoint] != null)
        {
            Debug.LogError($"'{linkPoint.name}' already has an attachment!");
            return;
        }

        attachment.transform.SetParent(linkPoint.transform, false);
        attachment.transform.localPosition = Vector3.zero;
        attachment.transform.localRotation = Quaternion.identity;
        _attachedObjects[linkPoint] = attachment;
        linkPoint.SetAttachedItem(attachment);
        attachment.OnAttached(_rigidbody);
    }

    public void RemoveAppendage(LinkPoint linkPoint)
    {
        if (!_attachedObjects.ContainsKey(linkPoint))
        {
            Debug.LogError($"'{linkPoint.name}' is not a valid attachment point!");
            return;
        }
        if (_attachedObjects[linkPoint] == null)
        {
            Debug.LogError($"'{linkPoint.name}' does not have an attachment to remove!");
            return;
        }

        linkPoint.SetAttachedItem(null);
    }

}
