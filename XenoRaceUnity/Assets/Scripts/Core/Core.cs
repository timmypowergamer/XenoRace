using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {

    [SerializeField]
    private Rigidbody _rigidbody;

    private Quaternion _targetRotation;
    private Quaternion _startRotation;
    private float delta = 0f;

    private static Core _instance;
    public static Core Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = GameObject.Find("Core");
                if (obj != null)
                {
                    _instance = obj.GetComponent<Core>();
                }
            }
            return _instance;
        }
    }

    [System.Serializable]
    public struct PartsData
    {
        public string SlotName;
        public string PartID;
    }

    public bool PlayerInputEnabled = false;
    private string _tempid;

    private Dictionary<string, LinkPoint> _linkPoints = new Dictionary<string, LinkPoint>();

    private void Awake()
    {
        _instance = this;

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

        if(LevelManager.Instance != null && LevelManager.Instance.PartsList != null)
        {
            SetPartsData(LevelManager.Instance.PartsList);
        }
    }

    public void AttachAppendage(Appendage attachment, string linkPointID)
    {
        if (!_linkPoints.ContainsKey(linkPointID))
        {
            Debug.LogError($"'{linkPointID}' is not a valid attachment point!");
            return;
        }
        if(attachment == null)
        {
            RemoveAppendage(linkPointID);
            return;
        }
        if (_linkPoints[linkPointID].AttachedItem != null)
        {
            RemoveAppendage(linkPointID);
        }

        LinkPoint linkPoint = _linkPoints[linkPointID];

        attachment.transform.SetParent(linkPoint.transform, false);
        attachment.transform.localPosition = Vector3.zero;
        attachment.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
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

        if (_tempid == linkPointID) return;

        delta = 0;
        _startRotation = transform.rotation;
        //transform.rotation = Quaternion.identity;
        //transform.rotation = Quaternion.FromToRotation(_linkPoints[linkPointID].transform.position - transform.position, (Vector3.up * 50) - transform.position);
        _targetRotation = Quaternion.FromToRotation(_linkPoints[linkPointID].transform.position - transform.position, Vector3.up);
        _tempid = linkPointID;
        //_targetRotation = Quaternion.LookRotation(_linkPoints[linkPointID].LookRotation, Vector3.up);
    }


    public List<PartsData> GetPartsData()
    {
        List<PartsData> parts = new List<PartsData>();
        foreach(KeyValuePair<string, LinkPoint> kvp in _linkPoints)
        {
            string partID = "";
            if (kvp.Value.AttachedItem != null) partID = kvp.Value.AttachedItem.ID;
            parts.Add(new PartsData() { SlotName = kvp.Key, PartID = partID });
        }
        return parts;
    }

    public void SetPartsData(List<PartsData> parts)
    {
        for (int i = 0; i < parts.Count; i++)
        {
            Appendage newPart = null;
            if (!string.IsNullOrEmpty(parts[i].PartID))
            {
                newPart = Instantiate(PartsManager.Instance.GetPartPrefab(parts[i].PartID));
            }
            AttachAppendage(newPart, parts[i].SlotName);
        }
    }

    private void Update()
    {
        if (PlayerInputEnabled) return;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, 25f);
        if (delta < 1)
        {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(_startRotation.eulerAngles, _targetRotation.eulerAngles, delta));
            delta = Mathf.Clamp(delta + Time.deltaTime * 4f, 0, 1f);
        }
        if (delta == 1)
        {
            transform.rotation = Quaternion.identity;
            transform.rotation = Quaternion.FromToRotation(_linkPoints[_tempid].transform.position - transform.position, (Vector3.up * 50) - transform.position);
        }
    }

    /// <summary>
    /// Enable or disable the player's ability to provide input. 
    /// </summary>
    /// <param name="enable">If true, we should allow the player to give the core input. Otherwise, we should not.</param>
    public void EnablePlayerInput(bool enable)
    {
        // Concern: Global Side Effects
        this.PlayerInputEnabled = enable; 
    }
}
