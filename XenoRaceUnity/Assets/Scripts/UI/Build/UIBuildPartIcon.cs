using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBuildPartIcon : Button {

    private UIBuildMode _parentPanel;
    private string _partID;
    public string PartID { get { return _partID; } }
    [SerializeField]
    private Image _icon;

    public void Init(string partID, UIBuildMode parent)
    {
        _parentPanel = parent;
        _partID = partID;
        _icon.sprite = PartsManager.Instance.GetPartPrefab(partID).Icon;
        _icon.preserveAspect = true;
    }

    public void SetHighlight()
    {
        Select();
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        _parentPanel.SelectPart(this);
    }

    
}
