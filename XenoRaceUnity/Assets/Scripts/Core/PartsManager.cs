using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsManager : MonoBehaviour {

    private static PartsManager _instance;

    public static PartsManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private List<Appendage> _partList;

    private Dictionary<string, Appendage> _partsDict = new Dictionary<string, Appendage>();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        _instance = this;
        for(int i = 0; i < _partList.Count; i++)
        {
            _partsDict.Add(_partList[i].ID, _partList[i]);
        }
    }

    public Appendage GetPartPrefab(string partID)
    {
        if(_partsDict.ContainsKey(partID))
        {
            return _partsDict[partID];
        }

        Debug.LogError($"Part '{partID}' is not defined in PartsManager!");
        return null;
    }

    public List<string> GetPartsList()
    {
        return new List<string>(_partsDict.Keys);
    }
}
