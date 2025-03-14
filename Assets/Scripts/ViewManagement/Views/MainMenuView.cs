using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;
    public override void Initialize()
    {
        _startGameButton.onClick.AddListener(() => {

            //TODO: Load an additional game scene here

            ViewManager.Show<GameOverlayView>();
        });
        _settingsButton.onClick.AddListener(() => ViewManager.Show<SettingsMenuView>());
        _creditsButton.onClick.AddListener(() => ViewManager.Show<CreditsMenuView>());
        _quitButton.onClick.AddListener(() => ViewManager.Show<ConfirmExitMenuView>());

    }
}
