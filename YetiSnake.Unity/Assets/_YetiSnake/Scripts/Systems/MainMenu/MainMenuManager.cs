using UnityEngine;

namespace YetiSnake.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] private Canvas _mainMenuCanvas;
        [SerializeField] private Canvas _levelsCanvas;
        [SerializeField] private Canvas _leaderboardCanvas;
        [SerializeField] private Canvas _settingsCanvas;

        public void OnLevelsButtonClick()
        {
            _mainMenuCanvas.gameObject.SetActive(false);
            _levelsCanvas.gameObject.SetActive(true);
        }

        public void OnChallengeButtonClick()
        {
            // load new scene
        }

        public void OnLeaderboardButtonClick()
        {
            _mainMenuCanvas.gameObject.SetActive(false);
            _levelsCanvas.gameObject.SetActive(true);
        }

        public void OnSettingsButtonClick()
        {
            _mainMenuCanvas.gameObject.SetActive(false);
            _levelsCanvas.gameObject.SetActive(true);
        }

        public void OnPrivacyPolicyButtonClick()
        {

        }

        public void OnBackButtonClick(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
            _mainMenuCanvas.gameObject.SetActive(true);
        }
    }
}
