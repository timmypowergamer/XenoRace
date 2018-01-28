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
        List<Core.PartsData> partsList = Core.Instance.GetPartsData();
        Debug.Log("Going to race!");
        SceneManager.LoadScene(_raceSceneName);
        Core.Instance.SetPartsData(partsList);
    }

    public void GoToBuildFromRaceScene()
    {
        List<Core.PartsData> partsList = Core.Instance.GetPartsData();
        SceneManager.LoadScene(_buildSceneName);
        Core.Instance.SetPartsData(partsList);
    }

    public void GoToBuildFromMenu()
    {
        SceneManager.LoadScene(_buildSceneName);
    }

    public void RestartRaceScene()
    {
        List<Core.PartsData> partsList = Core.Instance.GetPartsData();
        SceneManager.LoadScene(_raceSceneName);
        Core.Instance.SetPartsData(partsList);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }
}
