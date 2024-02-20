using UnityEngine;
using UnityEngine.SceneManagement;

namespace YetiSnake.Levels
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

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
        }

        public void LoadLevel(Level level)
        {
            SceneManager.LoadScene(level.SceneIndex);
        }
    }
}
