using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildMode : UIPanel
{
    [SerializeField]
    private List<UIBuildSlot> _slots;

    private List<UIBuildPartIcon> _parts = new List<UIBuildPartIcon>();

    [SerializeField]
    private CanvasGroup _slotsGroup;
    [SerializeField]
    private CanvasGroup _partsGroup;

    [SerializeField]
    private UIBuildPartIcon _iconPrefab;
    [SerializeField]
    private RectTransform _partsGrid;

    private bool _selectingPart = false;

    private int _selectedSlotIndex;

    protected override void Init()
    {
        base.Init();

        List<string> parts = PartsManager.Instance.GetPartsList();
        foreach(string part in parts)
        {
            UIBuildPartIcon icon = Instantiate(_iconPrefab, _partsGrid, false);
            icon.Init(part, this);
            _parts.Add(icon);
        }
        _slots[0].Select();
    }

    public void SelectSlot(UIBuildSlot slot)
    {
        if (slot != null)
        {
            _selectingPart = true;
            _slotsGroup.interactable = false;
            //_partsGroup.interactable = true;
            for(int i = 0; i < _slots.Count; i++)
            {
                if(_slots[i] == slot)
                {
                    _selectedSlotIndex = i;
                    break;
                }
            }

            if (slot.SelectedPart != null)
            {
                slot.SelectedPart.SetHighlight();
            }
            else
            {
                _parts[0].SetHighlight();
            }
        }
        else
        {
            _slotsGroup.interactable = true;
            _slots[_selectedSlotIndex].Select();
            _selectingPart = false;
            //_partsGroup.interactable = false;
        }
    }

    public void SelectPart(UIBuildPartIcon part)
    {
        _slots[_selectedSlotIndex].SetSelectedPart(part);
    }

    private void Update()
    {
        if(_selectingPart)
        {
            if(Input.GetButtonDown("Cancel"))
            {
                SelectSlot(null);
            }
        }
        if (Input.GetButtonDown("Start"))
        {
            LevelManager.Instance.GoToRaceFromBuildScene();
        }
    }
}
