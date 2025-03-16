using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : View
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Camera _camera;

    public override void Initialize()
    {
        _startGameButton.onClick.AddListener(() => {

            _camera.gameObject.SetActive(false);
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            ViewManager.Show<GameOverlayView>();
        });
        _settingsButton.onClick.AddListener(() => ViewManager.Show<SettingsMenuView>());
        _creditsButton.onClick.AddListener(() => ViewManager.Show<CreditsMenuView>());
        _quitButton.onClick.AddListener(() => ViewManager.Show<ConfirmExitMenuView>());

    }
}
