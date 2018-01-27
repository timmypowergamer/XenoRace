using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildMode : UIPanel
{
    [SerializeField]
    private List<UIBuildSlot> _slots;

    [SerializeField]
    private List<UIBuildPartIcon> _parts;

    public void SelectSlot(UIBuildSlot slot)
    {
        if (slot.SelectedPart == null)
        {
            slot.SelectedPart.SetHighlight();
        }
    }
}
