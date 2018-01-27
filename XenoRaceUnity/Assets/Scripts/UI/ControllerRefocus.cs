using UnityEngine;
using UnityEngine.EventSystems;

// If there is no selected item, set the selected item to the event system's first selected item
public class ControllerRefocus : MonoBehaviour
{
    private GameObject _lastSelected;
    void Update()
    {
        if(_lastSelected != null)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(_lastSelected);
            }
        }
        _lastSelected = EventSystem.current.currentSelectedGameObject;
    }
}