using UnityEngine;
using YetiSnake.PlayerObject;
using YetiSnake.MapDraw;

namespace YetiSnake
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {  get; private set; }

        [field: SerializeField] public MapDrawer MapDrawer { get; private set; }

        [Header("Player Prefab")]
        [SerializeField] private Player _player;

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

        private void Start()
        {
            Player player = Instantiate(_player);
            player.InitPlayer(MapDrawer.GetNode(3, 3));
        }
    }
}
