using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBuildSlot : Button {

    private UIBuildPartIcon _selectedPart;
    public UIBuildPartIcon SelectedPart { get { return _selectedPart; } }

    [SerializeField]
    private string _linkPointID;

    [SerializeField]
    private UIBuildMode _parent;

    protected override void Reset()
    {
        _parent = GetComponentInParent<UIBuildMode>();
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        _parent.SelectSlot(this);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if(_selectedPart != null)
        {
            _selectedPart.OnSelect(null);
        }
        Core.Instance.ShowLinkPoint(_linkPointID);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        if(_selectedPart != null)
        {
            _selectedPart.OnDeselect(null);
        }
    }

    public void SetSelectedPart(UIBuildPartIcon part)
    {
        _selectedPart = part;
        Core.Instance.AttachAppendage(PartsManager.Instance.GetPartPrefab(part.PartID), _linkPointID);
        _parent.SelectSlot(null);
    }
}
