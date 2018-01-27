using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBuildPartIcon : Button {

    private UIBuildMode _parentPanel;
    private string _partID;
    public string PartID { get { return _partID; } }

    public void Init(string partID, UIBuildMode parent)
    {
        _parentPanel = parent;
        _partID = partID;
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
