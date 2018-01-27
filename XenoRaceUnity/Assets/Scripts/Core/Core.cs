using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {

    [SerializeField]
    private Rigidbody _rigidbody;

    public static Core Instance;

    public bool PlayerInputEnabled = false;

    private Dictionary<string, LinkPoint> _linkPoints = new Dictionary<string, LinkPoint>();

    private void Awake()
    {
        Instance = this;

        LinkPoint[] points = GetComponentsInChildren<LinkPoint>();
        for(int i = 0; i < points.Length; i++)
        {
            _linkPoints.Add(points[i].ID, points[i]);
            Appendage attached = points[i].GetComponentInChildren<Appendage>();
            if(attached != null)
            {
                AttachAppendage(attached, points[i].ID);
            }
        }
    }

    public void AttachAppendage(Appendage attachment, string linkPointID)
    {
        if (!_linkPoints.ContainsKey(linkPointID))
        {
            Debug.LogError($"'{linkPointID}' is not a valid attachment point!");
            return;
        }
        if (_linkPoints[linkPointID].AttachedItem != null)
        {
            Debug.LogError($"'{linkPointID}' already has an attachment!");
            return;
        }

        LinkPoint linkPoint = _linkPoints[linkPointID];

        attachment.transform.SetParent(linkPoint.transform, false);
        attachment.transform.localPosition = Vector3.zero;
        attachment.transform.localRotation = Quaternion.identity;
        linkPoint.SetAttachedItem(attachment);
        attachment.OnAttached(_rigidbody);
    }

    public void RemoveAppendage(string linkPointID)
    {
        if (!_linkPoints.ContainsKey(linkPointID))
        {
            Debug.LogError($"'{linkPointID}' is not a valid attachment point!");
            return;
        }
        if (_linkPoints[linkPointID].AttachedItem == null)
        {
            Debug.LogError($"'{linkPointID}' does not have an attachment to remove!");
            return;
        }

        _linkPoints[linkPointID].SetAttachedItem(null);
    }


    public void ShowLinkPoint(string linkPointID)
    {
        if (!_linkPoints.ContainsKey(linkPointID))
        {
            Debug.LogError($"'{linkPointID}' is not a valid attachment point!");
            return;
        }

        transform.rotation = Quaternion.identity;
        transform.rotation = Quaternion.FromToRotation(_linkPoints[linkPointID].transform.position - transform.position, Camera.main.transform.position - transform.position);
    }
}
