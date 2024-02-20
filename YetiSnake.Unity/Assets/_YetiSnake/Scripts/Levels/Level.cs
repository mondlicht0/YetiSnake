using UnityEngine;
using UnityEngine.UI;

namespace YetiSnake.Levels
{
    public class Level : MonoBehaviour
    {
        private Button _button;
        [field: SerializeField] public int SceneIndex { get; private set; }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(() => LevelManager.Instance.LoadLevel(this));
        }
    }
}
