using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuView : View
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Camera _camera;

    public override void Initialize()
    {
        _continueButton.onClick.AddListener(() =>
        {
            Unpause();
        });
        _settingsButton.onClick.AddListener(() => ViewManager.Show<SettingsMenuView>());
        _mainMenuButton.onClick.AddListener(() =>
        {
            //TODO: Unload your game scene
            SceneManager.UnloadSceneAsync(1);
            _camera.gameObject.SetActive(true);

            Time.timeScale = 1;
            ViewManager.Show<MainMenuView>();
        });
        _quitButton.onClick.AddListener(() => ViewManager.Show<ConfirmExitMenuView>());
    }


    private void Update()
    {
        //Unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Unpause();
        }
    }
    private static void Unpause()
    {
        Time.timeScale = 1;
        ViewManager.Show<GameOverlayView>();
    }

}
