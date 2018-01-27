using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {

    [SerializeField]
    private Transform[] _attachmentPoints;
    [SerializeField]
    private Rigidbody _rigidbody;

    private Dictionary<Transform, Appendage> _attachedObjects = new Dictionary<Transform, Appendage>();

    private void Awake()
    {
        for(int i = 0; i < _attachmentPoints.Length; i++)
        {
            _attachedObjects.Add(_attachmentPoints[i], null);
        }
    }

    public void AttachAppendage(Appendage attachment, Transform linkPoint)
    {
        if (!_attachedObjects.ContainsKey(linkPoint)) Debug.LogError($"'{linkPoint.name}' is not a valid attachment point!");

        attachment.transform.SetParent(linkPoint, false);
        attachment.transform.localRotation = Quaternion.identity;

        FixedJoint joint = attachment.gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = _rigidbody;

       
    }

}
