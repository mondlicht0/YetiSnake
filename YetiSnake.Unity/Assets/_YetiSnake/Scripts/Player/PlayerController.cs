using UnityEngine;
using YetiSnake.Input;
using YetiSnake.MapDraw;

namespace YetiSnake.PlayerObject
{
    public class PlayerController : MonoBehaviour
    {
        private Player _player;

        [SerializeField] private MobileInput _swipeInput;

        private MoveDirection _currentMoveDirection;
        private bool _up, _down, _left, _right;
        private bool _shouldMove;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            SetPlayerDirection();
            Move();
        }

        private void SetPlayerDirection()
        {
            if (_swipeInput.ToUp)
            {
                _currentMoveDirection = MoveDirection.Up;
                _shouldMove = true;
            }

            else if (_swipeInput.ToDown)
            {
                _currentMoveDirection = MoveDirection.Down;
                _shouldMove = true;
            }

            else if (_swipeInput.ToLeft)
            {
                _currentMoveDirection = MoveDirection.Left;
                _shouldMove = true;
            }

            else if (_swipeInput.ToRight)
            {
                _currentMoveDirection = MoveDirection.Right;
                _shouldMove = true;
            }
        }

        private void Move()
        {
            if (!_shouldMove)
            {
                return;
            }

            _shouldMove = false;

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
                transform.position = targetNode.WordPosition;
                _player.SetPlayerNode(targetNode);
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
