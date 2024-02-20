using System.Threading;
using UnityEngine;
using YetiSnake.Input;
using YetiSnake.MapDraw;
using YetiSnake.YetiNodes;

namespace YetiSnake.PlayerObject
{
    public class PlayerController : MonoBehaviour
    {
        private Player _player;

        [SerializeField] private MobileInput _swipeInput;
        private InputHandler _inputHandler;

        [SerializeField] private float _moveRate = 0.25f;
        private float _timer;

        private MoveDirection _currentMoveDirection;
        private bool _up, _down, _left, _right;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            InputHandler.Instance.OnEndTouch += SetPlayerDirection;
        }

        private void Start()
        {
            InputHandler.Instance.OnEndTouch += SetPlayerDirection;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _moveRate)
            {
                _timer = 0;
                Move();
            }
        }

        private void SetPlayerDirection(Vector2 position, float time)
        {

            if (_swipeInput.ToUp)
            {
            Debug.Log("Ocenka");
                _currentMoveDirection = MoveDirection.Up;
            }

            else if (_swipeInput.ToDown)
            {
                Debug.Log("Ocenka");
                _currentMoveDirection = MoveDirection.Down;
            }

            else if (_swipeInput.ToLeft)
            {
                _currentMoveDirection = MoveDirection.Left;
            }

            else if (_swipeInput.ToRight)
            {
                _currentMoveDirection = MoveDirection.Right;
            }
        }

        private void Move()
        {
            int x = 0;
            int y = 0;

            switch ( _currentMoveDirection )
            {
                case MoveDirection.Up:
                    y = 1;
                    break;
                case MoveDirection.Down:
                    y = -1;
                    break;
                case MoveDirection.Left:
                    x = -1;
                    break;
                case MoveDirection.Right:
                    x = 1;
                    break;
            }

            Node targetNode = MapDrawer.Instance.GetNode(_player.PlayerNode.X + x, _player.PlayerNode.Y + y);

            if ( targetNode == null )
            {
                // Game Over
            }

            else
            {
                if (targetNode == GameManager.Instance.YetiNode)
                {
                    GameManager.Instance.RandomSpawnYeti();
                }

                MapDrawer.Instance.AvaliableNodes.Remove(_player.PlayerNode);
                transform.position = targetNode.WordPosition;
                _player.SetPlayerNode(targetNode);
                MapDrawer.Instance.AvaliableNodes.Add(_player.PlayerNode);
            }
        }
    }

    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
