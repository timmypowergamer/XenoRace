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

    [SerializeField]
    private Image _partIcon;

#if UNITY_EDITOR
    protected override void Reset()
    {
        _parent = GetComponentInParent<UIBuildMode>();
    }
#endif

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        _parent.SelectSlot(this);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        //if(_selectedPart != null)
        //{
        //    _selectedPart.OnSelect(null);
        //}
        Core.Instance.ShowLinkPoint(_linkPointID);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        //if(_selectedPart != null)
        //{
        //    _selectedPart.OnDeselect(null);
        //}
    }

    public void SetSelectedPart(UIBuildPartIcon part)
    {
        if(part == _selectedPart)
        {
            part = null;
        }
        _selectedPart = part;
        if (_selectedPart != null)
        {
            Appendage newPart = Instantiate(PartsManager.Instance.GetPartPrefab(part.PartID));
            Core.Instance.AttachAppendage(newPart, _linkPointID);
            _partIcon.gameObject.SetActive(true);
            _partIcon.sprite = newPart.Icon;
        }
        else
        {
            Core.Instance.RemoveAppendage(_linkPointID);
            _partIcon.gameObject.SetActive(false);
        }
    }
}
