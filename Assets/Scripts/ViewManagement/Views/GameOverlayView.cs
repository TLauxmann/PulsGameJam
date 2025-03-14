using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverlayView : View
{
    [SerializeField] private Button _pauseGameButton;
    public override void Initialize()
    {
        _pauseGameButton.onClick.AddListener(() => ViewManager.Show<PauseMenuView>());
    }
    private void Update()
    {
        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            ViewManager.Show<PauseMenuView>();
        }
    }

}
