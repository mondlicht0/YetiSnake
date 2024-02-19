using UnityEngine;
using YetiSnake.PlayerObject;
using YetiSnake.MapDraw;

namespace YetiSnake
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private MapDrawer _mapDrawer;

        [Header("Player Prefab")]
        [SerializeField] private Player _player;

        private void Start()
        {
            Player player = Instantiate(_player);
            player.InitPlayer(_mapDrawer.GetNode(3, 3).WordPosition);
        }
    }
}
