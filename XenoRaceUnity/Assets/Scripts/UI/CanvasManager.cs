using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CanvasType
{
	World,
	Screen,
	ImmortalScreen
}

public enum CanvasStates
{
    Menu,
    Build,
    Test,
    Play
}

public enum UIPanelID
{
    NONE = 0,
    HUD = 1,
    Build = 2, 
    GameOver = 3
}

[ExecuteInEditMode]
public class CanvasManager : MonoBehaviour
{
	[SerializeField]
	private bool _autoRegister = true;

    [SerializeField]
    private CanvasStates _initialState;

	private static CanvasManager _instance;
	public static CanvasManager instance
	{
		get
		{
			if(_instance == null)
			{				
				_instance = FindObjectOfType<CanvasManager>();
			}
			return _instance;
		}
	}
	public static GameObject screenCanvas { get; private set; }
	public static GameObject worldCanvas { get; private set; }
	public static GameObject immortalScreenCanvas { get; private set; }
    public static Camera CanvasCamera { get; private set; }

    [SerializeField]
    private GameObject _screenCanvas;
    [SerializeField]
    private GameObject _worldCanvas;
    [SerializeField]
    private GameObject _immortalScreenCanvas;

    [SerializeField]
    private RectTransform _containerPrefab;

	#region UIPanel Registration and lookup
	private Dictionary<UIPanelID, UIPanel> _registeredPanels = new Dictionary<UIPanelID, UIPanel>();

	public UnityEngine.Events.UnityAction<CanvasStates> OnStateActive;

    [Serializable]
    public class CanvasMode
    {
		[Tooltip("Used only to identify the mode in the inspector.")]
		public string ModeName;
        public List<CanvasStates> ActiveStates;
        public List<GameObject> Prefabs;
        public CanvasType canvasType;

        private RectTransform _spawnedCanvas;
        private CanvasManager _manager;

        public Transform Activate(CanvasManager manager)
        {
            //only bother if we aren't spawned. Otherwise we are already active.
            if(_spawnedCanvas == null)
            {
                _manager = manager;
                //generate a new container for the prefabs
                _spawnedCanvas = Instantiate(manager._containerPrefab);

				switch(canvasType)
				{
					case CanvasType.ImmortalScreen:
						_spawnedCanvas.transform.SetParent(_manager._immortalScreenCanvas.transform, false);
						break;
					case CanvasType.Screen:
						_spawnedCanvas.transform.SetParent(_manager._screenCanvas.transform, false);
						break;
					case CanvasType.World:
						_spawnedCanvas.transform.SetParent(_manager._worldCanvas.transform, false);
						break;
				}
                
                for (int i = 0; i < Prefabs.Count; i++)
                {
                    //spawn each prefab in the collection
                    GameObject element = Instantiate(Prefabs[i]);
                    element.transform.SetParent(_spawnedCanvas.transform, false);
                }
                _manager.RegisterAll(_spawnedCanvas.transform);
                return _spawnedCanvas;
            }
            return null;
        }

        public void Deactivate()
        {
            if(_spawnedCanvas != null)
            {
                _manager.RemoveAll(_spawnedCanvas.transform);
                Destroy(_spawnedCanvas.gameObject);
                _spawnedCanvas = null;
            }
        }
    }

    [SerializeField]
    private List<CanvasMode> _canvasModes;

    public void RegisterAll(Transform parent)
    {
        foreach (UIPanel panel in parent.GetComponentsInChildren<UIPanel>(true))
        {
            if (!IsRegistered(panel))
            {
                Register(panel);
            }
			else
			{
				Debug.LogError("Panel ID '" + panel.PanelID.ToString() + "' is already registered with UIManager. Please create a new ID first");
			}
        }
    }

    public void Register(UIPanel panel)
    {
        if (!_registeredPanels.ContainsKey(panel.PanelID))
        {
            _registeredPanels.Add(panel.PanelID, panel);
            panel.OnRegistered();
        }
        else
        {
            Debug.LogError("Panel ID '" + panel.PanelID.ToString() + "' is already registered with UIManager. Please create a new ID first");
        }
    }

    public void RemoveAll(Transform parent)
    {
        foreach (UIPanel panel in parent.GetComponentsInChildren<UIPanel>(true))
        {
            if (IsRegistered(panel))
            {
                Remove(panel);
            }
        }
    }

    public void Remove(UIPanel panel)
    {
        if (_registeredPanels.ContainsKey(panel.PanelID))
        {
			_registeredPanels.Remove(panel.PanelID);
			panel.OnUnregistered();
        }
        else
        {
            Debug.LogError("Panel ID '" + panel.PanelID.ToString() + "' is not registered with UIManager.");
        }
    }

    public bool IsRegistered(UIPanel panel)
    {
		return IsRegistered(panel.PanelID);
    }

	public bool IsRegistered(UIPanelID id)
	{
		return _registeredPanels.ContainsKey(id);
	}

    public T Get<T>(UIPanelID id) where T : UIPanel
    {
        if (_registeredPanels.ContainsKey(id))
        {
            if (_registeredPanels[id] is T)
            {
                return _registeredPanels[id] as T;
            }
            else
            {
                Debug.LogError("UIPanel mathcing ID '" + id.ToString() + "' is not of type '" + typeof(T).ToString() + "'.");
            }
        }
        else
        {
            Debug.LogError("No UIPanel with id '" + id.ToString() + "' registered with UIManager.");
        }
        return null;
    }
    #endregion



	void Awake(){
		if( _instance == null ) _instance = this;
		else throw new System.Exception( "Unexpected Duplicate Canvas Manager" );

        //World canvas is not a child of CanvasManager anymore. (In general I don't like relying on transform.find() because heirarchies and names can change and break things) -EK
        screenCanvas = _screenCanvas;
        worldCanvas = _worldCanvas;
        immortalScreenCanvas = _immortalScreenCanvas;
        CanvasCamera = GetComponent<Camera>();

		//auto register everything under the UI manager
		if(_autoRegister) RegisterAll(transform);
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            ActivateMode(_initialState);
        }
    }

    public void DeactivateMode(CanvasStates nextMode)
	{
		for (int i = 0; i < _canvasModes.Count; i++)
		{
			if (!_canvasModes[i].ActiveStates.Contains(nextMode))
			{
				_canvasModes[i].Deactivate();
			}
		}
	}

	public void ActivateMode(CanvasStates newMode)
	{
		for (int i = 0; i < _canvasModes.Count; i++)
		{
			if (_canvasModes[i].ActiveStates.Contains(newMode))
			{
				_canvasModes[i].Activate(this);
			}
		}

		if (OnStateActive != null)
        {
            OnStateActive.Invoke(newMode);
        }
	}

}
