using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIPanel {

    [SerializeField]
    private Button _startButton;

    protected override void Init()
    {
        base.Init();
        _startButton.Select();
    }

    public void PlayPressed()
    {
        LevelManager.Instance.GoToBuildFromMenu();
    }

    public void QuitPressed()
    {
        Application.Quit();
    }
}
