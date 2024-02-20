using UnityEngine;
using YetiSnake.PlayerObject;
using YetiSnake.MapDraw;
using YetiSnake.Nodes;
using YetiSnake.Utilities;
using UnityEngine.SceneManagement;
using TMPro;

namespace YetiSnake
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {  get; private set; }
        public bool IsChallenge = false;
        public int Duration = 30;

        private int _yetiScore = 0;

        [field: SerializeField] public MapDrawer MapDrawer { get; private set; }

        [Header("Player Prefab")]
        [SerializeField] private Player _playerPrefab;
        private Player _playerObject;

        [SerializeField] private Node _yetiNode;
        [SerializeField] private GameObject _yetiObject;
        [SerializeField] private Sprite _yetiSprite;

        [Header("Canvases")]
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private Canvas _pauseCanvas;
        [SerializeField] private Canvas _settingsCanvas;
        [SerializeField] private TextMeshProUGUI _yetiScoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        [SerializeField] private Timer _timer;
        private AudioSource _audioSource;

        private bool _isPause;
        public int HighScore { get; private set; }
        public int Volume { get; private set; }

        public Node YetiNode { get => _yetiNode; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            else
            {
                Destroy(Instance);
                Instance = this;
            }

            UpdateScoreText();
        }

        private void Start()
        {
            StartNewGame();
        }

        public void GetToHome()
        {
            SceneManager.LoadScene(0);
        }

        public void ShowPauseMenu()
        {
            Pause();
            _pauseCanvas.gameObject.SetActive(true);
        }

        public void HideMenu(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
            if (_isPause == true)
            {
                Pause();
            }
        }

        private void Pause()
        {
            Time.timeScale = _isPause ? 1 : 0;
            _isPause = !_isPause;
        }

        public void ShowSettings()
        {
            Pause();
            _settingsCanvas.gameObject.SetActive(true);
        }

        public void GameOver()
        {
            SaveHighScore();
            _gameOverCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void UpdateScoreText()
        {
            _yetiScoreText.text = _yetiScore.ToString();
            if (IsChallenge)
            {
                if (_yetiScore > HighScore)
                {
                    _highScoreText.text = HighScore.ToString();
                }
            }
        }

        private void StartNewGame()
        {
            if (IsChallenge)
            {
                _timer.StartTimer(Duration, _isPause);

                _timer.OnEndTimer += GameOver;
            }

            LoadHighScore();
            _yetiScore = 0;
            UpdateScoreText();

            if (IsChallenge)
            {
                _highScoreText.text = HighScore.ToString();
            }

            _gameOverCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;

            ClearPreviousGame();
            MapDrawer.DrawMap();

            _playerObject = Instantiate(_playerPrefab);
            _playerObject.InitPlayer(MapDrawer.GetNode(3, 3));
            _playerObject.GetComponent<PlayerController>().OnYetiEated += AddYetiScore;

            CreateYeti();
        }

        private void AddYetiScore()
        {
            _yetiScore++;
            UpdateScoreText();
        }

        private void ClearPreviousGame()
        {
            _playerObject?.ClearTail();
            MapDrawer?.ClearMap();
            Destroy(_playerObject?.gameObject);
            Destroy(_yetiObject);
        }

        private void CreateYeti()
        {
            _yetiObject = new GameObject("Yeti");
            SpriteRenderer yetiRenderer = _yetiObject.AddComponent<SpriteRenderer>();
            yetiRenderer.sprite = Utils.CreateSprite(Color.red, _yetiSprite);
            yetiRenderer.sortingOrder = 2;

            RandomSpawnYeti();
        }

        public void RandomSpawnYeti()
        {
            int randPos = UnityEngine.Random.Range(0, MapDrawer.AvaliableNodes.Count);
            Node node = MapDrawer.AvaliableNodes[randPos];
            Utils.PlaceObject(_yetiObject, node.WordPosition); 
            //_yetiObject.transform.position = node.WordPosition;
            _yetiNode = node;
        }
        
        private void LoadHighScore()
        {
            HighScore = PlayerPrefs.GetInt(PlayerPrefsContainer.HighScoreKey, 0);
        }

        private void SaveHighScore()
        {
            if (_yetiScore > HighScore)
            {
                HighScore = _yetiScore;
                PlayerPrefs.SetInt(PlayerPrefsContainer.HighScoreKey, HighScore);
                PlayerPrefs.Save();
            }
        }
    }
}
