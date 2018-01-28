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
        StartCoroutine(doSceneChange(_raceSceneName, CanvasStates.Play));
    }

    public void GoToBuildFromRaceScene()
    {
        CanvasManager.instance.DeactivateMode(CanvasStates.Build);
        StartCoroutine(doSceneChange(_buildSceneName, CanvasStates.Build));
    }

    public void GoToBuildFromMenu()
    {
        CanvasManager.instance.DeactivateMode(CanvasStates.Build);
        StartCoroutine(doSceneChange(_buildSceneName, CanvasStates.Build));
    }

    public void RestartRaceScene()
    {
        StartCoroutine(doSceneChange(_raceSceneName, CanvasStates.Play));
    }

    public void GoToMainMenuFromRace()
    {
        CanvasManager.instance.DeactivateMode(CanvasStates.Menu);
        StartCoroutine(doSceneChange(_mainMenuSceneName, CanvasStates.Menu));
    }

    private IEnumerator doSceneChange(string sceneName, CanvasStates nextMode)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
        CanvasManager.instance.ActivateMode(nextMode);
    }
    
}
