using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YetiSnake.PlayerObject;

namespace YetiSnake
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] private Canvas _mainMenuCanvas;
        [SerializeField] private Canvas _levelsCanvas;
        [SerializeField] private Canvas _leaderboardCanvas;
        [SerializeField] private Canvas _settingsCanvas;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        private void Start()
        {
            _highScoreText.text = $"HIGH SCORE - {PlayerPrefs.GetInt(PlayerPrefsContainer.HighScoreKey)}";
        }

        public void OnLevelsButtonClick()
        {
            _mainMenuCanvas.gameObject.SetActive(false);
            _levelsCanvas.gameObject.SetActive(true);
        }

        public void OnChallengeButtonClick()
        {
            SceneManager.LoadScene(2);
        }

/*        public void OnLeaderboardButtonClick()
        {
            _mainMenuCanvas.gameObject.SetActive(false);
            _levelsCanvas.gameObject.SetActive(true);
        }*/

        public void OnSettingsButtonClick()
        {
            _mainMenuCanvas.gameObject.SetActive(false);
            _settingsCanvas.gameObject.SetActive(true);
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
