using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private string _raceSceneName;

    [SerializeField]
    private string _buildSceneName;

    [SerializeField]
    private string _mainMenuSceneName;

    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }

    public List<Core.PartsData> PartsList = null;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        _instance = this;
    }

    public void GoToRaceFromBuildScene()
    {
        CanvasManager.instance.DeactivateMode(CanvasStates.Play);
        PartsList = Core.Instance.GetPartsData();
        SceneManager.LoadScene(_raceSceneName);
        CanvasManager.instance.ActivateMode(CanvasStates.Play);
    }

    public void GoToBuildFromRaceScene()
    {
        CanvasManager.instance.DeactivateMode(CanvasStates.Build);
        SceneManager.LoadScene(_buildSceneName);
        CanvasManager.instance.ActivateMode(CanvasStates.Build);
    }

    public void GoToBuildFromMenu()
    {
        CanvasManager.instance.DeactivateMode(CanvasStates.Build);
        SceneManager.LoadScene(_buildSceneName);
        CanvasManager.instance.ActivateMode(CanvasStates.Build);
    }

    public void RestartRaceScene()
    {
        SceneManager.LoadScene(_raceSceneName);
    }

    public void GoToMainMenuFromRace()
    {
        CanvasManager.instance.DeactivateMode(CanvasStates.Menu);
        SceneManager.LoadScene(_mainMenuSceneName);
        CanvasManager.instance.ActivateMode(CanvasStates.Menu);
    }

    
}
