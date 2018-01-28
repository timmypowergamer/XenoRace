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
        PartsList = Core.Instance.GetPartsData();
        SceneManager.LoadScene(_raceSceneName);
    }

    public void GoToBuildFromRaceScene()
    {
        SceneManager.LoadScene(_buildSceneName);
    }

    public void GoToBuildFromMenu()
    {
        SceneManager.LoadScene(_buildSceneName);
    }

    public void RestartRaceScene()
    {
        SceneManager.LoadScene(_raceSceneName);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    
}
